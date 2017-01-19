using ShoppingCart.UI.Validation.Interfaces;

namespace ShoppingCart.UI.Validation
{
    public class DataAnnotation : IDataAnnotation
    {
        public EntityValidationResult ValidateEntity<T>(T entity) where T : IEntity
        {
            return new EntityValidator<T>().Validate(entity);
        }
    }
}