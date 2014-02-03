using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NuGet.Client
{
    public class PackageDocumentClient
    {
        public PackageDocumentClient(Configuration.Repositories repositories)
        {

        }

        public XDocument GetNuspec(IPackageMetadata package)
        {
            throw new NotImplementedException();
        }
    }

    public static class PackageDocumentClientExtensions
    {
        public static PackageDocumentClient CreatePackageDocumentClient(this Clients host, Configuration.Repositories repositories)
        {
            return new PackageDocumentClient(repositories);
        }
}
}
