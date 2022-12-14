namespace _02.DOM
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using _02.DOM.Interfaces;
    using _02.DOM.Models;

    public class DocumentObjectModel : IDocument
    {
        public DocumentObjectModel(IHtmlElement root)
        {
            this.Root = root;
        }

        public DocumentObjectModel()
        {
            this.Root = new HtmlElement(
                ElementType.Document,
                new HtmlElement(
                    ElementType.Html,
                    new HtmlElement(ElementType.Head),
                    new HtmlElement(ElementType.Body)
                )
            );
        }

        public IHtmlElement Root { get; private set; }

        public IHtmlElement GetElementByType(ElementType type)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(this.Root);
            while (queue.Count != 0)
            {
                var currentElement = queue.Dequeue();
                if (currentElement.Type == type)
                {
                    return currentElement;
                }

                foreach (var child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return null;
        }

        public List<IHtmlElement> GetElementsByType(ElementType type)
        {
            var result = new List<IHtmlElement>();
            this.GetElementByTypeUseDfs(this.Root, type, result);
            return result;
        }

        private void GetElementByTypeUseDfs(IHtmlElement node, ElementType type, List<IHtmlElement> result)
        {
            foreach (var child in node.Children)
            {
                this.GetElementByTypeUseDfs(child, type, result);
            }

            if (node.Type == type)
            {
                result.Add(node);
            }
        }

        public bool Contains(IHtmlElement htmlElement)
        {
            return this.FindElement(htmlElement) != null;
        }

        private IHtmlElement FindElement(IHtmlElement htmlElement)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var currentElement = queue.Dequeue();
                if (currentElement == htmlElement)
                {
                    return currentElement;
                }

                foreach (var child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public void InsertFirst(IHtmlElement parent, IHtmlElement child)
        {
            if (!this.Contains(parent))
            {
                throw new InvalidOperationException();
            }
            parent.Children.Insert(0, child);
            child.Parent = parent;
        }

        public void InsertLast(IHtmlElement parent, IHtmlElement child)
        {
            if (!this.Contains(parent))
            {
                throw new InvalidOperationException();
            }
            parent.Children.Add(child);
            child.Parent = parent;
        }

        public void Remove(IHtmlElement htmlElement)
        {
            if (!this.Contains(htmlElement))
            {
                throw new InvalidOperationException();
            }
            htmlElement.Parent.Children.Remove(htmlElement);
            htmlElement.Parent = null;
            htmlElement.Children.Clear();
        }

        public void RemoveAll(ElementType elementType)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var htmlElement = queue.Dequeue();

                if (htmlElement.Type == elementType)
                {
                    htmlElement.Parent.Children.Remove(htmlElement);
                    htmlElement.Parent = null;
                    htmlElement.Children.Clear();
                }

                foreach (var child in htmlElement.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        public bool AddAttribute(string attrKey, string attrValue, IHtmlElement htmlElement)
        {
            if (!this.Contains(htmlElement))
            {
                throw new InvalidOperationException();
            }

            return htmlElement.AddAttribute(attrKey, attrValue);
        }

        public bool RemoveAttribute(string attrKey, IHtmlElement htmlElement)
        {
            if (!this.Contains(htmlElement))
            {
                throw new InvalidOperationException();
            }
            return htmlElement.RemoveAttribute(attrKey);
        }

        public IHtmlElement GetElementById(string idValue)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var currentElement = queue.Dequeue();
                if (currentElement.CheckId(idValue))
                {
                    return currentElement;
                }

                foreach (var child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            this.ToStringByDfs(this.Root, 0, sb);

            return sb.ToString();
        }

        private void ToStringByDfs(IHtmlElement node, int indent, StringBuilder sb)
        {
            sb.Append(' ', indent).AppendLine(node.Type.ToString());

            foreach (var child in node.Children)
            {
                this.ToStringByDfs(child, indent + 2, sb);
            }
        }
    }
}
