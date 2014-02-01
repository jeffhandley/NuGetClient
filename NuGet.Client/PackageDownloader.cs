using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Client
{
    public class PackageDownloader
    {
        private PackageMetadataClient _metadataClient;

        public PackageDownloader(Uri repository)
        {
            _metadataClient = RepositoryContext.Clients.CreatePackageMetadataClient(repository);
        }

        public IEnumerable<IPackageMetadata> GetSortedPackageVersions(string packageId)
        {
            IEnumerable<IPackageMetadata> versionsAvailable = _metadataClient.GetPackageVersions(packageId);

            return versionsAvailable.OrderBy(m => m.Version, SemanticVersionComparer.Strict);
        }

        public IEnumerable<IPackageMetadata> FilterToArchitecture(IEnumerable<IPackageMetadata> packages, string architecture)
        {
            return packages.Where(m => m.GetMetadataProperty<string>("architecture") == architecture);
        }


    }
}
