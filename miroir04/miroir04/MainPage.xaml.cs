using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace miroir04
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //ATTRIBUTS
        Button button;
        WarningLight warningLight;
        EmotionApi emotionApi;

        bool checkBoxState;
        
        //CONSTUCTOR
        public MainPage()
        {
            this.InitializeComponent();

            checkBoxState = false;

            button = new Button();
            warningLight = new WarningLight();
            emotionApi = new EmotionApi();

            //subscription of classes to events
            button.buttonPressed += this.OnButtonPressed;
            button.buttonPressed += warningLight.OnButtonPressed;
        }

        //METHODS



        //EVENTS LISTENED AND EXECUTED
        private async void OnButtonPressed(object source, EventArgs e)
        {
            if (checkBoxState == false)
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () => { checkBoxStatePhysicalButton.SetValue(CheckBox.IsCheckedProperty, true); });
                checkBoxState = true;
            }
            else
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () => { checkBoxStatePhysicalButton.SetValue(CheckBox.IsCheckedProperty, false); });
                checkBoxState = false;
            }
        }
    }
}
