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
    public class GetFilteredProductsRequestHandler : IRequestHandler<GetFilteredProductsRequest, List<ProductListDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetFilteredProductsRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductListDto>> Handle(GetFilteredProductsRequest request, CancellationToken cancellationToken)
        {
            switch (request.Filters.FilterType)
            {
                case 1:
                    return _mapper.Map<List<ProductListDto>>(await _productRepository.GetProductsByCategory(request.Filters.CategoryId,request.Filters.TypeId,request.Filters.BrandId));
                case 2:
                    return _mapper.Map<List<ProductListDto>>(await _productRepository.GetProductsByCreateDate(request.Filters.FromDate,request.Filters.ToDate));
                case 3:
                    return _mapper.Map<List<ProductListDto>>(await _productRepository.GetProductsByPriceRange(request.Filters.FromPrice,request.Filters.ToPrice));
                case 4:
                    return _mapper.Map<List<ProductListDto>>(await _productRepository.GetProductsByAvailablity(request.Filters.AvailableCount));
                default:
                    return _mapper.Map<List<ProductListDto>>(await _productRepository.GetProducts());
            }
        }
    }
}
