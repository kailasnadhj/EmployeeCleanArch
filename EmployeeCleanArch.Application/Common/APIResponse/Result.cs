using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Application.Common.APIResponse
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T Data { get; private set; }
        public string Message { get; private set; }

        private Result(bool isSuccess, T data, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }

        public static Result<T> Success(T data, string message)
        {
            return new Result<T>(true, data, message);
        }

        public static Result<T> Failure(string message)
        {
            return new Result<T>(false, default(T), message);
        }

        public static Task<Result<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }

        public static Task<Result<T>> FailureAsync(string message)
        {
            return Task.FromResult(Failure(message));
        }
    }

}
