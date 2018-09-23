using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using CodingChallengeKneat;
using CodingChallengeKneat.Controllers;


namespace CodingChallengeKneat.Tests
{
    /// <summary>
    /// Summary description for HomeControllerTest
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        public HomeControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

    

        [TestMethod]
        public void IndexTest()
        {
            //Arrange
            HomeController controller = new HomeController();
            
            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void StopListTest()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void stopsTest()
        {
            string mglt = "75";
            string consum = "2 months";
            string distance = "3000000";

            //Arrange
            HomeController controller = new HomeController();

            //Act

            int value = controller.stops(mglt, consum, distance);

            // Assert
            Assert.AreEqual(27, value);
        }

        [TestMethod]
        public void convertToDayTest()
        {
            string value = "2 months";
            //Arrange
            HomeController controller = new HomeController();

            //Act

            double days = controller.ConvertToDay(value);

            // Assert
            Assert.AreEqual(62.0, days);
        }
    }
}
