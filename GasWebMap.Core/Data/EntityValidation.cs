using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GasWebMap.Core.Data
{
    public class EntityValidation<T, Tid> : IEntityValidator<T, Tid> where T : IEntityBase<Tid>
    {
        #region IEntityValidator<T,Tid> Members

        public IEnumerable<ValidationResult> Validate(T entity)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(entity, null, null);
            Validator.TryValidateObject(entity, validationContext, validationResults, true);
            return validationResults;
        }

        #endregion
    }

    public class EntityValidation<T> : EntityValidation<T, Guid> where T : IEntityBase
    {
    }
}