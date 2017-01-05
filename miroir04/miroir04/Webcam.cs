using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace miroir04
{
    class Webcam
    {
        //ATTRIBUTS
        public string data { get; set; }

        private MediaCapture mediaCapture;
        private StorageFile photoFile;
        private readonly string PHOTO_FILE_NAME = "photo.jpg";

        //CONSTRUCTOR
        public Webcam()
        {

        }

        //METHODS
        public async Task<string> initWebcam()
        {
            string datas;

            try
            {
                if (mediaCapture != null)
                {
                    mediaCapture.Dispose();
                    mediaCapture = null;
                }

                mediaCapture = new MediaCapture();
                await mediaCapture.InitializeAsync();
                datas = "Initialisation de la webcam ok";
            }
            catch
            {
                datas = "initialisation pas ok - erreur relevee";
            }

            return datas;
        }

        public async Task<string> TakePicture()
        {
            photoFile = await KnownFolders.PicturesLibrary.CreateFileAsync(
                PHOTO_FILE_NAME, CreationCollisionOption.GenerateUniqueName);
            //ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();
            //await mediaCapture.CapturePhotoToStorageFileAsync(imageProperties, photoFile);

            //string statutText = "Take Photo succeeded: " + photoFile.Path;

            //return statutText;
            return "Charlotte";
        }
    }
}
