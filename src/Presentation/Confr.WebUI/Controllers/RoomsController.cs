using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Confr.Application.Rooms.Commands.CreateRoom;
using Confr.Application.Rooms.Commands.DeleteRoom;
using Confr.Application.Rooms.Commands.UpdateRoom;
using Confr.Application.Rooms.Queries.GetRoomCalendar;
using Confr.Application.Rooms.Queries.GetRoomDetails;
using Confr.Application.Rooms.Queries.GetRoomsList;

namespace Confr.WebUI.Controllers
{
    public class RoomsController : BaseController
    {
        // GET: api/rooms
        [HttpGet]
        public async Task<ActionResult<RoomsListViewModel>> GetRoomsListAsync()
        {
            return Ok(await Mediator.Send(new GetRoomsListQuery()));
        }

        // GET: api/rooms/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDetailsViewModel>> GetRoomDetailsAsync([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new GetRoomDetailsQuery { Id = id }));
        }

        // GET: api/rooms/{id}/calendar
        [HttpGet("{id}/calendar")]
        public async Task<ActionResult<RoomDetailsViewModel>> GetRoomCalendarAsync([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new GetRoomCalendarQuery { Id = id }));
        }

        // POST: api/rooms
        [HttpPost]
        public async Task<ActionResult<int>> CreateRoomAsync([FromBody] CreateRoomCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT: api/rooms/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateRoomAsync(
            [FromRoute] int id,
            [FromBody] UpdateRoomCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        // DELETE: api/rooms/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeteleRoomAsync([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteRoomCommand{ Id = id }));
        }
    }
}
