using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializePeople;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializePeople.Tests
{
    [TestClass()]
    public class PersonTests
    {
        public Person person;
        [TestInitialize]
        public void Setup()
        {
            person = new Person("Bela", DateTime.Parse("1988-02-03"), Person.Genders.Male);
        }
        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("Bela is Male and 33 years old.",person.ToString());
        }

        [TestMethod()]
        public void SerializeTest()
        {

        }
    }
}