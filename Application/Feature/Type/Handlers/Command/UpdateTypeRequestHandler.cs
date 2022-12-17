using Application.Feature.Type.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Type.Handlers.Command
{
    public class UpdateTypeRequestHandler : IRequestHandler<UpdateTypeRequest,bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateTypeRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public Task<bool> Handle(UpdateTypeRequest request, CancellationToken cancellationToken)
        {
            var type = _mapper.Map<Domain.Type>(request.type);
            return _categoryRepository.UpdateType(type);
        }
    }
}
