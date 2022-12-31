using Application.DTO.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Requests
{
    public class GetCategoryByTitleRequest : IRequest<CategoryDto>
    {
        public string Title { get; set; } = ""!;
        public bool IncludeProduct { get; set; } = false!;
    }
}
