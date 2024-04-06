using CryptoMonitor.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace CryptoMonitor.DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class DataSource : NamedEntity
    {
        public string Description { get; set; }
    }
}
