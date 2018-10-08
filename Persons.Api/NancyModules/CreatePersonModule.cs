
using System;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Persons.Abstractions;
using Persons.Abstractions.Commands;

namespace Persons.Api.NancyModules
{
    public class CreatePersonModule : NancyModule
    {
        public CreatePersonModule(ICommandDispatcher commandDispatcher) : base("/api/v1/")
        {
            Post["/persons"] = parameters =>
            {
                CreatePersonCommand command = null;
                try
                {
                    command = this.Bind<CreatePersonCommand>(c => c.Id);
                }
                catch (ModelBindingException e)
                {
                    return Response.AsText("").WithStatusCode(HttpStatusCode.BadRequest);
                }

                try
                {
                    commandDispatcher.Execute(command);
                }
                catch (ArgumentException ae)
                {
                    return Response.AsText(ae.Message).WithStatusCode(HttpStatusCode.UnprocessableEntity);
                }
                catch (Exception e)
                {
                    return Response.AsText(e.Message).WithStatusCode(HttpStatusCode.InternalServerError);
                }

                return Response.AsText("").WithStatusCode(HttpStatusCode.Created).WithHeader("Location", $"/api/v1/persons/{command.Id}");
            };
        }
    }
}
