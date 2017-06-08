using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PoorMansAccordion.CustomControls
{
    public class AccordionNodeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private bool _subContentIsVisible = false;

        #endregion

        public AccordionNodeViewModel(string headerText, int expandedContentHeight, Color headerBackgroundColor, Color headerTextColor, Color? lineColor = null, string iconExpandText = "+", string icondeContractText = "-", bool expandInitially = false)
        {

            HeaderText = headerText;
            _expandedContentHeight = expandedContentHeight;
            _iconExpandButtonText = iconExpandText;
            _iconContractButtonText = icondeContractText;
            HeaderBackGroundColor = headerBackgroundColor;
            HeaderTextColor = headerTextColor;
            LineColor = lineColor ?? headerTextColor;

            if (expandInitially)
            {
                ExpandContractAccordion.Execute(null);
            }
        }

        public int ContentHeight
        {
            get { return _currentContentHeight; }
            set
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
            private set
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

        public bool SubContentIsVisible
        {
            get
            {
                return _subContentIsVisible;
            }
            private set
            {
                _subContentIsVisible = value;
                OnPropertyChanged();
            }
        }

        public void ShowSubContent()
        {
            SubContentIsVisible = true;
            ContentHeight = _expandedContentHeight;
        }

        public void HideSubContent(int? reduceSize = null)
        {
            SubContentIsVisible = false;

            if (reduceSize.HasValue)
            {
                ContentHeight = reduceSize.Value;
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
                        IconText = _iconExpandButtonText;
                        ContentHeight = 0;
                    }
                    else
                    {
                        IsExpanded = true;
                        IconText = _iconContractButtonText;
                        ContentHeight = _expandedContentHeight;
                    }
                });
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class AcNodeConfiguration
    {
        public AcNodeConfiguration()
        {
            iconContractedText = "-";
            iconExpandedText = "+";
            HeaderBackGroundColor = Color.Silver;
            HeaderTextColor = Color.Black;
            FontFamily = Device.OnPlatform("MarkerFelt-Thin", "Droid Sans Mono", "Comic Sans MS");

        }

        public string HeaderText { get; set; }
        public double ExpandedContentHeight { get; set; }
        public Color HeaderBackGroundColor { get; set; }
        public Color HeaderTextColor { get; set; }
        public Color LineColor { get; set; }
        public string iconExpandedText { get; set; }
        public string iconContractedText { get; set; }
        public bool ExpandInitially { get; set; }
        public FontAttributes? HeaderFontAttributes { get; set; }
        public double FontSize { get; set; }
        public string FontFamily { get; set; }

        public double GetFontSize()
        {
            Label lblDummy = new Label();
            if (FontSize > 0)
            {
                return FontSize;
            }

            return Device.GetNamedSize(NamedSize.Default, lblDummy);
        }
    }

    public static class AccordionFactory
    {
        public static StackLayout CreateNewNode(AcNodeConfiguration configuration, View contentView)
        {
            if (configuration.ExpandedContentHeight == 0)
            {
                configuration.ExpandedContentHeight = contentView.Height;
            }
            AccordionNodeViewModel accordionNodeView = new AccordionNodeViewModel(configuration.HeaderText,
                (int)configuration.ExpandedContentHeight,
                configuration.HeaderBackGroundColor,
                configuration.HeaderTextColor,
                configuration.LineColor,
                configuration.iconExpandedText,
                configuration.iconContractedText,
                configuration.ExpandInitially);

            var result = new StackLayout()
            {
                BindingContext = accordionNodeView,
                Spacing = 0,
                Margin = new Thickness(0, 0, 0, 0)
            };

            result.Children.Add(BuildHeaderLayout(configuration));
            result.Children.Add(BuildContentLayout(contentView));

            return result;
        }

        private static StackLayout BuildHeaderLayout(AcNodeConfiguration configuration)
        {
            //Header layout
            StackLayout headerLayout = new StackLayout()
            {
                Margin = new Thickness(0, 0, 0, 0)
            };
            headerLayout.SetBinding(StackLayout.BackgroundColorProperty, "HeaderBackGroundColor");

            //Line one
            BoxView lineOne = new BoxView() { HeightRequest = 1, HorizontalOptions = LayoutOptions.FillAndExpand };
            lineOne.SetBinding(BoxView.ColorProperty, "LineColor");
            headerLayout.Children.Add(lineOne);

            headerLayout.Children.Add(BuildHeaderLabelsLayout(configuration));

            //Line two
            BoxView lineTwo = new BoxView() { HeightRequest = 1, HorizontalOptions = LayoutOptions.FillAndExpand };
            lineTwo.SetBinding(BoxView.ColorProperty, "LineColor");
            headerLayout.Children.Add(lineTwo);

            return headerLayout;
        }

        private static View BuildContentLayout(View content)
        {
            var stackLayout = new StackLayout();
            stackLayout.SetBinding(StackLayout.IsVisibleProperty, "IsExpanded");
            stackLayout.SetBinding(StackLayout.HeightRequestProperty, "ContentHeight");

            stackLayout.Children.Add(content);

            var result = new ScrollView();
            result.Content = stackLayout;
            return result;
        }

        private static StackLayout BuildHeaderLabelsLayout(AcNodeConfiguration configuration)
        {
            StackLayout headerLabelsLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(5, 10, 10, 10)
            };

            TapGestureRecognizer headerTapGestureRecognizer = new TapGestureRecognizer();
            headerTapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "ExpandContractAccordion");
            headerLabelsLayout.GestureRecognizers.Add(headerTapGestureRecognizer);

            headerLabelsLayout.Children.Add(CreateLabel("HeaderText", "HeaderTextColor", configuration, null));
            headerLabelsLayout.Children.Add(CreateLabel("IconText", "HeaderTextColor", configuration, LayoutOptions.EndAndExpand));

            return headerLabelsLayout;
        }

        private static Label CreateLabel(string textBinding, string textColorBinding, AcNodeConfiguration configuration,
            LayoutOptions? layoutOptions)
        {
            Label lbl = new Label();
            lbl.SetBinding(Label.TextProperty, textBinding);
            lbl.SetBinding(Label.TextColorProperty, textColorBinding);


            lbl.FontFamily = configuration.FontFamily;
            lbl.FontSize = configuration.GetFontSize();

            if (configuration.HeaderFontAttributes.HasValue)
            {
                lbl.FontAttributes = configuration.HeaderFontAttributes.Value;
            }

            if (layoutOptions.HasValue)
            {
                lbl.HorizontalOptions = layoutOptions.Value;
            }

            return lbl;
        }
    }
}
