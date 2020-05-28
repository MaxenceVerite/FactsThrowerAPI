using FactsThrowingAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactsThrowingAPI.Models
{
    public class Tag : Entity
    { 
        public Tag(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Tag(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
