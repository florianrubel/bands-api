using BandsApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BandsApi.ValidationAttributes
{
    public class TitleAndDescription : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            System.Type type = validationContext.ObjectInstance.GetType();
            System.Type createEnumerableType = typeof(List<AlbumForCreatingDto>);
            System.Type updateEnumerableType = typeof(List<AlbumForUpdatingDto>);

            if (type == createEnumerableType || type == updateEnumerableType)
            {
                List<string> errors = new List<string>();
                IEnumerable<AlbumManipulationDto> albums = (IEnumerable<AlbumManipulationDto>)validationContext.ObjectInstance;
                foreach (AlbumManipulationDto album in albums)
                {
                    if (album.Title == album.Description)
                    {
                        errors.Add(nameof(AlbumManipulationDto));
                        errors.Add("title");
                        errors.Add("description");
                    }
                }
                if (errors.Count > 0)
                {
                    return new ValidationResult(
                       "The title and the description need to be different.",
                       errors
                   );
                }
            }
            else
            {
                AlbumManipulationDto album = (AlbumManipulationDto)validationContext.ObjectInstance;

                if (album.Title == album.Description)
                {
                    return new ValidationResult(
                        "The title and the description need to be different.",
                        new[] { nameof(AlbumManipulationDto), "title", "description" }
                    );
                }
            }

            return ValidationResult.Success;
        }
    }
}
