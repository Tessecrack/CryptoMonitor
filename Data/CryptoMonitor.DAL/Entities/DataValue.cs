using CryptoMonitor.DAL.Entities.Base;

namespace CryptoMonitor.DAL.Entities
{
    public class DataValue : Entity
    {
        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

        public string Value { get; set; }

        public DataSource Source { get; set; }

        public bool IsFaulted { get; set; }
    }
}
