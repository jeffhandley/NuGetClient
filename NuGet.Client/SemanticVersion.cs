using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public class SemanticVersion
    {
        internal static int Compare(SemanticVersion feedVersion, SemanticVersion semanticVersion, SemanticVersionComparer semanticVersionComparer)
        {
            throw new NotImplementedException();
        }

        internal static int Compare(SemanticVersion feedVersion, SemanticVersion semanticVersion, SemanticVersionComparison semanticVersionComparison)
        {
            throw new NotImplementedException();
        }
    }
}
