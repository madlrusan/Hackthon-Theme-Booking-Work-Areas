using ITEC.Backend.Domain.Models.Base;

namespace ITEC.Backend.Domain.Models
{
    public class Office : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Floor>? Floors { get; set; }
    }
}
