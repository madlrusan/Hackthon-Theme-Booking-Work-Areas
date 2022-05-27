using ITEC.Backend.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Domain.Models
{
    public class Floor : EntityBase
    {
        public string Name { get; set; }
        public int OfficeId { get; set; }
        public int MapId { get; set; }
        public UploadedFile? Map { get; set; }
        public Office? Office { get; set; }
        public ICollection<Desk>? Desks { get; set; }
    }
}
