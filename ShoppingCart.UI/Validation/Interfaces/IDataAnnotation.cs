namespace ShoppingCart.UI.Validation.Interfaces
{
    public interface IDataAnnotation
    {
        EntityValidationResult ValidateEntity<T>(T entity) where T : IEntity;
    }
}