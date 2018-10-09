using Persons.Abstractions.Entities;
using System;

namespace Persons.Abstractions
{
    public interface IPersonRepository
    {
        Person Find(Guid id);
        void Insert(Person item);
    }

}
