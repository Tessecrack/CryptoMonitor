using AutoMapper;
using CryptoMonitor.DAL.Entities;
using CryptoMonitor.Domain.Base;

namespace CryptoMonitor.API.Infrastucture.Automapper
{
	public class DataSourceMap : Profile
	{
        public DataSourceMap()
        {
            CreateMap<DataSourceInfo, DataSource>()
                .ReverseMap();
        }
    }
}
