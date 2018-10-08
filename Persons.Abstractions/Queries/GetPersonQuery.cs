using System;
using Persons.Abstractions;
using Persons.Abstractions.Entities;

namespace Persons.Api
{
    public class GetPersonQuery: IQuery<Person>
    {
        public Guid Id { get; set; }

        public GetPersonQuery()
        {
        }
    }
}
