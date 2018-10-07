using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Persons.Abstractions;

namespace Persons.Api
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly TinyIoCContainer _Resolver;

        public QueryDispatcher(TinyIoCContainer resolver)
        {
            _Resolver = resolver;
        }

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null)
                throw new ArgumentNullException("Query");

            var handler = _Resolver.Resolve<IQueryHandler<TQuery, TResult>>();

            if (handler == null)
                throw new TinyIoCResolutionException(typeof(TQuery));

            return handler.Execute(query);
        }
    }
}
