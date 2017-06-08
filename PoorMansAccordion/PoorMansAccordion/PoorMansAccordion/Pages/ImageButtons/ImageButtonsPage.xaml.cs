using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoorMansAccordion.CustomControls;
using Xamarin.Forms;

namespace PoorMansAccordion.Pages.ImageButtons
{
    public partial class ImageButtonsPage : ContentPage
    {
        private ImageButtonsPageViewModel _vm;

        //Always force a binding context for this page
        public ImageButtonsPage(ImageButtonsPageViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            this.BindingContext = _vm;

            LoadImageButtons();
        }


        private void LoadImageButtons()
        {
            foreach (var species in _vm.AllMyFishSpecieses)
            {
                ImgBtnGenerator.AddButton(speciesBtnContainer, _vm.FishSelected_Command, StyleSheet.Species_Button_Width,
                   StyleSheet.Species_Button_Height, species.Name, species.Icon, StyleSheet.Species_Font_Size, StyleSheet.Button_BackColor);
            }
        }
    }
}
