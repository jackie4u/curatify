using System.ComponentModel.DataAnnotations;

namespace DimePubWeb.Models.ViewModels
{
    public class PodcastAdd
    {
        [Required(ErrorMessage = "Enter Source URL of the Feed")]
        [MaxLength(255)]
        public string? SourceUri { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }
    }
}
