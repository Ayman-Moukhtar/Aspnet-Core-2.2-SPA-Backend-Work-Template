using System.Collections.Generic;

namespace WorkTemplate.Croscutting.Contracts.CustomExceptions
{
    public class ServiceValidationErrorsException
        : UserErrorException
    {
        #region Properties

        public ICollection<string> ValidationErrors { get; }

        #endregion

        #region Constructor

        public ServiceValidationErrorsException(ICollection<string> validationErrors)
            : base("Model is invalid")
        {
            ValidationErrors = validationErrors;
        }
        #endregion
    }
}