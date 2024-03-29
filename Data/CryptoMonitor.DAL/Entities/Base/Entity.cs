using CryptoMonitor.Interfaces.Base.Entities;

namespace CryptoMonitor.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
