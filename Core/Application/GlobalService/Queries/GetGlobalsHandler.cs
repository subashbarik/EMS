using Application.CompanyService.Queries;
using Application.Models;
using Application.Options;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;


namespace Application.GlobalService.Queries
{
    public class GetGlobalsHandler : IRequestHandler<GetGlobalsQueries, GlobalModel>
    {
        private readonly IOptions<AppConfigurationOptions> _options;
        private readonly IMediator _mediator;
        public GetGlobalsHandler(IOptions<AppConfigurationOptions> options,IMediator mediator)
        {   
            _options = options;
            _mediator = mediator;
        }
        public async Task<GlobalModel> Handle(GetGlobalsQueries request, CancellationToken cancellationToken)
        {
            GlobalModel global = new();
            global.ServerAppConfigurations = _options.Value;
            var companyInfo = await _mediator.Send(new GetCompanyInfoQuery());
            global.companyInfo = companyInfo;
            return global;
        }
    }
}
