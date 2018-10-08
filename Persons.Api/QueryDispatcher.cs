using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Persons.Abstractions;

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
