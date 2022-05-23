using FeedDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedDataLibrary.Services
{
    public interface IFeedRepository
    {
        IQueryable<Feed> Feeds { get; }

        IQueryable<FeedItem> FeedItems { get; }

        Feed GetFeedById(int feedID);

        int AddFeed(string sourceUri, string? note);

        void RefreshAllFeeds();

        void RefreshFeed(int feedID);

        void RefreshFeed(Feed feed);

        bool DeleteFeed(int feedID);
    }
}
