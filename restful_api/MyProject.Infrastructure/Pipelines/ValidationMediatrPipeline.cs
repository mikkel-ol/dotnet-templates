using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace MyProject.Infrastructure.Pipelines
{
    public class ValidationMediatrPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> validator;

        public ValidationMediatrPipeline(IValidator<TRequest> validator = null)
        {
            this.validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (validator == null) return await next();

            var result = await validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid) throw new ValidationException(result.Errors);

            return await next();
        }
    }
}
