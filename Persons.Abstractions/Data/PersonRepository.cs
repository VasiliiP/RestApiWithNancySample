using Dapper;
using Persons.Abstractions.Entities;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace Persons.Abstractions.Data
{
    public class PersonRepository: SqLiteBaseRepository, IPersonRepository
    {
        
        public Person Find(Guid id)
        {
            if (!File.Exists(DbFile)) return null;

            var byteArr = id.ToByteArray();
            SqlMapper.AddTypeHandler(new GuidHandler());

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();

                Person result = cnn.Query<Person>(
                    @"SELECT ID, Name, BirthDay
                    FROM Person
                    WHERE ID = @byteArr", new { byteArr }).FirstOrDefault();
                return result;
            }
        }

        public void Insert(Person item)
        {
            SqlMapper.AddTypeHandler(new GuidHandler());

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
                         ID                                  UNIQUEIDENTIFIER,
                         Name                           varchar(100) not null,
                         BirthDay                           datetime not null
                      )");
            }
        }
    }

    /// <summary>
    /// Для маппинга byte поля в таблице Sqlite cо свойством типа Guid
    /// </summary>
    public class GuidHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            return new Guid(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToByteArray();
        }
    }

}
