using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public class DependencyResolution
    {
        public IEnumerable<IPackageIdentity> Installs { get; private set; }
        public IEnumerable<IPackageIdentity> Uninstalls { get; private set; }

    }
}
