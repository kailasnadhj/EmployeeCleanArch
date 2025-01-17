using System;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Application.Common.APIResponse
{
    public abstract class APIResponse<T>
    {
        public bool IsSuccess { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }
        public T Data { get; protected set; }
        public string Message { get; protected set; }

        protected APIResponse(bool isSuccess, T data, HttpStatusCode statusCode, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            StatusCode = statusCode;
            Message = message;
        }

        public static APIResponse<T> Success(T data, string message = "Request succeeded")
        {
            return new SuccessResponse<T>(data, HttpStatusCode.OK, message);
        }

        public static APIResponse<T> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ErrorResponse<T>(default, statusCode, message);
        }

        public static Task<APIResponse<T>> SuccessAsync(T data, string message = "Request succeeded")
        {
            return Task.FromResult(Success(data, message));
        }

        public static Task<APIResponse<T>> FailureAsync(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return Task.FromResult(Failure(message, statusCode));
        }
    }

    public class SuccessResponse<T> : APIResponse<T>
    {
        public SuccessResponse(T data, HttpStatusCode statusCode, string message)
            : base(true, data, statusCode, message) { }
    }

    public class ErrorResponse<T> : APIResponse<T>
    {
        public ErrorResponse(T data, HttpStatusCode statusCode, string message)
            : base(false, data, statusCode, message) { }
    }
}
