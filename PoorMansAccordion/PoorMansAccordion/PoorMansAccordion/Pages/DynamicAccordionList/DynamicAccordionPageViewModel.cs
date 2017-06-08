using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoorMansAccordion.CustomControls;
using Xamarin.Forms;

namespace PoorMansAccordion.Pages.DynamicAccordionList
{
    public class DynamicAccordionPageViewModel : BaseViewModel
    {
        public DynamicAccordionPageViewModel(IMyNavigationService navigationService) : base(navigationService)
        {
          
        }


    }

    public class FoodType
    {
        public string Name { get; set; }
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }

        public static List<FoodType> GetFoodTypes()
        {
            return new List<FoodType>()
            {
                new FoodType() {BackColor = Color.Aqua, ForeColor = Color.Black, Name = "Veggie"},
                new FoodType() {BackColor = Color.Orange, ForeColor = Color.White, Name="Fruit"},
                new FoodType() {BackColor = Color.Olive, ForeColor = Color.Lime, Name = "Meat"}
            };
        }
    }

   
}
