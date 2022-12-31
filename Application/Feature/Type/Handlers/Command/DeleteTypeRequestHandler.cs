using Application.Feature.Type.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Type.Handlers.Command
{
    public class DeleteTypeRequestHandler : IRequestHandler<DeleteTypeRequest,bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteTypeRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteTypeRequest request, CancellationToken cancellationToken)
        {
            var tmpType = await _categoryRepository.GetType(request.Id);
            if (tmpType == null)
                return false;
            else
            {
                tmpType.State = 1;
                var type = _mapper.Map<Domain.Type>(tmpType);
                return await _categoryRepository.Update(type);
            }
        }
    }
}
