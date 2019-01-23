using FluentValidation;

namespace Confr.Application.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
