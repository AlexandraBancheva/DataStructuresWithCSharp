namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private Stack<ILink> links = new Stack<ILink>();

        public int Size => this.links.Count;

        public void Clear()
        {
            this.links.Clear();
        }

        public bool Contains(ILink link)
        {
            return this.links.Contains(link) ? true : false;
        }

        public ILink DeleteFirst()
        {
            if (this.links.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var linkDeleted = this.links.Last();

            var temp = new Stack<ILink>();
            while (this.links.Count != 0)
            {
                var current = this.links.Pop();
                if (current != linkDeleted)
                {
                    temp.Push(current);
                }
            }
            this.links = temp;

            return linkDeleted;
        }

        public ILink DeleteLast()
        {
            if (this.links.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var linkForDelete = this.links.Peek();
            this.links.Pop();
            return linkForDelete;
        }

        public ILink GetByUrl(string url)
        {
            return this.links.FirstOrDefault(u => u.Url == url) ?? null;
        }

        public ILink LastVisited()
        {
            if (this.links.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return this.links.Peek();
        }

        public void Open(ILink link)
        {
            this.links.Push(link);
        }

        public int RemoveLinks(string url)
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }

            var temp = new Stack<ILink>();
            int countRemovedLinks = 0;
            url = url.ToLower();
           

            while (this.links.Count != 0)
            {
                var link = this.links.Pop();
                if (link.Url.ToLower().Contains(url))
                {
                    countRemovedLinks++;
                }
                else
                {
                    temp.Push(link);
                }
            }
            this.links = temp;

            if (countRemovedLinks == 0)
            {
                throw new InvalidOperationException();
            }

            return countRemovedLinks;
        }

        public ILink[] ToArray()
        {
            return this.links.ToArray();
        }

        public List<ILink> ToList()
        {
            return this.links.ToList();
        }

        public string ViewHistory()
        {
            if (this.links.Count == 0)
            {
                return "Browser history is empty!";
            }
            var sb = new StringBuilder();
            foreach (var item in this.links)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
