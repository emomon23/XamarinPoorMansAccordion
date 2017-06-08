using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PoorMansAccordion.Pages.ImageButtons
{
    public class ImageButtonsPageViewModel : BaseViewModel
    {
        private FishSpecies _selectedFishSpecies = null;

        public ImageButtonsPageViewModel(IMyNavigationService navigationService) : base(navigationService){}

        public ICommand FishSelected_Command
        {
            get
            {
                return new Command<string>((string buttonText) =>
                {
                    _selectedFishSpecies = AllMyFishSpecieses.FirstOrDefault(f => f.Name == buttonText);
                    App.Current.MainPage.DisplayAlert("Fish clicked", $"You chose {_selectedFishSpecies.Name}!", "Close");
                });
            }
        }

        public FishSpecies SelectedFishSpecies
        {
            get
            {
                return _selectedFishSpecies;
            }
            set
            {
                OnPropertyChanged();
                _selectedFishSpecies = value;
            }
        }

        public List<FishSpecies> AllMyFishSpecieses
        {
            get
            {
                //Imagine getting these data from a database or remote service call instead
                return new List<FishSpecies>()
                {
                    new FishSpecies() {Name = "walleye"},
                    new FishSpecies() {Name = "catfish", Icon = "catfish.gif"},
                    new FishSpecies() {Name = "crappie"},
                    new FishSpecies() {Name = "large mouth"},
                    new FishSpecies() {Name = "muskie", Icon = "muskie.gif"},
                    new FishSpecies() {Name = "pike"},
                    new FishSpecies() {Name = "small mouth"},
                    new FishSpecies() {Name = "sunfish"}
                };
            }
        }
    }

    //Test data model
    public class FishSpecies
    {
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                if (string.IsNullOrEmpty(Icon))
                {
                    Icon = $"{value.Replace(" ", "")}.png";
                }
            }
        }

        public string Icon { get; set; }
    }
}
