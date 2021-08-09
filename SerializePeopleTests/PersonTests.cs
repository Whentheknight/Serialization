using NUnit.Framework;
using SerializePeople;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializePeople.Tests
{
    [TestFixture]
    public class PersonTests
    {
        public Person person;
        [SetUp]
        public void Setup()
        {
            person = new Person("Bela", DateTime.Parse("1988-02-03"), Person.Genders.Male);
        }
        [Test]
        public void ToString_ConvertsFieldValuesAsExpected_AreEqual()
        {
            Assert.AreEqual("Bela is Male and 33 years old.",person.ToString());
        }

        [Test]
        public void Serialize_WritesGoodValuesIntoFile_IsTrue()
        {
            var output = "test";
            person.Serialize(output);

            string readLines;

            var current = Directory.GetCurrentDirectory();
            var fileName = Path.Combine(current, $"{output}.txt");



            using (StreamReader sr = File.OpenText(fileName))
            {
                readLines = "";
                while (sr.ReadLine()!= null)
                {
                    readLines += sr.ReadLine();
                }
            }
            Assert.IsTrue(readLines.Contains("birth date"));
        }

        [Test]
        public void Deserialize_CheckIfGender_AreEqual_AfterDeserialization()
        {
            var readFrom = "test";
            Assert.AreEqual(person.Gender,Person.Deserialize(readFrom).Gender);
            Console.WriteLine(Person.Deserialize(readFrom).Name);
        }
    }
}