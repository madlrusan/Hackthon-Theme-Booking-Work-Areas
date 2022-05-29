using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Options
{
    public class SendgridOptions
    {
        public string ApiKey { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public bool? DevMode { get; set; }
        public string DevModeReceiver { get; set; }
    }
}
