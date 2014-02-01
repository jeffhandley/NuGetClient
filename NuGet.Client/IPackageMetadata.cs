using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public class IPackageMetadata
    {
        public string Id { get; set; }
        public SemanticVersion Version { get; set; }
        public T GetMetadataProperty<T>(string name);
    }
}
