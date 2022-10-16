using Application.Models;
using Application.Options;
using MediatR;
using Microsoft.Extensions.Options;


namespace Application.GlobalService.Queries
{
    public class GetGlobalsHandler : IRequestHandler<GetGlobalsQueries, GlobalModel>
    {
        
        private readonly IOptions<AppConfigurationOptions> _options;
        public GetGlobalsHandler(IOptions<AppConfigurationOptions> options)
        {   
            _options = options;
        }
        public Task<GlobalModel> Handle(GetGlobalsQueries request, CancellationToken cancellationToken)
        {
            GlobalModel global = new();
            global.ServerAppConfigurations = _options.Value;
            return Task.FromResult(global);
            
        }
    }
}
