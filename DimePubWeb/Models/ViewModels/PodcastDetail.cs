using FeedDataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace DimePubWeb.Models.ViewModels
{
    public class PodcastDetail
    {
        public Feed Podcast { get; set; }

        public PodcastDetail(Feed podcast)
        {
            Podcast = podcast;
        }
    }
}
