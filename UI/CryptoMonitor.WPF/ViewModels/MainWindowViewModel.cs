using CryptoMonitor.DAL.Entities;
using CryptoMonitor.Interfaces.Base.Repositories;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CryptoMonitor.WPF.ViewModels
{
    internal class MainWindowViewModel : TitledViewModel
    {
        private readonly IRepository<DataSource> _dataSources;

        public MainWindowViewModel(IRepository<DataSource> dataSources)
        {
            Title = "Crypto monitoring BYBIT";
            this._dataSources = dataSources;
        }

        public ObservableCollection<DataSource> DataSources { get; set; } = new ObservableCollection<DataSource>();


        #region LoadDataSourcesCommand

        private LambdaCommand _loadDataSourceCommand;
        public ICommand LoadDataSourcesCommand => _loadDataSourceCommand ??= new(OnLoadDataSourcesCommandExecuted);
        private async void OnLoadDataSourcesCommandExecuted(object parameter) 
        {
            DataSources.Clear();
            foreach (var source in await _dataSources.GetAllAsync())
            {
                DataSources.Add(source);
            }
        }

        #endregion
    }
}
