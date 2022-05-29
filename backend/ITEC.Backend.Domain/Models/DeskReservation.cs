using ITEC.Backend.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Domain.Models
{
    public class DeskReservation : EntityBase
    {
        public DateTime ReservationDate { get; set; }
        public int DeskId { get; set; }
        public Desk? Desk { get; set; }
    }
}
