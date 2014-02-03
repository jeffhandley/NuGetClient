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
        private PackageDocumentClient _documentClient;
        private PackageDownloadClient _downloadClient;

        /// <summary>
        /// Ignore the name of the class really.  The point is to illustrate:
        /// 1. Download a feed definition from the server, which will provide a
        ///    list of all package ids/versions that can be used.  Think of this
        ///    as either A) a curated feed of the package ids/versions that were
        ///    shipped in the box with VS and/or supported by CSS, or B) the
        ///    package ids/versions that were involved in a build of an internal
        ///    product.
        /// 2. Get the sorted list of versions for a specific package that exist
        ///    within the set allowed by the feeds.
        /// 3. Narrow that list down to those that have a specified architecture
        /// 4. Narrow the list down further based on a VC Toolset being one of
        ///    the toolsets supported by the package. This is an illustration of
        ///    needing the nuspec XML to make a decision, rather than just the
        ///    JSON-LD metadata.
        /// 5. Download the nupkg file for the package.
        /// 6. Unzip the nupkg and discover the nuspec files within it.
        ///    (When allowing multiple nuspecs to exist within the package).
        /// </summary>
        /// <param name="repositories"></param>
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

        /// <summary>
        /// Get the first feed definition matching the id/version specified from the
        /// configured repositories.
        /// </summary>
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

        /// <summary>
        /// Using the specified feeds as a filter of possible matches, get the versions of a package
        /// that exist within the configured repositories.  Sort the results based on the version with
        /// the newest version first.
        /// </summary>
        /// <remarks>
        /// Note that build metadata cannot be used in the order by here because build metadata cannot be sorted.
        /// </remarks>
        public IEnumerable<IPackageMetadata> GetSortedPackageVersionsWithinFeeds(string packageId, IEnumerable<IFeed> feeds)
        {
            IEnumerable<IFeedPackage> packageVersionFilter = feeds.SelectMany(f => f.Packages
                .Where(p => String.Compare(p.PackageId, packageId, StringComparison.OrdinalIgnoreCase) == 0));

            IEnumerable<IPackageMetadata> versionsAvailable = _metadataClient.GetPackageVersions(packageId);

            return versionsAvailable
                .Where(v => packageVersionFilter.Any(f => SemanticVersion.Compare(f.PackageVersion, v.Version, SemanticVersionComparison.Exact) == 0))
                .OrderByDescending(m => m.Version, SemanticVersionComparer.MajorMinorPatchPrerelease);
        }

        /// <summary>
        /// Filter the packages found to match a specified architecture.  This information is available
        /// right on the metadata service's output since it's just a scalar value.
        /// </summary>
        public IEnumerable<IPackageMetadata> FilterToArchitecture(IEnumerable<IPackageMetadata> packages, string architecture)
        {
            // Simple scalar properties are available directly from the metadata returned
            // from the metadata service.  But complex properties are not.
            return packages.Where(m => m.GetMetadataProperty<string>("architecture") == architecture);
        }

        /// <summary>
        /// Filter the packages down to those that are compatible with a specified VS Toolset.  This might
        /// require getting the nuspec file (served directly from the document service without downloading the nupkg).
        /// </summary>
        public IEnumerable<IPackageMetadata> FilterToVCToolset (IEnumerable<IPackageMetadata> packages, string vcToolset)
        {
            foreach (IPackageMetadata package in packages)
            {
                // If the IPackageMetadata includes a link to the package's nuspec file
                // that link will be followed to get the nuspec file and load it as an
                // XDocument.  If the IPackageMetadata does not include a link to the
                // nuspec file, then the Id/Version of the package will be used to fetch
                // the nuspec file from the _documentClient from one of the repositories.
                XDocument nuspec = _documentClient.GetNuspec(package);

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

        /// <summary>
        /// Download the nupkg files that the discovered packages were found in.
        /// </summary>
        public IEnumerable<INupkg> DownloadNupkgs(IEnumerable<IPackageMetadata> packages)
        {
            foreach (IPackageMetadata package in packages)
            {
                yield return _downloadClient.GetNupkg(package);
            }
        }

        /// <summary>
        /// From within the downloaded nupkg files, disover the nuspec files within them
        /// and illustrate that we can now round-trip back to IPackageMetadata objects off
        /// the local nupkg files.
        /// </summary>
        public IEnumerable<IPackageMetadata> GetNuspecs(IEnumerable<INupkg> packages)
        {
            foreach (INupkg package in packages)
            {
                foreach (IPackageMetadata metadata in package.MetadataDefinitions)
                {
                    yield return metadata;
                }
            }
        }
    }
}
