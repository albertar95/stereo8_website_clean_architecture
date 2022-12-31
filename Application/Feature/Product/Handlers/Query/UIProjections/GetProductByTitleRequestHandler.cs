using Application.DTO.Product;
using Application.Feature.Product.Requests.UIProjections;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Product.Handlers.Query.UIProjections
{
    public class GetProductByTitleRequestHandler : IRequestHandler<GetProductByTitleRequest, DetailProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByTitleRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<DetailProductDto> Handle(GetProductByTitleRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<DetailProductDto>(await _productRepository.GetProductByTitle(request.Title, request.IncludeAll));
        }
    }
}
