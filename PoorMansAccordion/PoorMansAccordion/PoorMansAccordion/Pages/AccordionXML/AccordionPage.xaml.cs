using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PoorMansAccordion.Pages.AccordionXML
{
    public partial class AccordionPage : ContentPage
    {
        public AccordionPage(AccordionPageViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}
