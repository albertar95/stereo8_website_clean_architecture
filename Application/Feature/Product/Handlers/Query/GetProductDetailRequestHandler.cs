using Application.DTO.Product;
using Application.Feature.Product.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Product.Handlers.Query
{
    public class GetProductDetailRequestHandler : IRequestHandler<GetProductDetailRequest,DetailProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductDetailRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<DetailProductDto> Handle(GetProductDetailRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<DetailProductDto>(await _productRepository.GetProduct(request.Id));
        }
    }
}
