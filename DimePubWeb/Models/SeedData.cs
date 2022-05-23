using FeedDataLibrary.Models;
using FeedDataLibrary.Services;
using Microsoft.EntityFrameworkCore;

namespace DimePubWeb.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            FeedDataContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<FeedDataContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Feeds.Any())
            {
                context.Feeds.AddRange(
                    new Feed("https://thedotnetcorepodcast.libsyn.com/rss", ".Net Core seed data"),
                    new Feed("http://feeds.feedburner.com/theazurepodcast", "The Azure podcast")
                );
                context.SaveChanges();
            }
        }
    }
}
