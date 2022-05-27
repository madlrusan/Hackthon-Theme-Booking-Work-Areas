using ITEC.Backend.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Domain.Models
{
    public class Desk : EntityBase
    {
        public string Name { get; set; }
        public float LocationX { get; set; }
        public float LocationY { get; set; }
        public bool IsHotelingDesk { get; set; }
        public int FloorId { get; set; }
        public Floor? Floor { get; set; }
        public ICollection<DeskReservation> Reservations { get;}


    }
}
