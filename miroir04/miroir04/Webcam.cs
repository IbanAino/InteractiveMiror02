using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace miroir04
{
    class Webcam
    {
        //ATTRIBUTS
        public string data { get; set; }

        private MediaCapture mediaCapture;
        private StorageFile photoFile;
        private readonly string PHOTO_FILE_NAME = "photo.jpg";

        Windows.Media.Capture.MediaCapture captureManager;

        //CONSTRUCTOR
        public Webcam()
        {

        }

        //METHODS
        public async Task<string> initWebcam()
        {
            try
            {
                if (mediaCapture != null)
                {
                    mediaCapture.Dispose();
                    mediaCapture = null;
                }

                mediaCapture = new MediaCapture();
                await mediaCapture.InitializeAsync();

                return "Initialisation de la webcam ok";
            }
            catch
            {
                return "initialisation webcam failed - erreur relevee";
            }
        }

        public async Task<string> TakePicture()
        {
            photoFile = await KnownFolders.PicturesLibrary.CreateFileAsync(
                PHOTO_FILE_NAME, CreationCollisionOption.GenerateUniqueName);
            ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();

            // create storage file in local app storage
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                "TestPhoto.jpg",
                CreationCollisionOption.GenerateUniqueName);

            try
            {
                await mediaCapture.CapturePhotoToStorageFileAsync(imageProperties, file);
                BitmapImage bmpImage = new BitmapImage(new Uri(file.Path));

                return " - photo prise, et enregistrée à : " + photoFile.Path;
            }
            catch
            {
                return "ERROR : photo non prise";
            }
        }
    }
}
