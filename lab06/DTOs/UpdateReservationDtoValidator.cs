using FluentValidation;

namespace lab06.DTOs;

public class UpdateReservationDtoValidator : AbstractValidator<UpdateReservationDto>
{
    public UpdateReservationDtoValidator()
    {
        RuleFor(x=> x.EndTime).GreaterThan(x => x.StartTime)
            .WithMessage("EndTime must be later than StartTime.");
    }
}