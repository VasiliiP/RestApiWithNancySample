using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace Persons.Api
{
    public class NancyTestModule: NancyModule
    {
        public NancyTestModule() : base("/products")
        {
            // would capture routes to /products/list sent as a synchronous GET request
            Get["/list"] = parameters => {
                return "The list of products";
            };
        }

    }
}
