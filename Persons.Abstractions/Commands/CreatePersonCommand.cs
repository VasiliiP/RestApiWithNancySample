using System;

namespace Persons.Abstractions.Commands
{
    public class CreatePersonCommand: ICommand
    {
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public Guid Id { get; set; }


        public CreatePersonCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
