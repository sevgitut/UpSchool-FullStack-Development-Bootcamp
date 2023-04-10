using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Queries.GetAll
{
    public class AddressGetAllQuery : IRequest<List<AddressGetAllDto>>
    {
        public int UserId { get; set; }
        public bool? IsDeleted { get; set; }

        public AddressGetAllQuery(int userId, bool? isDeleted)
        {

            UserId = userId;
            IsDeleted = isDeleted;
        }

    }
}