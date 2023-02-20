using System.Collections.Generic;

namespace Seje.OrdenCaptura.SharedKernel.Results
{
    public class Result : IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Reasons { get; set; } = new Dictionary<string, string>();

        public Result()
        {

        }
        public Result(bool success, string reason = null)
        {
            this.Success = success;
            this.Message = reason;
        }

        public static Result Ok()
        {
            return new Result(true, null);
        }
        public static Result Failure(string message)
        {
            return new Result(false, message);
        }
        public static NotFoundResult NotFound(string message)
        {
            return new NotFoundResult(message);
        }
        public static NotFoundResult NotFound()
        {
            return new NotFoundResult(null);
        }


    }
    public class SuccessResult : Result
    {
        public SuccessResult() : base(true, null)
        {

        }
        public static SuccessResult Create()
        {
            return new SuccessResult();
        }
    }

    public class FailureResult : Result
    {
        public FailureResult()
        {

        }
        public FailureResult(string reason) : base(false, reason)
        {

        }
        public static FailureResult Create(string reason)
        {
            return new FailureResult(reason);
        }
        public static FailureResult Create(string reason, Dictionary<string, string> reasons)
        {
            return new FailureResult(reason) { Reasons = reasons };
        }
    }
    public class NotFoundResult : Result
    {

        private static string _defaultMessage = "No se encontró el recurso especificado";
        public NotFoundResult(string message) : base(false, string.IsNullOrWhiteSpace(message) ? _defaultMessage : message)
        {

        }
    }
    public class Result<T> : Result, IResult<T>
    {
        public T Entity { get; set; }
        public Result(bool success, string reason = null, T entity = default(T)) : base(success, reason)
        {
            this.Entity = entity;
        }
        public static Result<T> Ok(T Entity)
        {
            return new Result<T>(true, null, Entity);
        }
        public static new Result<T> Failure(string message)
        {
            return new Result<T>(false, message);
        }
        public static new NotFoundResult<T> NotFound(string message)
        {
            return new NotFoundResult<T>(message);
        }
        public static new NotFoundResult<T> NotFound()
        {
            return new NotFoundResult<T>(null);
        }

    }
    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult() : base(true)
        {

        }
        public static SuccessResult<T> Create()
        {
            return new SuccessResult<T>();
        }
    }
    public class FailureResult<T> : Result<T>
    {
        public FailureResult() : base(false)
        {

        }
        public FailureResult(string reason) : base(false, reason)
        {

        }
        public static FailureResult<T> Create(string reason)
        {
            return new FailureResult<T>(reason);
        }
        public static FailureResult<T> Create(string reason, Dictionary<string, string> reasons)
        {
            return new FailureResult<T>(reason) { Reasons = reasons };
        }
    }
    public class NotFoundResult<T> : Result<T>
    {

        private static string _defaultMessage = "No se encontró el recurso especificado";
        public NotFoundResult(string message) : base(false, string.IsNullOrWhiteSpace(message) ? _defaultMessage : message)
        {

        }
    }
}
