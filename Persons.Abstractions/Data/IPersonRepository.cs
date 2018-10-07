using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persons.Abstractions.Entities;

namespace Persons.Abstractions
{
    public interface IPersonRepository
    {
        Person Find(Guid id);
        void Insert(Person item);
    }

}
