using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;

namespace SerializePeople
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        private Genders gender;
        private DateTime birthDate;
        [NonSerialized]
        private int age;

        
        

        public Genders Gender { get => gender; private set => gender = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public int Age { get => age; }

        public enum Genders : int { Male, Female };

        public Person()
        {
        }

        public Person(String name, DateTime birthDate, Genders gender)
        {
            this.Name = name;
            this.BirthDate = birthDate;
            this.age = (int)(DateTime.Now.Year - BirthDate.Year);
            this.Gender = gender;
        }

        public override string ToString()
        {
            return String.Format("{0} is {1} and {2} years old.", Name, Gender, Age);
        }


        public void Serialize(string output)
        {

            var current = Directory.GetCurrentDirectory();


            var fileName = Path.Combine(current, $"{output}.txt");

            // Create file to save the data
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, this);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }

            }
        }
        public static Person Deserialize(string readFrom)
        {
            var current = Directory.GetCurrentDirectory();


            var fileName = Path.Combine(current, $"{readFrom}.txt");

            Person person = new Person();

            FileStream fs = new FileStream(fileName, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                person = (Person)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            return person;

        }

    }
}
