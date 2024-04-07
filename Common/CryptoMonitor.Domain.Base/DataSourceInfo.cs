using CryptoMonitor.Interfaces.Base.Entities;

namespace CryptoMonitor.Domain.Base
{
	public class DataSourceInfo : INamedEntity
	{
		public string Name { get; set; }

		public int Id { get; set; }

		public string Description { get; set; }
	}

	public class DataValueInfo : IEntity
	{
		public int Id { get; set; }

		public DateTimeOffset Time { get; set; }

		public string Value { get; set; }

		public bool IsFaulted { get; set; }
	}
}
