using MementoFX.Domain;
using System;

namespace Seje.OrdenCaptura.SharedKernel
{
    public static class AggregateExtensions
    {
        public static bool IsEmpty(this Aggregate aggregate)
        {
            return aggregate.Id == Guid.Empty;
        }
    }
}
