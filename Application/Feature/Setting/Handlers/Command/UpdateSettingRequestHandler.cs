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
    public class UpdateSettingRequestHandler : IRequestHandler<UpdateSettingRequest, bool>
    {
        private readonly IGeneralRepository _generalRepository;

        public UpdateSettingRequestHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }
        public async Task<bool> Handle(UpdateSettingRequest request, CancellationToken cancellationToken)
        {
            return await  _generalRepository.Update(request.Setting);
        }
    }
}
