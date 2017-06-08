using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoorMansAccordion.CustomControls;
using Xamarin.Forms;

namespace PoorMansAccordion.Pages.DynamicAccordionList
{
    public partial class DynamicAccordionPage : ContentPage
    {
        private DynamicAccordionPageViewModel _vm;

        //Always force a binding context for this page
        public DynamicAccordionPage(DynamicAccordionPageViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            this.BindingContext = _vm;

            DynamicallyBuildUI();
        }


        private void DynamicallyBuildUI()
        {
            //*** GO LOOK AT THE XAML PAGE TO SEE HOW I CREATED A 'SHELL' FOR MY PAGE ***
            var names = new string[] {"Mike", "Tom", "Bob", "Joe", "Gretchen", "Sarah", "Grace"};

            //This is going to be my 'outer' accordion. For each name, I will create an
            //accordion header.  For the accordion content, I will create a 'nested' accordion
            foreach (var name in names)
            {
                //Create the 'nested' accordion here
                var innerAccordionView = BuildNestedAccordion();

                //Create the 'parent' accordion node, and specify
                //the 'nested' (innerAccordionView) as the content view
                var acNode = AccordionFactory.CreateNewNode(
                    new AcNodeConfiguration()
                    {
                        HeaderText = name,
                        HeaderBackGroundColor = StyleSheet.Default_Accent_Color,
                        HeaderTextColor = StyleSheet.Default_Accent_Font_Color,
                        FontSize = StyleSheet.AccordionNode_HeaderFontSize,
                        HeaderFontAttributes = StyleSheet.AccordNode_HeaderFontAttributes,
                        ExpandedContentHeight = 300
                    },
                    innerAccordionView);

                //Add the accordion node to the stack layout defined in the xaml file.
                peopleNames.Children.Add(acNode);
            }

        }

        private View BuildNestedAccordion()
        {
            StackLayout nestedLayout = new StackLayout();
            
            //Imagine this query was submited to a database or rest api?
            var foodTypes = FoodType.GetFoodTypes();
           
            foreach (var ft in foodTypes)
            {
                //Create a content view for each food type 
                //(this could be a list view, or any other view you require)
                var yourNodesContent = BuildYourContentBasedOnTheItem(ft);

                var acNode = AccordionFactory.CreateNewNode(
                                   new AcNodeConfiguration()
                                   {
                                       HeaderText = ft.Name,
                                       HeaderBackGroundColor = ft.BackColor,
                                       HeaderTextColor = ft.ForeColor,
                                       FontSize = StyleSheet.AccordionNode_HeaderFontSize,
                                       HeaderFontAttributes = StyleSheet.AccordNode_HeaderFontAttributes,
                                       ExpandedContentHeight = StyleSheet.AccordionNodeContent_LabelHeight 
                                   },

                                   yourNodesContent);

                nestedLayout.Children.Add(acNode);
            }

            return nestedLayout;
        }
        
        private View BuildYourContentBasedOnTheItem(FoodType ft)
        {
            return new Label() {Text = $"Content for item '{ft.Name}'"};
        }
    }
}
