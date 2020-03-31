using System;

namespace CRM.Domain.Feautures
{
    public interface ISoftDelete
    {
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
