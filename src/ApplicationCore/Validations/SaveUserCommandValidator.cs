using FluentValidation;
using Vimo.ApplicationCore.Services.UserServices;

namespace Vimo.ApplicationCore.Validations
{
    public class SaveUserCommandValidator : AbstractValidator<SaveUserCommand>
    {
        public SaveUserCommandValidator()
        {
            RuleFor(command => command.Name)
                .Must(MustBeFill)
                .WithMessage("Ad bilgisi zorunlu bir alandır.");

            RuleFor(command => command.Username)
                .Must(MustBeFill)
                .WithMessage("Kullanıcı adı bilgisi zorunlu bir alandır.");

            RuleFor(command => command.Email)
                .Must(MustBeFill)
                .WithMessage("E-posta bilgisi zorunlu bir alandır.");

            RuleFor(command => command.Email)
                .EmailAddress()
                .WithMessage("E-Posta formatına uygun olmalıdır.");
        }

        private bool MustBeNew(SaveUserCommand command)
        {
            return command.Id == default;
        }

        private bool MustBeFill(string field)
        {
            return !string.IsNullOrEmpty(field);
        }
    }
}