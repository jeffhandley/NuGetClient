﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public class PackageMetadataClient
    {
        public PackageMetadataClient(Configuration.Repositories repositories)
        {

        }

        internal IEnumerable<IPackageMetadata> GetPackageVersions(string packageId)
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
