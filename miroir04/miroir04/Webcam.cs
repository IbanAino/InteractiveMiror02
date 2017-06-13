using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;


namespace miroir04
{
    class Webcam
    {
        //ATTRIBUTS
        bool webcamInitialized = false;
        MediaCapture mediaCapture;

        //CONSTRUCTOR
        public Webcam()
        {
            initWebcam();
        }

        //METHODS
        public async Task<string> initWebcam()
        {
            //Video and Audio is initialized by default  
            mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();
            webcamInitialized = true;

            return "webcam initialized";
        }

        public async Task<String> TakePicture()
        {
            IStorageFile photoFile = await KnownFolders.PicturesLibrary.CreateFileAsync(
                "photo.jpeg", CreationCollisionOption.ReplaceExisting);
            ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();
            await mediaCapture.CapturePhotoToStorageFileAsync(imageProperties, photoFile);

            /*
            StorageFile photo2 = await KnownFolders.PicturesLibrary.GetFileAsync("photo.jpeg");
            IRandomAccessStream photoStream = await photo2.OpenReadAsync();
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(photoStream);
            captureImage.Source = bitmap;
            photoStream.Dispose();
            */

            return "photo prise";
        }
    }
}
