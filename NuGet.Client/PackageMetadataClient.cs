using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NuGet.Client
{
    public class PackageMetadataClient
    {
        public PackageMetadataClient(Configuration.Repositories repositories)
        {

        }

        internal IEnumerable<IPackageVersionMetadata> GetPackageVersions(string packageId)
        {
            throw new NotImplementedException();
        }

        public IPackageMetadata GetPackageMetadata(string packageId)
        {
            throw new NotImplementedException();
        }

        public XDocument GetNuspec(IPackageVersionMetadata package)
        {
            throw new NotImplementedException();
        }

        internal IPackageMetadata GetPackageMetadata(string packageId, SemanticVersion version)
        {
            throw new NotImplementedException();
        }
    }

    public static class PackageMetadataClientExtensions
    {
        public static PackageMetadataClient CreatePackageMetadataClient(this Clients host, Configuration.Repositories repositories)
        {
            return new PackageMetadataClient(repositories);
        }

    }
}
