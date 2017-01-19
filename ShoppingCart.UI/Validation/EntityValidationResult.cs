using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.UI.Validation
{
    [Serializable]
    public class EntityValidationResult
    {
        public IList<ValidationResult> ValidationErrors { get; }

        public bool HasError => ValidationErrors.Count > 0;

        public EntityValidationResult(IList<ValidationResult> violations = null)
        {
            ValidationErrors = violations ?? new List<ValidationResult>();
        }
    }
}
