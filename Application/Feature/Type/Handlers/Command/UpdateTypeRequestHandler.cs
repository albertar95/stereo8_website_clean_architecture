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

        public async Task<bool> Handle(UpdateTypeRequest request, CancellationToken cancellationToken)
        {
            var type = await _categoryRepository.GetType(request.type.Id);
            if (type == null) return false;
            else
            {
                _mapper.Map(request.type, type);
                return await _categoryRepository.Update(type);
            }
        }
    }
}
