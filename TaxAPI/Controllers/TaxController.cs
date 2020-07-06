using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Taxsation.Data.models;

namespace TaxAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "These are not the droids we are looking for.", "Move along." };
        }

        private readonly ILogger<TaxController> _logger;
        private Taxsation.Data.models.DBContext _db = null;
        

        public TaxController(ILogger<TaxController> logger, Taxsation.Data.models.DBContext dbContext)
        {
            _logger = logger;
            _db = dbContext;

           

        }

        [HttpGet("CalculateTax/{zipCode}/{taxableAmount}")]
        public IActionResult CalculateTax(string zipCode, decimal taxableAmount)
        {
            decimal taxvalue = 0;

            bool testing = false;
            if (zipCode.StartsWith("test"))
            {
                zipCode = zipCode.Replace("test", "");
                testing = true;
            }
            var postalCode = _db.PostalCodeRates.FirstOrDefault(a => a.PostalCode == zipCode);
            if (postalCode != null)
            {
                int taxTypeID = postalCode.TaxTypeId;
                //var rates = _db.TaxRates.Where(a => a.TaxTypeId == taxTypeID);
                var ratesMaster = _db.TaxTypes.Include(t => t.TaxRates).FirstOrDefault(a => a.TaxTypeId == taxTypeID);
                ICollection<TaxRates> rates = (ICollection<TaxRates>)ratesMaster.TaxRates;

                switch (ratesMaster.TaxTypeName)
                {
                    case "Progressive":
                        taxvalue = CalcProgressive(rates, taxableAmount);
                        break;
                    case "Flat Value":
                        taxvalue = CalcFlatValue(rates, taxableAmount);
                        break;
                    case "Flat Rate":
                        taxvalue = CalcFlatRate(rates, taxableAmount);
                        break;
                }
            }
            if (!testing)
            {
                var req = new Requests
                {
                    CalculatedTax = taxvalue
                    ,
                    RequestDate = DateTime.Now
                    ,
                    RequestValue = taxableAmount
                    ,
                    RequestZipCode = zipCode
                };
                _db.Add<Requests>(req);
                _db.SaveChanges();
            }
            return Ok(taxvalue);
        }

        private decimal CalcProgressive(ICollection<TaxRates> rates, decimal taxableAmount)
        {
            decimal tax = 0;
            decimal lastLimit = 0;

            foreach (var row in rates)
            {
                if (row.UpperLimit == null)
                {
                    tax += (row.Rate / 100) * ((taxableAmount) - lastLimit);
                    break;
                }
                else if (taxableAmount > row.UpperLimit)
                {
                    tax += (row.Rate / 100) * ((decimal)(row.UpperLimit) - lastLimit);
                    lastLimit = (decimal)row.UpperLimit;
                }
                else
                {
                    tax += (row.Rate / 100) * ((taxableAmount) - lastLimit);
                    break;
                }
            }

            return tax;
        }

        private decimal CalcFlatRate(ICollection<TaxRates> rates, decimal taxableAmount)
        {
            decimal tax = 0;

            foreach (var row in rates)
            {

                tax += (row.Rate / 100) * taxableAmount;

                break;
            }

            return tax;
        }

        private decimal CalcFlatValue(ICollection<TaxRates> rates, decimal taxableAmount)
        {
            decimal tax = 0;

            foreach (var row in rates)
            {
                if (taxableAmount < row.UpperLimit)
                {
                    tax += (row.Rate / 100) * taxableAmount;
                }
                else
                {
                    tax += (decimal)row.FlatValue;
                }
                break;
            }
            return tax;
        }

        [HttpGet("GetTaxTypes")]
        public IActionResult GetTaxTypes()
        {
            List<GenericItem> types = new List<GenericItem>();
            var itemsDb = _db.TaxTypes.ToList();
            foreach (var item in itemsDb)
            {
                var typ = new GenericItem();
                typ.Identifier = item.TaxTypeId.ToString();
                typ.Description = item.TaxTypeName;

                types.Add(typ);
            }
            return  Ok(types);
        }

        [HttpGet("GetPostalCodes")]
        public IActionResult GetPostalCodes()
        {
            List<GenericItem> types = new List<GenericItem>();
            var itemsDb = _db.PostalCodeRates.ToList();
            foreach (var item in itemsDb)
            {
                var typ = new GenericItem();
                typ.Identifier  = item.PostalCodeId.ToString();
                typ.Description = item.PostalCode;

                types.Add(typ);
            }

            return Ok(types);
        }

        [HttpGet("GetPostalCodeTaxTypes")]
        public IActionResult GetPostalCodeTaxTypes()
        {
            List<GenericItem> types = new List<GenericItem>();
            var itemsDb = _db.PostalCodeRates.Include(a => a.TaxType).ToList();
            foreach (var item in itemsDb)
            {
                var typ = new GenericItem();
                typ.Identifier  = item.PostalCode;
                typ.Description = item.TaxType.TaxTypeName;

                types.Add(typ);
            }
            return Ok(types);
        }

        [HttpGet("GetRequests")]
        public IActionResult GetRequests()
        {
            List<Requests> types = new List<Requests>();
            var requestDb = _db.Requests.ToList();
            
            return Ok(requestDb);
        }

    }


}
