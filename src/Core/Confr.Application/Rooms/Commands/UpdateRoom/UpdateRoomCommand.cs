using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediatR;
using Confr.Application.Interfaces.Mapping;
using Confr.Domain.Entities;

namespace Confr.Application.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<DateTime> Calendar { get; set; }

        public UpdateRoomCommand()
        {
            Calendar = new List<DateTime>();
        }
    }
}
