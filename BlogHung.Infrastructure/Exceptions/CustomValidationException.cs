using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogHung.Infrastructure.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomValidationException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="failures"></param>
        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
