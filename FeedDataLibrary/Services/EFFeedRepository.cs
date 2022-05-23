using FeedDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedDataLibrary.Services
{
    public class EFFeedRepository : IFeedRepository
    {
        private FeedDataContext context;

        public EFFeedRepository(FeedDataContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Feed> Feeds => context.Feeds;

        public IQueryable<FeedItem> FeedItems => context.FeedItems;

        private Feed ExtendFeed(Feed feed)
        {
            feed.FeedItems = context.FeedItems.Where(i => i.FeedID == feed.FeedID).OrderByDescending(o => o.DatePublished).ToList();
            return feed;
        }

        public Feed GetFeedById(int feedID)
        {
            return ExtendFeed(context.Feeds.First(p => p.FeedID == feedID));
        }

        public int AddFeed(string sourceUri, string? note)
        {
            // ToDo: Add validation for duplicates (by source url)
            if (!FeedParser.CheckSourceUrl(sourceUri))
                return 0; //TODO: Improve error handling
            else
            {
                Feed feed = FeedParser.GetFeed(sourceUri, note);

                if (feed != null)
                {
                    context.Feeds.Add(feed);
                    FeedParser.RefreshFeedItems(feed);
                    context.SaveChanges();
                    return feed.FeedID;
                }
                else return 0;
            }
        }

        // TODO: Reimplement with parallel execution
        // TODO: Fix bug of duplicating items
        public void RefreshAllFeeds()
        {
            foreach (var feed in context.Feeds)
            {
                FeedParser.RefreshFeedItems(feed);
            }
            context.SaveChanges();
        }

        //TODO: Reimplement with async
        public void RefreshFeed(int feedID)
        {
            RefreshFeed(GetFeedById(feedID));
        }

        public void RefreshFeed(Feed feed)
        {
            FeedParser.RefreshFeedItems(feed);
            context.SaveChanges();
        }

        /// <summary>
        /// Delete feed from the database with cascade
        /// </summary>
        /// <param name="feedTitle">output - if success and true, then return name of the removed feed otherwise return error feedTitle</param>
        /// <returns></returns>
        public bool DeleteFeed(int feedID)
        {
            Feed feed = context.Feeds.First(p => p.FeedID == feedID);
            if (feed != null)
            {
                context.Feeds.Remove(feed);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}