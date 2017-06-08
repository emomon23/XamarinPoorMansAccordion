using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace PoorMansAccordion.CustomControls
{
    public class ImgBtnGenerator
    {
        private Color _backColor;
        private Color _textColor;

        private static ImgBtnGenerator _buttonGeneraotr;

        private ImgBtnGenerator()
        {
            _backColor = StyleSheet.Default_Accent_Color;
            _textColor = StyleSheet.Default_Accent_Font_Color;
        }

        public View CreateButton(ICommand command, double width, double height, string text, string image = null, double? fontSize = null, Color? backColorOverride = null)
        {
            backColorOverride = backColorOverride.HasValue ? backColorOverride : _backColor;

            StackLayout layout = new StackLayout()
            {
                Margin = new Thickness(5, 5, 5, 5),
                Padding = new Thickness(8, 8, 8, 8),
                BackgroundColor = backColorOverride.Value,
                WidthRequest = width,
                HeightRequest = height,
                Orientation = StackOrientation.Horizontal
            };

            if (string.IsNullOrEmpty(text))
            {
                //No text, just an image, center in on the 'button'
                layout.VerticalOptions = LayoutOptions.CenterAndExpand;
                layout.HorizontalOptions = LayoutOptions.CenterAndExpand;
                layout.Padding = new Thickness(2, 2, 2, 2);
            }

            if (! string.IsNullOrEmpty(image))
            {
                Image img = new Image()
                {
                    Source = image,
                    WidthRequest = string.IsNullOrEmpty(text) ? width : width / 5,
                    HeightRequest = string.IsNullOrEmpty(text) ? height: height / 1.8,
                    BackgroundColor = Color.Transparent
                };

                layout.Children.Add(img);
            }

            if (! string.IsNullOrEmpty(text))
            {
                Label lbl = new Label()
                {
                    TextColor = _textColor,
                    Text = $" {text.ToUpper()}",
                    FontSize = fontSize ?? height / 2.2,
                    BackgroundColor = backColorOverride.Value,
                    HeightRequest = height - 10,
                    VerticalTextAlignment = TextAlignment.Center
                };

                layout.Children.Add(lbl);
            }

            TapGestureRecognizer tap = new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    await layout.ScaleTo(.95, 200, Easing.BounceIn);
                    layout.ScaleTo(1, 200);
                    command.Execute(text);
                })
            };

            layout.GestureRecognizers.Add(tap);

            return layout;
        }


        public static void AddButton(StackLayout container, ICommand command, double width,
                                        double height, string text = null, string image = null, double? fontSize = null, Color? backColor = null)
        {
            var bg = ImgBtnGenerator.GetInstance();
            var button = bg.CreateButton(command, width, height, text, image, fontSize, backColor);
            container.Children.Add(button);
        }

        public static ImgBtnGenerator GetInstance()
        {
            if (_buttonGeneraotr == null)
            {
                _buttonGeneraotr = new ImgBtnGenerator();
            }

            return _buttonGeneraotr;
        }

        public static ImgBtnGenerator GetInstance(Color backColor, Color texColor)
        {
            var result = GetInstance();
            result._backColor = backColor;
            result._textColor = texColor;

            return result;
        }
    }
}
