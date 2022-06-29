﻿using Business.Commons;
using FluentValidation;
using MediatR;
using System.Net;

namespace Business.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            if (failures.Count == 0) return await next();

            var errors = new Dictionary<string, string>();
            foreach (var error in failures)
                errors.Add(string.IsNullOrEmpty(error.PropertyName) ? "Error" : error.PropertyName, error.ErrorMessage);

            throw new ResponseException((int)HttpStatusCode.BadRequest, "Validation errors occurred", errors);
        }
    }
}
