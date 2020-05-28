using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactsThrowingAPI.DAL.DAO
{
    public class TagDAO
    {

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Fact_TagDAO> Tags_Fact { get; set; }
    }
}
