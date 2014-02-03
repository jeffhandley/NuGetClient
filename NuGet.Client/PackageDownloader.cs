using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NuGet.Client
{
    public class PackageDownloader
    {
        private FeedsClient _feedsClient;
        private PackageMetadataClient _metadataClient;
        private PackageDownloadClient _downloadClient;

        public PackageDownloader(Configuration.Repositories repositories)
        {
            // The Repositories are loaded from the NuGet configuration.
            // They indicate what repositories are enabled (by Uri), and which
            // services are enabled within them.  For instance, it's possible
            // to disable services within a repository even though the repository
            // offers them.  This allows the user full control over which repositories
            // are used for which services.  By default, all services are enabled
            // on a repository though, which will be the case almost 100% of the time.
            _feedsClient = RepositoryContext.Clients.CreateFeedsClient(repositories);
            _metadataClient = RepositoryContext.Clients.CreatePackageMetadataClient(repositories);
            _downloadClient = RepositoryContext.Clients.CreatePackageDownloadClient(repositories);
        }

        public IFeed GetFeed(string feedId, SemanticVersion feedVersion)
        {
            foreach (IFeed feed in _feedsClient.GetFeedVersions(feedId))
            {
                if (SemanticVersion.Compare(feedVersion, feed.Version, SemanticVersionComparison.Exact) == 0)
                {
                    return feed;
                }
            }

            return null;
        }

        public IEnumerable<IPackageMetadata> GetSortedPackageVersionsWithinFeeds(string packageId, IEnumerable<IFeed> feeds)
        {
            IEnumerable<IFeedPackage> packageVersionFilter = feeds.SelectMany(f => f.Packages
                .Where(p => String.Compare(p.PackageId, packageId, StringComparison.OrdinalIgnoreCase) == 0));

            IEnumerable<IPackageMetadata> versionsAvailable = _metadataClient.GetPackageVersions(packageId);

            return versionsAvailable
                .Where(v => packageVersionFilter.Any(f => SemanticVersion.Compare(f.PackageVersion, v.Version, SemanticVersionComparison.Exact) == 0))
                .OrderBy(m => m.Version, SemanticVersionComparer.MajorMinorPatchPrerelease);
        }

        public IEnumerable<IPackageMetadata> FilterToArchitecture(IEnumerable<IPackageMetadata> packages, string architecture)
        {
            // Simple scalar properties are available directly from the metadata returned
            // from the metadata service.  But complex properties are not.
            return packages.Where(m => m.GetMetadataProperty<string>("architecture") == architecture);
        }

        public IEnumerable<IPackageMetadata> FilterToVCToolset (IEnumerable<IPackageMetadata> packages, string vcToolset)
        {
            foreach (IPackageMetadata package in packages)
            {
                // If the IPackageMetadata includes a link to the package's nuspec file
                // that link will be followed to get the nuspec file and load it as an
                // XDocument.  If the IPackageMetadata does not include a link to the
                // nuspec file, then the Id/Version of the package will be used to fetch
                // the nuspec file from the _metadataClient from one of the repositories.
                XDocument nuspec = _metadataClient.GetNuspec(package);

                if (nuspec.Root
                    .Element("package")
                    .Element("metadata")
                    .Elements("vcToolset")
                    .Any(p => string.Compare(p.Value, vcToolset,  StringComparison.OrdinalIgnoreCase) == 0))
                {
                    yield return package;
                }
            }
        }

        public void DownloadNupkgs(IEnumerable<IPackageMetadata> packages)
        {
            foreach (IPackageMetadata package in packages)
            {
                INupkg nupkg = _downloadClient.GetNupkg(package);
            }
        }
    }
}
