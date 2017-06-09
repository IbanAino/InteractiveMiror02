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
        MediaCapture mediaCapture;
        private IStorageFile photoFile;
        private string PHOTO_FILE_NAME = "phpto.jpeg";


        //CONSTRUCTOR
        public Webcam()
        {

        }

        //METHODS
        
        public async Task<string> initWebcam()
        {
            //Video and Audio is initialized by default  
            mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();

            return "webcam initialized";
        }

        /*
        public async Task<string> TakePicture()
        {
            photoFile = await KnownFolders.PicturesLibrary.CreateFileAsync(
                            PHOTO_FILE_NAME, CreationCollisionOption.GenerateUniqueName);
            ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();

            //await mediaCapture.CapturePhotoToStorageFileAsync(imageProperties, photoFile);

            
            IRandomAccessStream photoStream = await photoFile.OpenReadAsync();
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(photoStream);
            
            //captureImage.Source = bitmap;

            return "Photo prise";
        }
        */



    }
}
