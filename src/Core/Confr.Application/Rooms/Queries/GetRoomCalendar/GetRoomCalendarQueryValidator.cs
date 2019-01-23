using FluentValidation;

namespace Confr.Application.Rooms.Queries.GetRoomCalendar
{
    public class GetRoomCalendarQueryValidator : AbstractValidator<GetRoomCalendarQuery>
    {
        public GetRoomCalendarQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
