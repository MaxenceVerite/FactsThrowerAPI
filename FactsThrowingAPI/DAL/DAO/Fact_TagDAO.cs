using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactsThrowingAPI.DAL.DAO
{
    public class Fact_TagDAO
    {
        public Guid IdTag { get; set; }
        public Guid IdFact { get; set; }

        public virtual FactDAO Fact { get; set; }

        public virtual TagDAO Tag { get; set; }
    }
}
