using MementoFX;

namespace Seje.OrdenCaptura.SharedKernel
{
    public class SejeDomaintEvent : DomainEvent
    {
        public string UserName { get; set; }
        public SejeDomaintEvent(string userName)
        {
            this.UserName = userName;
        }
    }
}
