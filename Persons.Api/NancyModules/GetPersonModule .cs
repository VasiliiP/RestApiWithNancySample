using System;
using Nancy;
using Nancy.ModelBinding;
using Persons.Abstractions;
using Persons.Abstractions.Entities;

namespace Persons.Api.NancyModules
{
    public class GetPersonModule : NancyModule
    {
        public GetPersonModule(IQueryDispatcher queryDispatcher) : base("/api/v1/")
        {
            Get["/persons/{id}"] = parameters =>
            {
                GetPersonQuery query = null;
                try
                {
                    query = this.Bind<GetPersonQuery>();
                }
                catch (ModelBindingException e)
                {
                    return Response.AsText(e.Message).WithStatusCode(HttpStatusCode.BadRequest);
                }

                Person person = null;
                try
                {
                    person = queryDispatcher.Execute<GetPersonQuery, Person>(query);
                }
                catch (Exception e)
                {
                    return Response.AsText(e.Message).WithStatusCode(HttpStatusCode.InternalServerError);
                }


                return person == null ? Response.AsText("").WithStatusCode(HttpStatusCode.NotFound) 
                    : Response.AsJson(person).WithStatusCode(HttpStatusCode.OK);
            };
        }
    }
}
