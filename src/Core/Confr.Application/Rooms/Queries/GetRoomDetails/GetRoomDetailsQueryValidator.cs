using FluentValidation;

namespace Confr.Application.Rooms.Queries.GetRoomDetails
{
    public class GetRoomDetailsQueryValidator : AbstractValidator<GetRoomDetailsQuery>
    {
        public GetRoomDetailsQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
