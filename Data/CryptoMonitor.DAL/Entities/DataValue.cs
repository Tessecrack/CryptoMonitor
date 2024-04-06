using CryptoMonitor.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace CryptoMonitor.DAL.Entities
{
    [Index(nameof(Time))]
    public class DataValue : Entity
    {
        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

        public string Value { get; set; }

        public DataSource Source { get; set; }

        public bool IsFaulted { get; set; }
    }
}
