using System.Data;

namespace Domain.Types
{
    public class DbParam
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DbType DataType { get; set; }
    }
}
