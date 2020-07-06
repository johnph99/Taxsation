using System;
using System.Collections.Generic;

namespace Taxsation.Data.models
{
    public partial class Requests
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal RequestValue { get; set; }
        public string RequestZipCode { get; set; }
        public decimal CalculatedTax { get; set; }
    }
}
