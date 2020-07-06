using System;
using System.Collections.Generic;

namespace Taxsation.Data.models
{
    public partial class TaxRates
    {
        public int RateIdId { get; set; }
        public int TaxTypeId { get; set; }
        public decimal Rate { get; set; }
        public decimal? UpperLimit { get; set; }
        public decimal? FlatValue { get; set; }

        public virtual TaxTypes TaxType { get; set; }
    }
}
