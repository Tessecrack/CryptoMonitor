using CryptoMonitor.Interfaces.Base.Entities;
using System.ComponentModel.DataAnnotations;

namespace CryptoMonitor.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
