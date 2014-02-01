using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Client
{
    public abstract class SemanticVersionComparer : IComparer<SemanticVersion>, IEqualityComparer<SemanticVersion>, IComparer, IEqualityComparer
    {
        public static SemanticVersionComparer Strict { get; }
    }
}
