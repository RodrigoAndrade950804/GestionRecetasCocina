using GestionRecetasCocina.Views;

namespace GestionRecetasCocina;

public partial class App : Application
{
    public App(AgregarRecetaPage agregarRecetaPage)
    {
        InitializeComponent();

        
        MainPage = new NavigationPage(agregarRecetaPage);
    }
}

