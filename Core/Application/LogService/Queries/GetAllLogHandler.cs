using Application.Dtos;
using Application.Types;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.LogService.Queries
{
    public class GetAllLogHandler : IRequestHandler<GetAllLogQuery, Pagination<LogDto>>
    {
        private readonly IGenericDapperRepository _dapper;
        private readonly ILogger<GetAllLogHandler> _logger;

        public GetAllLogHandler(IGenericDapperRepository dapper,ILogger<GetAllLogHandler> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }
        public async Task<Pagination<LogDto>> Handle(GetAllLogQuery request, CancellationToken cancellationToken)
        {
            
            int totalLogCount = await GetLogCount(request);
            var logs = await Getlogs(request);
            var deSerializedData = TransformData(logs);
            return new Pagination<LogDto>(request.LogParams.PageIndex, request.LogParams.PageSize, totalLogCount, deSerializedData);
        }
        private async Task<int> GetLogCount(GetAllLogQuery request)
        {
            int totalLogCount = await _dapper.CountDataAsync<dynamic>("usp_GetLogCount",new
                                        {
                                            Level = request.LogParams.Level
                                        });
            return totalLogCount;
        }
        private async Task<List<Log>> Getlogs(GetAllLogQuery request)
        {
            var logs = await _dapper.LoadData<Log, dynamic>("usp_GetAllLogs",
                                                new
                                                {
                                                    Page = request.LogParams.PageIndex,
                                                    Size = request.LogParams.PageSize,
                                                    Level = request.LogParams.Level
                                                });


            return logs;
        }
        private IReadOnlyList<LogDto> TransformData(List<Log> logs)
        {
            List<LogDto> logList = new();
            foreach (var log in logs)
            {
                var deserializedLog = JsonSerializer.Deserialize<LogDto>(log.LogEvent);
                deserializedLog.Id= log.Id;
                logList.Add(deserializedLog);
            }
            return logList;
        }
    }
}
