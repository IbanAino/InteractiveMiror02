using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
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
            /*new Action(async () =>
            {
                data = await initWebcam();
            }).Invoke();*/
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

    }
}
