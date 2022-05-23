using FeedDataLibrary.Models;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedDataLibrary.Services
{
    public static class FeedParser
    {
        public static bool CheckSourceUrl(string url)
        {
            try
            {
                // First check if the URL is properly formatted
                bool isFormatted = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (!isFormatted)
                    return false;

                // Then check if the URL endpoint exists
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(url, null);
                    response.Wait();
                    if (response.IsCompletedSuccessfully)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static Feed GetFeed(string url, string? note)
        {
            try
            {
                Feed feed = new(url);

                XDocument doc = XDocument.Load(url);
                var c = from channel in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements() select channel;
                // RSS/Channel
                feed.Title = c.First(i => i.Name.LocalName == "title").Value;
                feed.Link = c.First(i => i.Name.LocalName == "link").Value;
                feed.Description = c.First(i => i.Name.LocalName == "description").Value;
                feed.Language = c.First(i => i.Name.LocalName == "language").Value;

                feed.SourceUri = url;
                feed.Note = note;

                // Handle dates
                try
                {
                    feed.DatePublishedLast = ParseDate(c.First(i => i.Name.LocalName == "pubDate").Value);
                }
                catch
                {
                    feed.DatePublishedLast = DateTime.Now;
                }
                return feed;
            }
            catch
            {
                //ToDo: Improve error handling
                throw;
            }
        }

        public static void RefreshFeedItems(Feed feed)
        {
            try
            {
                XDocument doc = XDocument.Load(feed.SourceUri);

                // Refresh the metadata of the Feed
                var c = from channel in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements() select channel;
                feed.Title = c.First(i => i.Name.LocalName == "title").Value;
                feed.Link = c.First(i => i.Name.LocalName == "link").Value;
                feed.Description = c.First(i => i.Name.LocalName == "description").Value;
                feed.Language = c.First(i => i.Name.LocalName == "language").Value;

                // Handle dates
                try
                {
                    feed.DatePublishedLast = ParseDate(c.First(i => i.Name.LocalName == "pubDate").Value);
                }
                catch
                {
                    feed.DatePublishedLast = DateTime.Now;
                }

                // RSS/Channel/FeedItem
                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                              select new FeedItem
                              {
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                                  Description = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  DatePublished = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value)
                              };
                if (entries.Any())
                    feed.FeedItems = entries.ToList();
            }
            //ToDo: Improve error handling
            catch (InvalidOperationException)
            {
                //In case of issue in parsing items do nothing
                return;
            }
            catch
            {
                throw;
            }
        }

        private static DateTime ParseDate(string date)
        {
            if (DateTime.TryParse(date, out DateTime result))
                return result;
            else
                return DateTime.Today;
        }
    }
}
