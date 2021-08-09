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
        private String name = null;
        private Genders gender;
        private int Age;

        public enum Genders:int {Male, Female};

        public Person()
        {
        }

        public Person(String name,DateTime birthDate,Genders gender)
        {
            this.name = name;
            Age = (int)(DateTime.Now.Year-birthDate.Year);
            this.gender = gender;
        }

        public override string ToString()
        {
            return String.Format("{0} is {1} and {2} years old.", name, gender, Age);
        }

        
        public void Serialize(string output)
        {
            var fileName = @$"C:\Users\Felhasználó\Documents\.NET modul\SI_assignments\2ndSIWeek\Serialization\{output}.txt";

            // Create file to save the data
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            FileStream fs = new FileStream(fileName, FileMode.Create);

            // Create and use a BinaryFormatter object to perform the serialization
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

            // Close the file
            finally
            {
                fs.Close();
            }
        }
    }
}
