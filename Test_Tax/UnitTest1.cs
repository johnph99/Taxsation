using NUnit.Framework;
using TaxAPI.Controllers;

namespace Test_Tax
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1_1()
        {
            Taxsation.Data.models.DBContext dbContext = new Taxsation.Data.models.DBContext();

            var controller = new TaxController(null, dbContext);

            var retval = controller.CalculateTax("test7441", 50000);
            decimal tax = (decimal)((Microsoft.AspNetCore.Mvc.OkObjectResult)retval).Value;

            Assert.AreEqual(8687.50, tax);
            Assert.Pass();

        }

        [Test]
        public void Test1_2()
        {
            Taxsation.Data.models.DBContext dbContext = new Taxsation.Data.models.DBContext();

            var controller = new TaxController(null, dbContext);

            var retval = controller.CalculateTax("test1000", 500000);
            decimal tax = (decimal)((Microsoft.AspNetCore.Mvc.OkObjectResult)retval).Value;

            Assert.AreEqual( 152683.50, tax);
            Assert.Pass();

        }

        [Test]
        public void Test2_1()
        {
            Taxsation.Data.models.DBContext dbContext = new Taxsation.Data.models.DBContext();

            var controller = new TaxController(null, dbContext);

            var retval = controller.CalculateTax("testA100", 50000);
            decimal tax = (decimal)((Microsoft.AspNetCore.Mvc.OkObjectResult)retval).Value;

            Assert.AreEqual(2500, tax);
            Assert.Pass();

        }

        [Test]
        public void Test2_2()
        {
            Taxsation.Data.models.DBContext dbContext = new Taxsation.Data.models.DBContext();

            var controller = new TaxController(null, dbContext);

            var retval = controller.CalculateTax("testA100", 300000);
            decimal tax = (decimal)((Microsoft.AspNetCore.Mvc.OkObjectResult)retval).Value;

            Assert.AreEqual(10000, tax);
            Assert.Pass();
        }

        [Test]
        public void Test3()
        {
            Taxsation.Data.models.DBContext dbContext = new Taxsation.Data.models.DBContext();

            var controller = new TaxController(null, dbContext);

            var retval = controller.CalculateTax("test7000", 50000);
            decimal tax = (decimal)((Microsoft.AspNetCore.Mvc.OkObjectResult)retval).Value;

            Assert.AreEqual(8750, tax);
            Assert.Pass();

        }

    }
}