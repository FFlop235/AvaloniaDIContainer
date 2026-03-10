using System;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaDIContainer.DB;
using AvaloniaDIContainer.Models;
using CommunityToolkit.Mvvm.ComponentModel;

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
}