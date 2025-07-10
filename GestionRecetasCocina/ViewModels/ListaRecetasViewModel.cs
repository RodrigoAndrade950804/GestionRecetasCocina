using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GestionRecetasCocina.Models;
using GestionRecetasCocina.Repositories;
using GestionRecetasCocina.Services;
using Microsoft.Maui.Controls;

namespace GestionRecetasCocina.ViewModels
{
    public class ListaRecetasViewModel : INotifyPropertyChanged
    {
        private readonly RecetaRepositories _repository;
        private readonly LogService _logService;

        public ObservableCollection<Receta> Recetas { get; set; } = new();

        public ICommand EliminarCommand { get; }

        public ICommand RecargarCommand { get; }

        public ListaRecetasViewModel()
        {
            // Constructor vacío para XAML si lo necesitas
        }

        public ListaRecetasViewModel(RecetaRepositories repository, LogService logService)
        {
            _repository = repository;
            _logService = logService;

            EliminarCommand = new Command<Receta>(async (receta) => await EliminarRecetaAsync(receta));
            RecargarCommand = new Command(async () => await CargarRecetasAsync());

            Task.Run(CargarRecetasAsync);
        }

        public async Task CargarRecetasAsync()
        {
            if (_repository is null)
                return;

            var recetas = await _repository.GetRecetasAsync();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Recetas.Clear();
                foreach (var r in recetas)
                    Recetas.Add(r);
            });
        }

        public async Task EliminarRecetaAsync(Receta receta)
        {
            if (receta == null) return;

            bool confirmado = await Application.Current.MainPage.DisplayAlert(
                "Eliminar",
                $"¿Estás seguro que deseas eliminar la receta '{receta.Nombre}'?",
                "Sí", "No");

            if (!confirmado)
                return;

            await _repository.DeleteRecetaAsync(receta);
            await _logService.LogEliminarAsync(receta.Nombre);

            await CargarRecetasAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

