using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

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
    }
}
