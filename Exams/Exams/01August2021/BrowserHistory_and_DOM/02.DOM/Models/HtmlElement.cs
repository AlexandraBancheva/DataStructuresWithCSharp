namespace _02.DOM.Models
{
    using System;
    using System.Collections.Generic;
    using _02.DOM.Interfaces;

    public class HtmlElement : IHtmlElement
    {
        public HtmlElement(ElementType type, params IHtmlElement[] children)
        {
            this.Type = type;
            this.Children = new List<IHtmlElement>();
            this.Attributes = new Dictionary<string, string>();

            foreach (var child in children)
            {
                this.Children.Add(child);
                child.Parent = this;
            }
        }

        public ElementType Type { get; set; }

        public IHtmlElement Parent { get; set; }

        public List<IHtmlElement> Children { get; }

        public Dictionary<string, string> Attributes { get; }

        public bool AddAttribute(string attrKey, string attrValue)
        {
            if (this.Attributes.ContainsKey(attrKey))
            {
                return false;
            }

            this.Attributes.Add(attrKey, attrValue);

            return true;
        }

        public bool CheckId(string id)
        {
            if (this.Attributes.ContainsKey("id"))
            {
                return this.Attributes["id"] == id;
            }
            return false;
        }

        public bool RemoveAttribute(string attrKey)
        {
            if (!this.Attributes.ContainsKey(attrKey))
            {
                return false;
            }
            this.Attributes.Remove(attrKey);
            return true;
        }
    }
}
