using FeedDataLibrary.Models;

namespace DimePubWeb.Models.ViewModels
{
    public class Episodes
    {
        public IEnumerable<FeedItem> EpisodesList { get; set; }
            = Enumerable.Empty<FeedItem>();

        public PagingInfo PagingInfo { get; set; } = new();
    }
}