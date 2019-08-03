using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WorkTemplate.Croscutting.Contracts.CustomExceptions;
using WorkTemplate.Croscutting.Contracts.EntityValidator;

namespace WorkTemplate.Croscutting.Implementation.EntityValidator
{
    public class DataAnnotationsEntityValidator
        : IEntityValidator
    {
        #region Private Methods

        private void SetValidatableObjectErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            if (typeof(IValidatableObject).IsAssignableFrom(typeof(TEntity)))
            {
                var validationContext = new ValidationContext(item, null, null);

                var validationResults = ((IValidatableObject)item).Validate(validationContext);

                errors.AddRange(validationResults.Select(vr => vr.ErrorMessage));
            }
        }

        private void SetValidationAttributeErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            //TypeDescriptor.AddProvider(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(TEntity)),
            //    typeof(TEntity));
            var result =
                TypeDescriptor.GetProperties(item)
                    .Cast<PropertyDescriptor>()
                    .SelectMany(property => property.Attributes.OfType<ValidationAttribute>(),
                        (property, attribute) => new { property, attribute }).ToList();

            foreach (var descriptor in result)
            {
                var value = descriptor.property.GetValue(item);
                string errorMessage = string.Empty;
                if (descriptor.attribute.RequiresValidationContext)
                {
                    errorMessage = descriptor.attribute.GetValidationResult(value, new ValidationContext(item))?.ErrorMessage;
                }
                else if (!descriptor.attribute.IsValid(value))
                {
                    errorMessage = descriptor.attribute.FormatErrorMessage(descriptor.property.DisplayName);
                }

                if (!string.IsNullOrWhiteSpace(errorMessage))
                    errors.Add(errorMessage);
            }
        }

        #endregion

        #region IEntityValidator Members

        public bool IsValid<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
                return false;

            var validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);
            SetValidationAttributeErrors(item, validationErrors);

            return !validationErrors.Any();
        }

        public ICollection<string> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
                return null;

            var validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);
            SetValidationAttributeErrors(item, validationErrors);

            return validationErrors;
        }

        public void ValidateAndThrow<TEntity>(TEntity item) where TEntity : class
        {
            if (IsValid(item)) return;

            throw new ServiceValidationErrorsException(GetInvalidMessages(item));
        }
        #endregion
    }
}