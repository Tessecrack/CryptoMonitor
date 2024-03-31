using CryptoMonitor.DAL.Context;
using CryptoMonitor.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoMonitor.API.Data
{
    public class DataDBInitializer
    {
        private readonly DataDB _db;

        public DataDBInitializer(DataDB db)
        {
            _db = db;
        }

        public void Initialize()
        {
            _db.Database.Migrate();

            if (_db.Sources.Any())
            {
                return;
            }

            var rnd = new Random();
            for (var i = 1; i <= 10; ++i)
            {
                var source = new DataSource()
                {
                    Name = $"Source-{i}",
                    Description = $"Test data source {i}",
                };

                _db.Sources.Add(source);
                var values = new DataValue[rnd.Next(10, 20)];
                for (var j = 0; j < values.Length; ++j)
                {
                    var value = new DataValue()
                    {
                        Source = source,
                        Time = DateTimeOffset.Now.AddDays(rnd.Next(0, 365)),
                        Value = $"{rnd.Next(0, 30)}"
                    };
                    values[j] = value;
                }
                _db.Values.AddRange(values);
            }
            _db.SaveChanges();
        }
    }
}
