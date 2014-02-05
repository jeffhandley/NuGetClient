using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Client
{
    public class ResolutionContext
    {
        public ResolutionContext()
        {
            PackageMetadataClients = new List<PackageMetadataClient>();
            AllowedPackageIdentities = new HashSet<IPackageIdentity>();
        }

        public IList<PackageMetadataClient> PackageMetadataClients { get; private set; }

        public ISet<IPackageIdentity> AllowedPackageIdentities { get; private set; }

        public IPackageMetadata GetMetadata(string packageId, SemanticVersion version)
        {
            foreach (PackageMetadataClient client in PackageMetadataClients)
            {
                IPackageMetadata metadata = client.GetPackageMetadata(packageId, version);

                if (metadata != null)
                {
                    if (AllowedPackageIdentities.Any())
                    {
                        return metadata.FilterToAllowedPackageIdentities(AllowedPackageIdentities);
                    }

                    return metadata;
                }
            }

            return null;
        }
    }
}
