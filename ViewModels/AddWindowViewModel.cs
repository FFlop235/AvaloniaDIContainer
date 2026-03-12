using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaDIContainer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace AvaloniaDIContainer.ViewModels;

public partial class AddWindowViewModel: ViewModelBase
{
    [ObservableProperty]
    private Person _person;
    
    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _email;
    
    [ObservableProperty]
    private int _age;

    [ObservableProperty] 
    private List<int> _ageList = new List<int>(Enumerable.Range(18, 70));
    
    private Action<object?> close;
    public AddWindowViewModel(Person newPerson, Action<object?> close)
    {
        Person = newPerson;
        this.close = close;
    }

    Window GetWindow()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            foreach (var window in desktop.Windows)
            {
                if (window.DataContext == this)
                {
                    return window;
                }
            }
        }

        return null;
    }

    [RelayCommand]
    public void Close()
    {
        close(null);
    }
    
    [RelayCommand]
    public async void Save()
    {
        if (Name == null || string.IsNullOrWhiteSpace(Name))
        {
            var errorBox = MessageBoxManager
                .GetMessageBoxStandard("Ошибка", "Укажите ФИО сотрудника", ButtonEnum.Ok);
            await errorBox.ShowWindowDialogAsync(GetWindow());
            close(false);
        }
        Person.Name = Name;
        
        if (Email == null || string.IsNullOrWhiteSpace(Email))
        {
            var errorBox = MessageBoxManager
                .GetMessageBoxStandard("Ошибка", "Укажите почту сотрудника", ButtonEnum.Ok);
            await errorBox.ShowWindowDialogAsync(GetWindow());
            close(false);
        }
        Person.Email =  Email;

        if (Age == null || Age < 18)
        {
            var errorBox = MessageBoxManager
                .GetMessageBoxStandard("Ошибка", "Некорректный возраст сотрудника", ButtonEnum.Ok);
            await errorBox.ShowWindowDialogAsync(GetWindow());
            close(false);
        }
        Person.Age = Age;
        
        close(true);
    }
}