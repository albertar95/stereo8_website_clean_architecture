using Application.DTO.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Requests
{
    public class UpdateCategoryRequest : IRequest<bool>
    {
        public UpdateCategoryDto category { get; set; } = null!;
    }
}
