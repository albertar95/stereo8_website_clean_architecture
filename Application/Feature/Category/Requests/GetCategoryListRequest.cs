using Application.DTO.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Requests
{
    public class GetCategoryListRequest : IRequest<List<CategoryListDto>>
    {
        public int State { get; set; }
    }
}
