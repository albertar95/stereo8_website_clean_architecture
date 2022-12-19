using Application.DTO.Category;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Requests
{
    public class GetCategoryRequest : IRequest<Domain.Category>
    {
        public Guid Id { get; set; }
    }
}
