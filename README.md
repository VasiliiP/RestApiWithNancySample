# RestApiWithNancySample
Реализация REST API

Фреймворк .net 4.7
Применяемые компоненты: NancyFx, TopShelf, Dapper, Autofac либо дефолтный DI Nancy, Serilog, LibLog
Приложение должно быть реализовано в виде Self-hosted веб сервиса Nancy и иметь возможность запускаться в виде службы либо консольного приложения (TopShelf). Serilog должен настраиваться только в Assembly приложения.
Приложение должно содержать 3 сборки
Persons.Service – исполняемый модуль
Persons.Abstractions – контракты команд и Query
Persons – реализации web-модулей Nancy, IQueryHandler, ICommandHandler

Реализовать 2 Usecase
1.	CreatePerson
Создание сущности (DDD) Person по имени и дате рождения. У сущности должно быть вычисляемое поле Age. Создается фабричным методом класса. Метод возвращает null, если возраст больше 120 лет или Name пустой. 
Сущность должна сохраняться в репозитории, реализущем интерфейс
IPersonRepository
{
Person Find(Guid id);
void Insert(Person item);
}
Реализация должна быть на Dapper.
Реализация Usecase должна быть оформлена в виде обработчика команды CreatePerson.
Пришедшая команда в обработчике должна логгироваться через LibLog.

2.	GetPerson
Вернуть Person по id.
Реализация Usecase должна быть оформлена в виде обработчика Query GetPerson, но возвращать не доменный объект, а плоский Dto, сериализуемый в JSON.

Команды/query должны поступать по REST API.
1.	CreatePerson
POST /api/v1/persons/
JSON-body  {“Name”:”…”, “BirthDay”:”1977-07-07”}
Возвращаемый код
Created + заголовок Location /api/v1/persons/{person_id}, если команда выполнена
BadRequest, если невозможно создать команду из присланных данных
UnprocessableEntity, если созданная сущность невалидна
2.	GetPerson
GET /api/v1/persons/{person_id}
Возвращаемый код
ОК + JSON-body  {“Name”:”…”, “BirthDay”:”1977-07-07”}, если сущность найдена
NotFound, если не найдена

Должно быть реализовано 2 веб-модуля Nancy, вызывающие соответствующие обработчики. Зависимости должны быть инжектированы через конструктор.
