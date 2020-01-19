using BandsApi.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace BandsApi.Models
{
    [TitleAndDescription]
    public abstract class AlbumManipulationDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(400)]
        public virtual string Description { get; set; }
    }
}
