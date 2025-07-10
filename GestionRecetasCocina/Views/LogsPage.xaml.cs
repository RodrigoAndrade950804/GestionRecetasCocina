using GestionRecetasCocina.ViewModels;

namespace GestionRecetasCocina.Views;

public partial class LogsPage : ContentPage
{
    public LogsPage(LogsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}