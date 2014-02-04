using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public interface IPackageIdentity
    {
        string Id { get; set; }
        SemanticVersion Version { get; set; }

    }
}
