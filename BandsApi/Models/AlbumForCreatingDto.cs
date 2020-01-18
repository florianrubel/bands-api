using BandsApi.ValidationAttributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BandsApi.Models
{
    [TitleAndDescription(ErrorMessage = "Title must be different from description")]
    public class AlbumForCreatingDto // : IValidatableObject
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult(
        //            "The title and description needs to be different.",
        //            new[] { nameof(AlbumForCreatingDto) }
        //        );
        //    }
        //}
    }
}
