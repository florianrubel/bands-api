using BandsApi.Models;
using System.ComponentModel.DataAnnotations;

namespace BandsApi.ValidationAttributes
{
    public class TitleAndDescription : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            AlbumForCreatingDto album = (AlbumForCreatingDto)validationContext.ObjectInstance;

            if(album.Title == album.Description)
            {
                return new ValidationResult(
                    "The title and the description need to be different.",
                    new[] { nameof(AlbumForCreatingDto) }
                );
            }

            return ValidationResult.Success;
        }
    }
}
