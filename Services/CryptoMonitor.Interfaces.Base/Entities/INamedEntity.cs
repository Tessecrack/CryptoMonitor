using System.ComponentModel.DataAnnotations;

namespace CryptoMonitor.Interfaces.Base.Entities
{
    public interface INamedEntity : IEntity
    {
        [Required]
        string Name { get; }
    }
}
