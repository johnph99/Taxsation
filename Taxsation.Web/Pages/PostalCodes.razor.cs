using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxsation.Web.Pages
{
    public partial class PostalCodes
    {


        List<Item> _taxTypes = null;

        public PostalCodes()
        {
            LoadList();
        }
        public void OnGet()
        {


        }

        protected void LoadList()
        {
            APIcaller caller = new APIcaller();
            var retTask  = caller.GetPostalCodeTaxTypes();
            _taxTypes = retTask.Result;

        }


    }
}
