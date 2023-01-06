using Application.Feature.Misc.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Misc.Handlers.Query
{
    public class GetIndexPageInformationRequestHandler : IRequestHandler<GetIndexPageInformationRequest, string[]>
    {
        private readonly IGeneralRepository _generalRepository;

        public GetIndexPageInformationRequestHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<string[]> Handle(GetIndexPageInformationRequest request, CancellationToken cancellationToken)
        {
            return await _generalRepository.GetIndexPageValues();
        }
    }
}
