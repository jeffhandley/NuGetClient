using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public interface INupkg
    {
        IEnumerable<IPackageMetadata> MetadataDefinitions { get; set; }

        void UnpackTo(string targetPath);
    }
}
