using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Command.HardDelete
{
    public class AddressHardDeleteCommand : IRequest<Response<int>>
    {
        public string Id { get; set; }
    }
}