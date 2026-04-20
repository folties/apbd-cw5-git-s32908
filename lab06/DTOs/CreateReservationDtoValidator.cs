using FluentValidation;

namespace lab06.DTOs;

public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationDtoValidator()
    {
        RuleFor(x=> x.EndTime).GreaterThan(x => x.StartTime)
            .WithMessage("EndTime must be later than StartTime.");
    }
}