using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }

        public static ApiResponse<T> Success(T data, string? message = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }

        public static ApiResponse<T> Failure(string error)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Errors = new List<string> { error }
            };
        }

        public static ApiResponse<T> Failure(List<string> errors)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Errors = errors
            };
        }
    }
}
