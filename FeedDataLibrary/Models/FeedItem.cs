using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FeedDataLibrary.Models
{
    public class FeedItem
    {
        //[BindNever]
        public int FeedItemID { get; set; }

        //[BindNever]
        public int FeedID { get; set; }

        //[Required]
        [MaxLength(255)]
        public string? Title { get; set; }

        //[Required]
        [MaxLength(255)]
        public string? Link { get; set; }

        public DateTime? DatePublished { get; set; }

        //[MaxLength(4000)]
        public string? Description { get; set; }

        // TODO: Extend for IsRead
        //public bool IsRead { get; set; }
    }
}