using System.ComponentModel.DataAnnotations;

namespace MediaManager.Entities
{
    public class MediaTypeWriteApi// : IValidatableObject

    {
        [Required]
        public string Name { get; set; }

        ///// <inheritdoc />
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{

        //}
    }
}