using System;
using System.Collections.Generic;

namespace GasWebMap.Services.Responses
{
    public abstract class DeleteOperation<T> : List<T>
    {
    }

    public abstract class DeleteOperation : DeleteOperation<Guid?>
    {
    }

    public abstract class DeleteOneOperation
    {
        public Guid Id { get; set; }
    }
}