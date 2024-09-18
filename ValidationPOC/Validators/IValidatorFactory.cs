using FluentValidation;

namespace ValidationPOC.Validators;

public interface IValidatorFactory<TParameters>
{
    Task<IValidator> CreateValidatorAsync(TParameters parameters);
}