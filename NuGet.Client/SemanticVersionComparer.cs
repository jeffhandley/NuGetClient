using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Client
{
    public enum SemanticVersionComparison
    {
        Major,
        MajorMinor,
        MajorMinorPatch,
        MajorMinorPatchPrerelease,
        Exact = 100
    }

    public abstract class SemanticVersionComparer : IComparer<SemanticVersion>, IEqualityComparer<SemanticVersion>, IComparer, IEqualityComparer
    {
        public static SemanticVersionComparer MajorMinorPatchPrerelease { get; set; }
        public static SemanticVersionComparer Exact { get; set; }

        public int Compare(SemanticVersion x, SemanticVersion y)
        {
            throw new NotImplementedException();
        }

        public bool Equals(SemanticVersion x, SemanticVersion y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(SemanticVersion obj)
        {
            throw new NotImplementedException();
        }

        public int Compare(object x, object y)
        {
            throw new NotImplementedException();
        }

        public bool Equals(object x, object y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
