using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public interface IFeed
    {
        string FeedId { get; set; }
        SemanticVersion Version { get; set; }
        IEnumerable<IFeedPackage> Packages { get; set; }
    }
}
