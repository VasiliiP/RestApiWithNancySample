using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persons.Abstractions;
using Persons.Abstractions.Entities;

namespace Persons.Api
{
    public class GetPersonQueryHandler : IQueryHandler<GetPersonQuery, Person>
    {
        private readonly IPersonRepository _PersonRepository;

        public GetPersonQueryHandler(IPersonRepository personRepository)
        {
            _PersonRepository = personRepository;
        }

        public Person Execute(GetPersonQuery query)
        {
            var person = _PersonRepository.Find(query.Id);
            return person;
        }
    }
}
