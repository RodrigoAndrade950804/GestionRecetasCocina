using GestionRecetasCocina.Repositories;
using GestionRecetasCocina.Services;
using GestionRecetasCocina.ViewModels;
using GestionRecetasCocina.Views;

namespace GestionRecetasCocina;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Registro de servicios y repositorios
        builder.Services.AddSingleton<RecetaRepositories>();
        builder.Services.AddSingleton<LogService>();

        // ViewModels
        builder.Services.AddTransient<AgregarRecetaViewModel>();
        builder.Services.AddTransient<ListaRecetasViewModel>();
        builder.Services.AddTransient<LogsViewModel>();

        // Views (Páginas)
        builder.Services.AddTransient<AgregarRecetaPage>();
        builder.Services.AddTransient<ListaRecetasPage>();
        builder.Services.AddTransient<LogsPage>();

        return builder.Build();
    }
}
