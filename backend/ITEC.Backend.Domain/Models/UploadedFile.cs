using ITEC.Backend.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Domain.Models
{
    public class UploadedFile : EntityBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Floor? Floor { get; set; }
    }
}
