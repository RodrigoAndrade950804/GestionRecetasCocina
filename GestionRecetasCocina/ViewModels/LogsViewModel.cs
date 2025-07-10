using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GestionRecetasCocina.Services;
using Microsoft.Maui.Dispatching;

namespace GestionRecetasCocina.ViewModels
{
    public class LogsViewModel : INotifyPropertyChanged
    {
        private readonly LogService? _logService;

        public ObservableCollection<string> Logs { get; set; } = new();

        public ICommand CargarLogsCommand { get; }

        // Constructor sin parámetros (si lo usas en XAML)
        public LogsViewModel()
        {
            CargarLogsCommand = new Command(async () => await CargarLogsAsync());
        }

        // Constructor principal
        public LogsViewModel(LogService logService)
        {
            _logService = logService;
            CargarLogsCommand = new Command(async () => await CargarLogsAsync());
            _ = CargarLogsAsync(); // Carga inicial
        }

        public async Task CargarLogsAsync()
        {
            if (_logService is null)
                return;

            var lines = await _logService.ReadLogsAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Logs.Clear();
                foreach (var line in lines)
                    Logs.Add(line);
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

