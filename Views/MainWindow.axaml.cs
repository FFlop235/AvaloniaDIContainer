using Avalonia.Controls;
using AvaloniaDIContainer.ViewModels;

namespace AvaloniaDIContainer.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}