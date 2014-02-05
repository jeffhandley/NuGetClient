using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public class PackageDownloadClient
    {
        public PackageDownloadClient(Configuration.Repositories repositories)
        {

        }

        internal INupkg GetNupkg(IPackageVersionMetadata package)
        {
            throw new NotImplementedException();
        }

        internal INupkg GetNupkg(IPackageIdentity identity)
        {
            throw new NotImplementedException();
        }
    }

    public static class PackageDownloadClientExtensions
    {
        public static PackageDownloadClient CreatePackageDownloadClient(this Clients host, Configuration.Repositories repositories)
        {
            return new PackageDownloadClient(repositories);
        }
    }
}
