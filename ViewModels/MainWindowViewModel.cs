using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaDIContainer.DB;
using AvaloniaDIContainer.Models;
using AvaloniaDIContainer.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaDIContainer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Person> _persons;
    
    private readonly IPersonRepository _personRepository;
    private readonly IServiceProvider _serviceProvider;

    public MainWindowViewModel(IPersonRepository personRepository, IServiceProvider serviceProvider)
    {
        _personRepository = personRepository;
        _serviceProvider = serviceProvider;
        
        var persons = _personRepository.GetAll().ToList();
        Persons = new ObservableCollection<Person>(persons);
    }

    Window GetMain()
    {
        Window main = null;
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            main = desktop.MainWindow;
        return main;
    }

    [RelayCommand]
    public void RefreshListCommand()
    {
        Persons.Clear();
        foreach (var person in _personRepository.GetAll())
        {
            Persons.Add(person);
        }
    }
    
    [RelayCommand]
    public async void AddPerson()
    {
        var person = new Person();
        var window = new AddWindow(person);
        // window.ShowDialog(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null);
        var result = await window.ShowDialog<bool>(GetMain());
        if (result)
        {
            _personRepository.Add(person);
            RefreshListCommand();
        }
    }
}