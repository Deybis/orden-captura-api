using System.Collections.Generic;

namespace Seje.OrdenCaptura.SharedKernel.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; set; }
        Dictionary<string, string> Reasons { get; }
    }
    public interface IResult<T> : IResult
    {
        T Entity { get; set; }
    }
}
