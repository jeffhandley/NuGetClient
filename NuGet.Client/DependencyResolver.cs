using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Client
{
    public class DependencyResolver
    {
        public static DependencyResolution Resolve(ResolutionContext context, string id, SemanticVersion version, PackageInventory localInventory)
        {
            IPackageMetadata metadata = context.GetMetadata(id, version);

            

            throw new NotImplementedException();
        }
    }
}
