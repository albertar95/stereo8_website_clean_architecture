using Application.DTO.Cart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Cart.Requests
{
    public class UpdateCartRequest : IRequest<bool>
    {
        public UpdateCartDto Cart { get; set; }
    }
}
