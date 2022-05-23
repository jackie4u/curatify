using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedDataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FeedDataLibrary.Services
{
    public class FeedDataContext : DbContext
    {
        public FeedDataContext(DbContextOptions<FeedDataContext> options) : base(options) { }

        public DbSet<Feed> Feeds { get; set; }

        public DbSet<FeedItem> FeedItems { get; set; }
    }
}