using FactsThrowingAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactsThrowingAPI.Models
{
    public class Fact : Entity
    {
        public Fact(Guid id, string title, string content)
        {
            this.Id = id;
            this.Content = content;
            this.Title = title;
        }
        public Fact(string title, string content)
        {
            this.Id = Guid.NewGuid();
            this.Content = content;
            this.Title = title;
        }

        public string Title { get; set; }
        public string Content { get; set; }

    }
}
