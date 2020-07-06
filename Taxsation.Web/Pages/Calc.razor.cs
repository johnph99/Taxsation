using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxsation.Web.Pages
{
    public partial class Calc
    {
        static List<Item> _zipcodes = null;
        static APIcaller caller;

        public Calc()
        {
            LoadList();
            caller = new APIcaller();
        }

        protected void LoadList()
        {
            if (caller==null)
                caller = new APIcaller();


            var retTask = caller.GetPostalCodes();
            _zipcodes = retTask.Result;

        }

        string _tax = null;

        private taxInfo _taxInfo = null;
        [Parameter]
        public taxInfo TaxInfo
        {
            get
            {
                if (_taxInfo == null)
                    _taxInfo = new taxInfo();
                return _taxInfo;
            }
            set { _taxInfo = value; }
        }

        protected void HandleValidSubmit()
        {
            
            var retTask = caller.CalculateTax(_taxInfo.PostalCode, _taxInfo.TaxableValue);
            _taxInfo.Tax = retTask.Result;
            _tax = _taxInfo.Tax.ToString();
        }

        public class taxInfo
        {
            public string PostalCode { get; set; }
            public decimal TaxableValue { get; set; }
            public decimal Tax { get; set; }
        }
    }
}
