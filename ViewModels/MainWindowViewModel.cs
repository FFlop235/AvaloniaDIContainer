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
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace AvaloniaDIContainer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Person> _persons;
    
    private readonly IPersonRepository _personRepository;
    private readonly IStatisticsService _statisticsService;

    public MainWindowViewModel(IPersonRepository personRepository, IStatisticsService statisticsService)
    {
        _personRepository = personRepository;
        _statisticsService  = statisticsService;
        
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
        var result = await window.ShowDialog<bool>(GetMain());
        if (result)
        {
            _personRepository.Add(person);
            RefreshListCommand();
        }
    }

    [RelayCommand]
    public async void ShowStatsCommand()
    {
        int totalCount = _statisticsService.GetTotalCount();
        double averageAge = _statisticsService.GetAverageAge();
        Person oldestPerson = _statisticsService.GetOldestPerson();

        string statisticString = $"Всего {totalCount} | Средний возраст: {averageAge} | Самый старший: {oldestPerson.Name} ({oldestPerson.Age})";

        var statisticBox = MessageBoxManager
            .GetMessageBoxStandard("Статистика", statisticString, ButtonEnum.Ok);
        await statisticBox.ShowWindowDialogAsync(GetMain());
    }
}