using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1._1.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        //Code source= https://www.youtube.com/watch?v=DuNLR_NJv8U&t=9938s
        public BaseViewModel()
        {

        }

        [ObservableProperty] // triggering events
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]// triggering events
        bool isBusy; 

        [ObservableProperty]
        string title;
        public bool IsNotBusy => !IsBusy; // the opposite to is busy.
    }
}
