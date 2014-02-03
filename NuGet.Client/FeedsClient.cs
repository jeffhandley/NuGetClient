using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public class FeedsClient
    {
        public FeedsClient(Configuration.Repositories repositories)
        {

        }

        internal IEnumerable<IFeed> GetFeedVersions(string feedId)
        {
            throw new NotImplementedException();
        }
    }

    public static class FeedsClientExtensions
    {
        public static FeedsClient CreateFeedsClient(this Clients host, Configuration.Repositories repositories)
        {
            return new FeedsClient(repositories);
        }
    }
}
