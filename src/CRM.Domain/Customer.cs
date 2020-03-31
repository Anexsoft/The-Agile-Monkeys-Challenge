using CRM.Domain.Feautures;

namespace CRM.Domain
{
    public class Customer : Audit, ISoftDelete
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Photo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
