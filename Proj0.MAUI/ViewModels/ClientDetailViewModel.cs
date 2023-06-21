﻿using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Proj0.MAUI.ViewModels
{
    public class ClientDetailViewModel : INotifyPropertyChanged
    {
        public Client Model { get; set; }
        public string openTime { get; set; }
        public int openMonth { get; set; }
        public int openYear { get; set; }
        public int openDay { get; set; }
        public string closedTime { get; set; }
        public int closedMonth { get; set; }
        public int closedYear { get; set; }
        public int closedDay { get; set; }
        public string name { get; set; }
        public string notes { get; set; }

        public bool isActive { get; set; }

        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }
        
        public void SetUpCommands()
        {
            openTime = Model.OpenDate.TimeOfDay.ToString();
            closedTime = Model.ClosedDate.TimeOfDay.ToString();
            openDay = Model.OpenDate.Day;
            closedDay = Model.ClosedDate.Day;
            openMonth = Model.OpenDate.Month;
            closedMonth = Model.ClosedDate.Month;
            openYear = Model.OpenDate.Year;
            closedYear = Model.ClosedDate.Year;
            name = Model.Name;
            notes = Model.Notes;
            if (ClientService.Current.allProjectsClosed(Model))
                IsActiveVisible = true;
            else
                IsActiveVisible = false;

            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ClientDetailViewModel).Model.Id));//first
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientDetailViewModel).Model.Id));
        }

        public void Undo()
        {
            SetUpCommands();
            NotifyPropertyChanged(nameof(openDay));
            NotifyPropertyChanged(nameof(openMonth));
            NotifyPropertyChanged(nameof(openYear));
            NotifyPropertyChanged(nameof(closedDay));
            NotifyPropertyChanged(nameof(closedMonth));
            NotifyPropertyChanged(nameof(closedYear));
            NotifyPropertyChanged(nameof(name));
            NotifyPropertyChanged(nameof(notes));
            NotifyPropertyChanged(nameof(isActive));
        }

        public ICommand DeleteCommand { get; private set; }
        public void ExecuteDelete(int id)
        {
            ClientService.Current.Delete(id);
        }

        public bool IsActiveVisible { get; set; }

        public ICommand EditCommand { get; private set; }
        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//ClientDetail?clientId={id}");
        }

        public ClientDetailViewModel(Client client)
        {
            Model = client;
            SetUpCommands();
        }

        public ClientDetailViewModel(int id)
        {
            Model = ClientService.Current.Get(id);
            if (Model == null)
                Model = new Client();
            SetUpCommands();
        }

        public ClientDetailViewModel()
        {
            Model = new Client();
            SetUpCommands();
        }

        public void Add()
        {
            Model.stringToOpenDate(openMonth.ToString() + '/' + openDay.ToString() + '/' + openYear.ToString() + ' ' + openTime);
            Model.stringToClosedDate(closedMonth.ToString() + '/' + closedDay.ToString() + '/' + closedYear.ToString() + ' ' + closedTime);
            Model.Name = name;
            Model.Notes = notes;
            Model.IsActive = isActive;
            ClientService.Current.Add(Model);
            Model = new Client();
        }

        public void Edit()
        {
            Model.stringToOpenDate(openMonth.ToString() + '/' + openDay.ToString() + '/' + openYear.ToString() + ' ' + openTime);
            Model.stringToClosedDate(closedMonth.ToString() + '/' + closedDay.ToString() + '/' + closedYear.ToString() + ' ' + closedTime);
            Model.Name = name;
            Model.Notes = notes;
            Model.IsActive = isActive;
            Model = new Client();
        }

        public void Active(bool isA)
        {
            isActive = isA;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}