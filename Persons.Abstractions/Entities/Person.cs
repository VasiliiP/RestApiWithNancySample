using System;

namespace Persons.Abstractions.Entities
{
    public class Person
    {
        public string Name { get; private set; }
        public Guid Id { get; private set; }
        public DateTime BirthDay { get; private set; }

        public int Age => CalculateAge();

        private int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDay.Year;
            if (BirthDay > today.AddYears(-age))
                age--;

            return age;
        }

        public static Person CreatePerson(string name, DateTime birthDay, Guid id)
        {
            var person = new Person
            {
                Name = name,
                BirthDay = birthDay,
                Id = id
            };

            if (person.Age > 120 || string.IsNullOrWhiteSpace(person.Name))
                return null;

            return person;
        }
    }
}
