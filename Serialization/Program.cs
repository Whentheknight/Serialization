using System;

namespace SerializePeople
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person("Vela", DateTime.Parse("1995-01-26"), Person.Genders.Male);
            person.Serialize("ide");
        }
    }
}
