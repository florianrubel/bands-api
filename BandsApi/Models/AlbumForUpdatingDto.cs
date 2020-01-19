using BandsApi.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace BandsApi.Models
{
    [TitleAndDescription]
    public class AlbumForUpdatingDto : AlbumManipulationDto
    {
        [Required]
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}
