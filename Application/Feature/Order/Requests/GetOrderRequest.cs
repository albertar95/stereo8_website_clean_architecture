using Application.DTO.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Order.Requests
{
    public class GetOrderRequest : IRequest<OrderDto>
    {
        public Guid Id { get; set; }
    }
}
