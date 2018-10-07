using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Persons.Abstractions.Entities;

namespace Persons.Abstractions.Data
{
    public class PersonRepository: SqLiteBaseRepository, IPersonRepository
    {
        
        public Person Find(Guid id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                Person result = cnn.Query<Person>(
                    @"SELECT Id, Name, DateOfBirth
                    FROM Person
                    WHERE Id = @id", new { id }).FirstOrDefault();
                return result;
            }
        }

        public void Insert(Person item)
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Query<Person>(
                    @"INSERT INTO Person 
                    ( ID, Name, BirthDay ) VALUES 
                    ( @ID, @Name, @BirthDay );
                    ", item);
            }
        }

        private static void CreateDatabase()
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    @"create table Person
                      (
                         ID                                  BLOB primary key,
                         Name                           varchar(100) not null,
                         BirthDay                           datetime not null
                      )");
            }
        }
    }
}
