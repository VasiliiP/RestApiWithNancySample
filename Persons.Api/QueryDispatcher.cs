using Autofac;
using Nancy.TinyIoc;
using Persons.Abstractions;
using System;

namespace Persons.Api
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _Context;
        public QueryDispatcher(IComponentContext context)
        {
            _Context = context;
        }

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null)
                throw new ArgumentNullException("Query");

            var handler = _Context.Resolve<IQueryHandler<TQuery, TResult>>();

            if (handler == null)
                throw new TinyIoCResolutionException(typeof(TQuery));

            return handler.Execute(query);
        }
    }
}
