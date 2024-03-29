﻿using Application.Feature.Product.Requests;
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
    public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest,bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProduct(request.product.Id);
            if (product == null) return false;
            else
            {
                _mapper.Map(request.product, product);
                return await _productRepository.Update(product);
            }
        }
    }
}
