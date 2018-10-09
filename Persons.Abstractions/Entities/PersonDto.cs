using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Abstractions.Entities
{
    public class PersonDto
    {
        public PersonDto(Person person)
        {
            Name = person.Name;
            Id = person.Id;
            BirthDay = person.BirthDay.Date.ToShortDateString();
            Age = person.Age;
        }
        public string Name { get; }
        public Guid Id { get; }
        public string BirthDay { get; }
        public int Age { get; }
    }
}
