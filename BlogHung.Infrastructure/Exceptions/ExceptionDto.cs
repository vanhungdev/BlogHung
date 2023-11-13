using BlogHung.Infrastructure.Exceptions;
using System;

namespace BlogHung.Infrastructure.Exceptions
{
    public class ExceptionDto
    { 
        public ExceptionDto(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.StackTrace;
        }

        public ExceptionDto(CustomValidationException ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.StackTrace;
        }

        public string Type { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
