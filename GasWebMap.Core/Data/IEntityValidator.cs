using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GasWebMap.Core.Data
{
    public interface IEntityValidator<T, Tid> where T : IEntityBase<Tid>
    {
        IEnumerable<ValidationResult> Validate(T entity);
    }

    public interface IEntityValidator<T> : IEntityValidator<T, Guid> where T : IEntityBase
    {
    }
}