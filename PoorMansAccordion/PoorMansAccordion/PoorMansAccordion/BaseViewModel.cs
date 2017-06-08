using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PoorMansAccordion
{

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected IMyNavigationService _myNavigationService;

        public BaseViewModel(IMyNavigationService navigation = null)
        {
            _myNavigationService = navigation;
        }

        public BaseViewModel(INavigation navigation)
        {
            _myNavigationService = new MyNavigationService(navigation);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
