﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
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

        //webcam attributs
        MediaCapture mediaCapture;
        private IStorageFile photoFile;
        private string PHOTO_FILE_NAME = "photo.jpeg";

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
                // check the checkBox on the screen
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () => { checkBoxStatePhysicalButton.SetValue(CheckBox.IsCheckedProperty, true); });
                checkBoxState = true;


                //take a picture
                await initWebcam();

                String filePath = null;
                
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    async () => {
                        filePath = await TakePictureBitmap();
                        textBlockInitWebcam.Text += filePath;
                    });

                //ask EmotionApi
                await Task.Delay(10000);

                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    async () => { textBlockEmotionApi.Text = await emotionApi.MakeRequestBitmap(filePath); });
            }
            else
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () => { checkBoxStatePhysicalButton.SetValue(CheckBox.IsCheckedProperty, false); });
                checkBoxState = false;
            }
        }

        //WEBCAM
        public async Task<string> initWebcam()
        {
            //Video and Audio is initialized by default  
            mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();

            return "webcam initialized";
        }
        
        public async Task<String> TakePictureBitmap()
        {
            photoFile = await KnownFolders.PicturesLibrary.CreateFileAsync(
                            PHOTO_FILE_NAME, CreationCollisionOption.ReplaceExisting);
            ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();
            await mediaCapture.CapturePhotoToStorageFileAsync(imageProperties, photoFile);



            StorageFile photo2 = await KnownFolders.PicturesLibrary.GetFileAsync("photo.jpeg");

            
            IRandomAccessStream photoStream = await photo2.OpenReadAsync();
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(photoStream);
            captureImage.Source = bitmap;

            photoStream.Dispose();
            
            return "photo prise";
        }
        
    }
}
