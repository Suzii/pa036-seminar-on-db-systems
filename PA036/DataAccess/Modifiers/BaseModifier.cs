using System.Collections.Generic;

namespace DataAccess.Modifiers
{
    public abstract class BaseModifier
    {
        public IEnumerable<int> Ids { get; set; }

        public string OrderProperty { get; set; }

        public bool OrderDesc { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
