using Autofac;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Persons.Abstractions;
using Persons.Abstractions.Commands;
using Persons.Abstractions.Data;
using Persons.Abstractions.Entities;
using Persons.Api;

namespace Persons.Service
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            //var builder = new ContainerBuilder();
            //builder.RegisterType<CreatePersonCommandHandler>()
            //    .As<ICommandHandler<CreatePersonCommand>>()
            //    .InstancePerLifetimeScope();

            //builder.Update(existingContainer.ComponentRegistry);
        }
        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during application startup.
        }


        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            // Perform registrations that should have a request lifetime
            container.Update(builder => builder.RegisterType<CreatePersonCommandHandler>().As<ICommandHandler<CreatePersonCommand>>());
            container.Update(builder => builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>());
            container.Update(builder => builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>());
            container.Update(builder => builder.RegisterType<GetPersonQueryHandler>().As<IQueryHandler<GetPersonQuery, Person>>());
            container.Update(builder => builder.RegisterType<PersonRepository>().As<IPersonRepository>());
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during request startup.
        }
    }
}
