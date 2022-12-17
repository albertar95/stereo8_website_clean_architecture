using Application.Feature.Product.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Product.Handlers.Command
{
    public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest,bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeleteProductRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var tmpProduct = await _productRepository.GetProduct(request.Id);
            if (tmpProduct == null)
                return false;
            else
            {
                tmpProduct.State = 1;
                var product = _mapper.Map<Domain.Product>(tmpProduct);
                return await _productRepository.UpdateProduct(product);
            }
        }
    }
}
