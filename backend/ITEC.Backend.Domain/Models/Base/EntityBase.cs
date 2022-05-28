
using Microsoft.AspNetCore.Identity;

namespace ITEC.Backend.Domain.Models.Base
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAtTimeUtc { get; set; }
        public DateTime? UpdatedAtTimeUtc { get; set; }
        public string CreatedByUserId { get; set; }
        public IdentityUser? CreatedByUser { get; set; }

    }
}
