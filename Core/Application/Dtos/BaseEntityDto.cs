using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Dtos
{
    public class BaseEntityDto : BaseDto
    {
        public int Id { get; set; } = -1;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
