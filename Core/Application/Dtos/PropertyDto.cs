using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PropertyDto
    {
        public string SourceContext { get; set; }
        public string ActionId { get; set; }
        public string ActionName { get; set; }
        public string RequestId { get; set; }
        public string RequestPath { get; set; }
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
        public string MachineName { get; set; }
        public int ProcessId { get; set; }
        public int ThreadId { get; set; }
    }
}
