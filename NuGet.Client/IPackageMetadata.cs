using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public class IPackageMetadata
    {
        public IPackageMetadata FilterToAllowedPackageIdentities(ISet<IPackageIdentity> AllowedPackageIdentities)
        {
            throw new NotImplementedException();
        }
    }
}
