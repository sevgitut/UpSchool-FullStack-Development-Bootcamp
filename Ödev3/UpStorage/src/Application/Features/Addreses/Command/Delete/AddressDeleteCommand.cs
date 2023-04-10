﻿using System;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Command.Delete
{
    public class AddressDeleteCommand : IRequest<Response<int>>
    {
        public Guid Id { get; set; }
    }
}