using System;
using System.Collections.Generic;

namespace Taxsation.Data.models
{
    public partial class TaxTypes
    {
        public TaxTypes()
        {
            PostalCodeRates = new HashSet<PostalCodeRates>();
            TaxRates = new HashSet<TaxRates>();
        }

        public int TaxTypeId { get; set; }
        public string TaxTypeName { get; set; }

        public virtual ICollection<PostalCodeRates> PostalCodeRates { get; set; }
        public virtual ICollection<TaxRates> TaxRates { get; set; }
    }
}
