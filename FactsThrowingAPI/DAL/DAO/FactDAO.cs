using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactsThrowingAPI.DAL.DAO
{
    public class FactDAO 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual ICollection<Fact_TagDAO> Facts_Tags { get; set; }
    }
}
