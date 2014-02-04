using NuGet.Client.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Client
{
    public class NuGetExe
    {
        public NuGetExe()
        {

        }

        public void InstallCommand(Repositories repositories, string targetPath, string id, SemanticVersion version = null, PackageInventory localInventory = null)
        {
            /// 1. Resolve the dependency graph
            ///     a. Get metadata for id (all versions if null, or specific version if specified),
            ///        including all first-level dependency statements
            ///     b. Get metadata for all first-level dependency packages (not yet in local inventory)
            ///        for all versions of id.  Repeat on these packages' dependencies until no deeper.
            ///     c. Resolve the graph of dependencies to one where no conflicts exist with local inventory.
            /// 2. Get all missing packages
            ///     a. Download and store as zip files in the targetPath
            ///     b. Unzip the packages into subfolders within targetPath
            ///         * Note that there will be callers that need to 
            /// 3. Update local inventory object

            DependencyResolution resolution = DependencyResolver.Resolve(repositories, id, version, localInventory);

            // This foreach should be parallel
            foreach (IPackageIdentity package in resolution.Installs)
            {
                string packagePath = Path.Combine(targetPath, package.Id, package.Version.ToString());
                DownloadAndUnzipSinglePackage(repositories, packagePath, package);
            }

            UpdateInventory(localInventory, resolution);
        }

        private static void UpdateInventory(PackageInventory localInventory, DependencyResolution resolution)
        {
            foreach (IPackageIdentity uninstall in resolution.Uninstalls)
            {
                Logger.LogPackageUninstalled(uninstall);
                localInventory.Remove(uninstall);
            }

            foreach (IPackageIdentity install in resolution.Installs)
            {
                Logger.LogPackageInstalled(install);
                localInventory.Add(install);
            }
        }

        public void DownloadAndUnzipSinglePackage(Repositories repositories, string targetPath, IPackageIdentity identity)
        {
            PackageDownloadClient downloadClient = RepositoryContext.Clients.CreatePackageDownloadClient(repositories);
            INupkg nupkg = downloadClient.GetNupkg(identity);
            nupkg.UnpackTo(targetPath);
        }
    }
}
