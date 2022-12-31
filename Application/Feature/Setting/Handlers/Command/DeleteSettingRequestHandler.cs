using Application.Feature.Setting.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Setting.Handlers.Command
{
    public class DeleteSettingRequestHandler : IRequestHandler<DeleteSettingRequest, bool>
    {
        private readonly IGeneralRepository _generalRepository;

        public DeleteSettingRequestHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }
        public async Task<bool> Handle(DeleteSettingRequest request, CancellationToken cancellationToken)
        {
            return await _generalRepository.DeleteSetting(request.Id);
        }
    }
}
