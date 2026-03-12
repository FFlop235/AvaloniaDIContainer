using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaDIContainer.Models;
using AvaloniaDIContainer.ViewModels;

namespace AvaloniaDIContainer.Views;

public partial class AddWindow : Window
{
    public AddWindow(Person newPerson)
    {
        InitializeComponent();
        DataContext = new AddWindowViewModel(newPerson, Close);
    }
}