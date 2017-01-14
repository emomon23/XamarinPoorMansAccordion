using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PoorMansAccordion
{
    //BaseViewModel implements INotifyPropertyChanged
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            VeggiesAccordionNode = new AccordionNodeViewModel("Veggies", 200, Color.Aqua, Color.Maroon, Color.Fuchsia);    
            FruitAccordionNode = new AccordionNodeViewModel("Fruit", 55, Color.Green, Color.Black);
        }

        public AccordionNodeViewModel VeggiesAccordionNode { get; private set; }
        public AccordionNodeViewModel FruitAccordionNode { get; private set; }

        public string[] VeggieList
        {
            get
            {
                return new string[] {"Carrots", "Potatos", "Onions", "Beans"};
            }
        }
    }



    public class AccordionNodeViewModel : BaseViewModel
    {
        #region Private Properties 

        private bool _isExpanded = false;
        private string _headerText;
        
        
        //What the height should be when expanded
        private int _expandedContentHeight;

        //What's the current height
        private int _currentContentHeight = 0;

        //These Could Be Images instead of text
        //What should display when the nodes is expanded
        private string _iconExpandButtonText;

        //What should display when the nodes is contracted
        private string _iconContractButtonText;

        //What is currrently displaying to the user
        private string _currentIconButtonText = "+";

        #endregion

        public AccordionNodeViewModel(string headerText, int expandedContentHeight, Color headerBackgroundColor, Color headerTextColor, Color? lineColor = null, string iconExpandText = "+", string icondeContractText = "-")
        {

            HeaderText = headerText;
            _expandedContentHeight = expandedContentHeight;
            _iconExpandButtonText = iconExpandText;
            _iconContractButtonText = icondeContractText;
            HeaderBackGroundColor = headerBackgroundColor;
            HeaderTextColor = headerTextColor;
            LineColor = lineColor ?? headerTextColor;
        }

        public int ContentHeight
        {
            get { return _currentContentHeight; }
            private set
            {
                _currentContentHeight = value;
                OnPropertyChanged();
            }
        }

        public string HeaderText
        {
            get
            {
                return _headerText;
            }
            private  set
            {
                _headerText = value;
                OnPropertyChanged();
            }
        }

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            private set
            {
                _isExpanded = value;
                OnPropertyChanged();
            }
        }

        public string IconText
        {
            get { return _currentIconButtonText; }
            private set
            {
                _currentIconButtonText = value;
                OnPropertyChanged();
            }
        }

        public Color HeaderBackGroundColor { get; private set; }

        public Color HeaderTextColor { get; private set; }

        public Color LineColor { get; private set; }


        public ICommand ExpandContractAccordion
        {
            get
            {
                return new Command(() =>
                {
                    if (IsExpanded)
                    {
                        IsExpanded = false;
                        IconText = _iconContractButtonText;
                        ContentHeight = 0;
                    }
                    else
                    {
                        IsExpanded = true;
                        IconText = _iconExpandButtonText;
                        ContentHeight = _expandedContentHeight;
                    }
                });
            }
        }
    }


    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
