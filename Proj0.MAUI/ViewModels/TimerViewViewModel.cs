﻿using Microsoft.Maui.Dispatching;
using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Proj0.MAUI.ViewModels
{
    public class TimerViewViewModel : INotifyPropertyChanged
    {
        public Project Project { get; set; }
        public string TimerDisplay
        {
            get
            {
                return string.Format("{0:00}:{1:00}:{2:00}",
              stopwatch.Elapsed.Hours,
              stopwatch.Elapsed.Minutes,
              stopwatch.Elapsed.Seconds);
            }
        }
        public string ProjectDisplay
        {
            get
            {
                return Project.ShortName;
            }
        }

        private IDispatcherTimer timer { get; set; }
        private Stopwatch stopwatch { get; set; }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public void ExecuteStart()
        {
            stopwatch.Start();
            timer.Start();
        }

        public void ExecuteStop()
        {
            if(stopwatch.IsRunning == true)
                TimeService.Current.Add(new Time { Id = 0, Date = DateTime.Now, Narrative = "Autogenerated narrative.", Hours = stopwatch.Elapsed.TotalHours, ProjectId = Project.Id, EmployeeId = 0 });
            stopwatch.Stop();
        }

        private void SetupCommands()
        {
            StartCommand = new Command(ExecuteStart);
            StopCommand = new Command(ExecuteStop);
        }
        public TimerViewViewModel(int projectId)
        {
            Project = ProjectService.Current.Get(projectId) ?? new Project();
            stopwatch = new Stopwatch();
            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.IsRepeating = true;

            timer.Tick += Timer_Tick;
            SetupCommands();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timer.IsRunning)
            {
                NotifyPropertyChanged(nameof(TimerDisplay));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}