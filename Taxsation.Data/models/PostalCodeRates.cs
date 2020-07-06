using System;
using System.Collections.Generic;

namespace Taxsation.Data.models
{
    public partial class PostalCodeRates
    {
        public int PostalCodeId { get; set; }
        public string PostalCode { get; set; }
        public int TaxTypeId { get; set; }

        public virtual TaxTypes TaxType { get; set; }
    }
}
