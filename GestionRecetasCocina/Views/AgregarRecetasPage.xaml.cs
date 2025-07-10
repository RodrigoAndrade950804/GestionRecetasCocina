using GestionRecetasCocina.ViewModels;

namespace GestionRecetasCocina.Views;

public partial class AgregarRecetaPage : ContentPage
{
    public AgregarRecetaPage(AgregarRecetaViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


