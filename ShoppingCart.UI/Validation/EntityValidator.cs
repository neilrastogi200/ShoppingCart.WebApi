using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShoppingCart.UI.Validation.Interfaces;

namespace ShoppingCart.UI.Validation
{
    public class EntityValidator<T> where T : IEntity
    {
        public EntityValidationResult Validate(T entity)
        {
            var validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(entity, null, null);
            var isValid = Validator.TryValidateObject(entity, vc, validationResults, true);

            return new EntityValidationResult(validationResults);
        }
    }
}