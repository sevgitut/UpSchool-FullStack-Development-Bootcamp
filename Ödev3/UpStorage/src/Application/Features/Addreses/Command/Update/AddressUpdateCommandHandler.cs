using Application.Common.Interfaces;
using Application.Features.Addresses.Command.Delete;
using Domain.Common;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Command.Update
{
    public class AddressUpdateCommandHandler : IRequestHandler<AddressUpdateCommand, Response<int>>
    {

        private readonly IApplicationDbContext _applicationDbContext;
        public AddressUpdateCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<Response<int>> Handle(AddressUpdateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}