using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Client
{
    public static class RepositoryContext
    {
        public static RepositoryContext()
        {
            Clients = new Clients();
        }

        public static Clients Clients { get; private set; }
    }
}
