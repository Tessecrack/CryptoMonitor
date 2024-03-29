using CryptoMonitor.Interfaces.Base.Entities;

namespace CryptoMonitor.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}
