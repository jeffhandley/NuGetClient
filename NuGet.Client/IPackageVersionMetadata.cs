using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NuGet.Client
{
    public class IPackageVersionMetadata
    {
        public string Id { get; set; }
        public SemanticVersion Version { get; set; }
        public T GetMetadataProperty<T>(string name)
        {
            return default(T);
        }
    }
}
