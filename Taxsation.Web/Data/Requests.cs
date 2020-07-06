using System;
using System.Collections.Generic;

namespace Taxsation.Web
{
    public partial class Requests
    {
        public int Id { get; set; }
        public DateTime requestDate { get; set; }
        public decimal requestValue { get; set; }
        public string requestZipCode { get; set; }
        public decimal calculatedTax { get; set; }
    }
}
