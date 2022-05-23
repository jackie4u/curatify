using FeedDataLibrary.Models;

namespace DimePubWeb.Models.ViewModels
{
    public class Podcasts
    {
        public IEnumerable<Feed> PodcastsList { get; set; }
            = Enumerable.Empty<Feed>();

        public Podcasts(IQueryable<Feed> podcastsList, string searchString)
        {
            if ((!String.IsNullOrEmpty(searchString)) && (podcastsList.Any()))
            {
                podcastsList = podcastsList.Where(s => s.Title.Contains(searchString));
            }
            PodcastsList = podcastsList.ToList();
        }
    }
}
