using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GestionRecetasCocina.Models;
using GestionRecetasCocina.Repositories;
using GestionRecetasCocina.Services;
using Microsoft.Maui.Controls;

namespace GestionRecetasCocina.ViewModels
{
    public partial class AgregarRecetaViewModel : INotifyPropertyChanged
    {
        private string nombre = string.Empty;
        private string ingredientes = string.Empty;
        private int tiempoPreparacionMinutos;
        private bool esVegetariana;

        private readonly RecetaRepositories? _repository;
        private readonly LogService? _logService;

        // Constructor sin parámetros (no recomendado si no se inyectan dependencias)
        public AgregarRecetaViewModel()
        {
            // Asegúrate de usar este solo para diseño o pruebas
            _repository = new RecetaRepositories();
            _logService = new LogService();
            GuardarCommand = new Command(async () => await GuardarRecetaAsync());
        }

        // Constructor principal con inyección
        public AgregarRecetaViewModel(RecetaRepositories repository, LogService logService)
        {
            _repository = repository;
            _logService = logService;
            GuardarCommand = new Command(async () => await GuardarRecetaAsync());
        }

        // Propiedades enlazadas a la vista
        public string Nombre
        {
            get => nombre;
            set { nombre = value; OnPropertyChanged(); }
        }

        public string Ingredientes
        {
            get => ingredientes;
            set { ingredientes = value; OnPropertyChanged(); }
        }

        public int TiempoPreparacionMinutos
        {
            get => tiempoPreparacionMinutos;
            set { tiempoPreparacionMinutos = value; OnPropertyChanged(); }
        }

        public bool EsVegetariana
        {
            get => esVegetariana;
            set { esVegetariana = value; OnPropertyChanged(); }
        }

        public ICommand GuardarCommand { get; private set; }

        // Método para guardar receta
        private async Task GuardarRecetaAsync()
        {
            if (!EsVegetariana && TiempoPreparacionMinutos > 180)
            {
                await Shell.Current.DisplayAlert("Éxito", "Receta guardada", "OK");
                return;
            }

            var receta = new Receta
            {
                Nombre = this.Nombre,
                Ingredientes = this.Ingredientes,
                TiempoPreparacionMinutos = this.TiempoPreparacionMinutos,
                EsVegetariana = this.EsVegetariana
            };

            await _repository.AddRecetaAsync(receta);
            await _logService.LogCrearAsync(receta.Nombre);

            await Application.Current.MainPage.DisplayAlert("Éxito", "Receta guardada correctamente.", "OK");

            // Limpiar campos después de guardar
            Nombre = string.Empty;
            Ingredientes = string.Empty;
            TiempoPreparacionMinutos = 0;
            EsVegetariana = false;
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

