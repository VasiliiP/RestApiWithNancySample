
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
                    command = this.Bind<CreatePersonCommand>();
                }
                catch (ModelBindingException e)
                {
                    return Response.AsText("").WithStatusCode(HttpStatusCode.BadRequest);
                }
                
                commandDispatcher.Execute(command);
                    
                return Response.AsText("").WithStatusCode(HttpStatusCode.Created).WithHeader("Location", $"/api/v1/persons/{{command.Id}}");
            };
        }
    }
}
