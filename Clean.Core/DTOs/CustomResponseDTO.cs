using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Clean.Core.DTOs
{
    public class CustomResponseDTO<T>
    {
        public T? Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<String>? Errors { get; set; }

        public static CustomResponseDTO<T> Success(int statusCode, T data)
        {
            return new CustomResponseDTO<T> { Data = data, StatusCode = statusCode };
        }
        public static CustomResponseDTO<T> Success(int statusCode)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode };
        }
        public static CustomResponseDTO<T> Fail(int statusCode, List<String> errors)
        {
            return new CustomResponseDTO<T> { Errors = errors, StatusCode = statusCode };
        }
        public static CustomResponseDTO<T> Fail(int statusCode, string errors)
        {
            return new CustomResponseDTO<T> { Errors = new List<string> { errors }, StatusCode = statusCode };
        }

    }
}
