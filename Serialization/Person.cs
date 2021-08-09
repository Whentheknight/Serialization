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
    public class Person: IDeserializationCallback, ISerializable
    {
        private string name;
        public string Name { get=>name; set=>name = value; }
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

        public void OnDeserialization(object sender)
        {
            age = (int)(DateTime.Now.Year - BirthDate.Year);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", name, typeof(string));
            info.AddValue("birth date", birthDate, typeof(DateTime));
            info.AddValue("gender", gender, typeof(Genders));
        }

        public Person(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            name = (string)info.GetValue("name", typeof(string));
            birthDate = (DateTime)info.GetValue("birth date", typeof(DateTime));
            gender = (Genders)info.GetValue("gender", typeof(Genders));
        }
    }
}
