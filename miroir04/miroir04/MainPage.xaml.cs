using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
        Webcam webcam;

        bool checkBoxState;
        ThreadPoolTimer clockTimer = null;
        int timeCount;
        DateTime dateTime;

        //CONSTUCTOR
        public MainPage()
        {
            this.InitializeComponent();

            checkBoxState = false;
            clockTimer = ThreadPoolTimer.CreatePeriodicTimer(clockTimer_Tick, TimeSpan.FromMilliseconds(1000));
            timeCount = 0;
            dateTime = DateTime.Now;

            button = new Button();
            warningLight = new WarningLight();
            emotionApi = new EmotionApi();
            webcam = new Webcam();

            //subscription of classes to events
            button.buttonPressed += this.OnButtonPressed;
            button.buttonPressed += warningLight.OnButtonPressed;

            // initialisation of the webcam
            new Action(async () =>
            {
                textBlockInitWebcam.Text = await webcam.initWebcam();
            }).Invoke();
        }

        //METHODS
        private async void clockTimer_Tick(ThreadPoolTimer timer)
        {
            dateTime = dateTime.AddSeconds(1);
            timeCount++;

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => { textBlockTime.Text = dateTime.ToString() + "  timecount : " + timeCount.ToString(); });
        }

        //EVENTS LISTENED AND EXECUTED
        //physical button pressed
        private async void OnButtonPressed(object source, EventArgs e)
        {
            // show the physical button's state on screen
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

            //ask EmotionApi
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () => { textBlockEmotionApi.Text = await emotionApi.MakeRequest("Charlotte"); });

            //take a picture
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () => { textBlockInitWebcam.Text += " " + await webcam.TakePicture(); });
        }
    }
}
