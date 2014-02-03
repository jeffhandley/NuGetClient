using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public interface IFeedPackage
    {
        string PackageId { get; set; }
        SemanticVersion PackageVersion { get; set; }
    }
}
