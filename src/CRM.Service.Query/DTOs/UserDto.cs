namespace CRM.Service.Query.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName
        {
            get
            {
                return $"{Surname}, {Name}";
            }
        }
        public string UserName { get; set; }
    }
}
