﻿using Clinica.Application.UseCase.Commons.Bases;

namespace Clinica.Application.UseCase.Commons.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<BaseError>? Errors { get;}

        public ValidationException(): base() 
        {
            Errors = new List<BaseError>();
        }

        public ValidationException(IEnumerable<BaseError>? errors): this()
        {
            Errors = errors;
        }
    }
}
