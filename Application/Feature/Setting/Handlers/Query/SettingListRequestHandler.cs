using Application.Feature.Setting.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Setting.Handlers.Query
{
    public class SettingListRequestHandler : IRequestHandler<SettingListRequest, List<Domain.Setting>>
    {
        private readonly IGeneralRepository _generalRepository;

        public SettingListRequestHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }
        public async Task<List<Domain.Setting>> Handle(SettingListRequest request, CancellationToken cancellationToken)
        {
            return (await _generalRepository.GetSettings(request.Pagesize,request.Skip,request.State)).ToList();
        }
    }
}
