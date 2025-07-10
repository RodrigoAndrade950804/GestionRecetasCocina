using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GestionRecetasCocina.Models;
using GestionRecetasCocina.Repositories;
using GestionRecetasCocina.Services;
using Microsoft.Maui.Controls;

namespace GestionRecetasCocina.ViewModels
{
    public class EditarRecetaViewModel : INotifyPropertyChanged
    {
        private readonly RecetaRepositories _repository;
        private readonly LogService _logService;

        private int id;
        private string nombre = string.Empty;
        private string ingredientes = string.Empty;
        private int tiempoPreparacionMinutos;
        private bool esVegetariana;

        public EditarRecetaViewModel(RecetaRepositories repository, LogService logService, Receta receta)
        {
            _repository = repository;
            _logService = logService;

            // Cargar datos de la receta a editar
            Id = receta.Id;
            Nombre = receta.Nombre;
            Ingredientes = receta.Ingredientes;
            TiempoPreparacionMinutos = receta.TiempoPreparacionMinutos;
            EsVegetariana = receta.EsVegetariana;

            GuardarCommand = new Command(async () => await GuardarCambiosAsync());
        }

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }

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

        public ICommand GuardarCommand { get; }

        private async Task GuardarCambiosAsync()
        {
            if (!EsVegetariana && TiempoPreparacionMinutos > 180)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El tiempo de preparación no puede superar 180 minutos si no es vegetariana.", "OK");
                return;
            }

            var recetaActualizada = new Receta
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Ingredientes = this.Ingredientes,
                TiempoPreparacionMinutos = this.TiempoPreparacionMinutos,
                EsVegetariana = this.EsVegetariana
            };

            await _repository.UpdateRecetaAsync(recetaActualizada);
            await _logService.LogActualizarAsync(recetaActualizada.Nombre);

            await Application.Current.MainPage.DisplayAlert("Éxito", "Receta actualizada correctamente.", "OK");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
