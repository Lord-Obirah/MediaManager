using System;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.Entities
{
    public class MovieWriteApi //: IValidatableObject
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid MediaTypeId { get; set; }

        ///// <inheritdoc />
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    throw new NotImplementedException();
        //}
    }
}