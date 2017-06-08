using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoorMansAccordion.Pages.AccordionXML;
using PoorMansAccordion.Pages.DynamicAccordionList;
using PoorMansAccordion.Pages.ImageButtons;
using Xamarin.Forms;

namespace PoorMansAccordion
{
    public interface IMyNavigationService
    {
        Task NavigateToAccordionXMLPage();
        Task NavigateToDynamicAccordionListPage();
        Task NavigateToImageButtonsPage();
    }

    public class MyNavigationService : IMyNavigationService
    {
        private INavigation _navigator;
       
        public MyNavigationService(INavigation navigator)
        {
            _navigator = navigator;
        }

        public async Task NavigateToAccordionXMLPage()
        {
            AccordionPageViewModel vm = new AccordionPageViewModel(this);
            var newPage = new AccordionPage(vm);

            await _navigator.PushAsync(newPage);
        }

        public async Task NavigateToDynamicAccordionListPage()
        {
            DynamicAccordionPageViewModel vm = new DynamicAccordionPageViewModel(this);
            var newPage = new DynamicAccordionPage(vm);

            await _navigator.PushAsync(newPage);
        }

        public async Task NavigateToImageButtonsPage()
        {
            ImageButtonsPageViewModel vm = new ImageButtonsPageViewModel(this);
            var newPage = new ImageButtonsPage(vm);

            await _navigator.PushAsync(newPage);
        }

        public async Task NavigateBackToMainPage()
        {
            await _navigator.PopAsync();
        }
    }
}
