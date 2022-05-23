using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FeedDataLibrary.Models
{
    public class Feed
    {
        //[BindNever]
        public int FeedID { get; set; }

        [MaxLength(255)]
        public string SourceUri { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }

        [MaxLength(255)]
        public string? Title { get; set; }

        [MaxLength(255)]
        public string? Link { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        [MaxLength(20)]
        public string? Language { get; set; }

        //ToDO: Change datetime datatype to date
        public DateTime? DatePublishedLast { get; set; }

        public DateTime DateAdded { get; set; }

        public ICollection<FeedItem>? FeedItems { get; set; }

        public Feed(string sourceUri)
        {
            SourceUri = sourceUri;
            DateAdded = DateTime.Now;
        }

        public Feed(string sourceUri, string? note)
        {
            SourceUri = sourceUri;
            DateAdded = DateTime.Now;
            Note = note;
        }
    }
}
