using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxsation.Web.Pages
{
    public partial class PreviousRequest
    {


        List<Requests> _requests = null;

        public PreviousRequest()
        {
            LoadList();
        }
        public void OnGet()
        {


        }

        protected void LoadList()
        {
            APIcaller caller = new APIcaller();
            var retTask  = caller.GetPreviousRequests();
            _requests = retTask.Result;

        }


    }
}
