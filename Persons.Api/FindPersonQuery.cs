using System;
using Persons.Abstractions;
using Persons.Abstractions.Entities;

namespace Persons.Api
{
    public class FindPersonQuery: IQuery<Person>
    {
        public Guid Id { get; }

        public FindPersonQuery(Guid id)
        {
            Id = id;
        }
    }
}
