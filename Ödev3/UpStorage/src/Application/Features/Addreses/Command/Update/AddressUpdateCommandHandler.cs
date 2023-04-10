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

        public async Task<Response<int>> Handle(AddressUpdateCommand request, CancellationToken cancellationToken)
        {
            var address = _applicationDbContext.Addresses.FirstOrDefault(x => x.Name == request.Name);

            address.Name = request.Name;
            address.UserId = request.UserId;
            address.CountryId = request.CountryId;
            address.CityId = request.CityId;
            address.District = request.District;
            address.PostCode = request.PostCode;
            address.AddressLine1 = request.AddressLine1;
            address.AddressLine2 = request.AddressLine2;
            address.CreatedOn = DateTimeOffset.Now;
            address.CreatedByUserId = request.UserId;
            address.IsDeleted = false;


            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<int>($"The address named \"{address.Name}\" was successfully updated.", address.Id);
        }
    }

}