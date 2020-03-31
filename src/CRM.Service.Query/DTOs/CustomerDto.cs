namespace CRM.Service.Query.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName 
        {
            get 
            {
                return $"{Surname}, {Name}";
            }
        }
        public string Photo { get; set; }
    }
}
