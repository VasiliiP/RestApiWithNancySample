using System;
using Autofac;
using Autofac.Core;
using Persons.Abstractions;
using Persons.Api.Logging;

namespace Persons.Api
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ILog log = LogProvider.GetCurrentClassLogger();

        private readonly IComponentContext _Context;
        public CommandDispatcher(IComponentContext context)
        {
            _Context = context;
        }


        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentNullException("Command");

            var handler = _Context.Resolve<ICommandHandler<TCommand>>();
                  
            if (handler == null)
                throw new DependencyResolutionException(nameof(ICommandHandler<TCommand>));

            
            log.Info(nameof(TCommand));

            handler.Execute(command);
        }
    }
}
