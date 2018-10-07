using Persons.Abstractions;
using Persons.Abstractions.Commands;
using Persons.Abstractions.Entities;
using System;

namespace Persons.Api
{
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand>
    {
        private readonly IPersonRepository _PersonRepository;

        public CreatePersonCommandHandler(IPersonRepository personRepository)
        {
            _PersonRepository = personRepository;
        }

        public void Execute(CreatePersonCommand command)
        {
            var person = Person.CreatePerson(command.Name, command.BirthDay, command.Id);
            if (person == null)
                throw new ArgumentException(nameof(person));
            _PersonRepository.Insert(person);
        }
    }
}
