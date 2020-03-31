using MediatR;

namespace CRM.Service.EventHandler.Commands
{
    public class CustomerImageUploadCommand : INotification
    {
        public string Name { get; set; }
    }
}
