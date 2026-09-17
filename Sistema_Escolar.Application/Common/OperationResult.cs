using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Common
{
    public class OperationResult 
    {
        public bool Success { get; }
        public string Message { get; }
        public object? Data { get; set; }

        private OperationResult(bool success, string message, object? data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static OperationResult Ok(object data, string message = "Operacion exitosa ")
            => new(true, message, data);

        public static OperationResult Ok(string message)
        => new(true, message, null);

        public static OperationResult Fail(string message)
            => new(false, message);


    }

    public class OperationResult<T>
    {
        public bool Success { get; }
        public string Message { get; }
        public T? Data { get; }

        private OperationResult(bool success, string message, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static OperationResult<T> Ok(T data, string message = "Operación exitosa")
            => new(true, message, data);

        public static OperationResult<T> Fail(string message)
            => new(false, message, default);
    }
}
