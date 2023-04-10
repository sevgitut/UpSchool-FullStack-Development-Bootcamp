using Application.Common.Interfaces;
using Application.Features.Addresses.Command.Add;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Command.Delete
{
    public class AddressDeleteCommandHandler : IRequestHandler<AddressDeleteCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(AddressDeleteCommand request, CancellationToken cancellationToken)
        {
            var address = await _applicationDbContext.Addresses.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (address == null) { return new Response<int>($"The addressId of {request.Id} can not be found"); }

            _applicationDbContext.Addresses.Remove(address);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<int>($"The address named \"{address.Name}\" has been succesfully deleted.", address.Id);
        }
    }
}