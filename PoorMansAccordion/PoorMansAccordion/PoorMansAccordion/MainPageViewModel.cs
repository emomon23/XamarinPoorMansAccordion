using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PoorMansAccordion.CustomControls;
using Xamarin.Forms;

namespace PoorMansAccordion
{
    //BaseViewModel implements INotifyPropertyChanged
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigation navigation):base(navigation) { }

        public ICommand ShowAccordionXML_Command
        {
            get
            {
                return new Command(async () =>
                {
                    await _myNavigationService.NavigateToAccordionXMLPage();
                });
            }
        }

        public ICommand ShowDynamicAccordion_Command
        {
            get
            {
                return new Command(async () =>
                {
                    await _myNavigationService.NavigateToDynamicAccordionListPage();
                });
            }
        }

        public ICommand ShowImageButtons_Command
        {
            get
            {
                return new Command(async () =>
                {
                    await _myNavigationService.NavigateToImageButtonsPage();
                });
            }
        }

    }
}
