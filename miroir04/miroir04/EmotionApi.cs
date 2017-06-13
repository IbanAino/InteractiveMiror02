using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;

using System.IO;

namespace miroir04
{
    class EmotionApi
    {
        //ATTRIBUTS

        //CONSTRUCTOR
        public EmotionApi()
        {

        }

        //METHODS
        public async Task<string> MakeRequest(string filePath)
        {
            StorageFile photo = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/portrait.jpeg"));

            // Setup http content using stream of captured photo
            IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
            HttpStreamContent streamContent = new HttpStreamContent(stream);
            streamContent.Headers.ContentType = new Windows.Web.Http.Headers.HttpMediaTypeHeaderValue("application/octet-stream");

            // Setup http request using content
            Uri apiEndPoint = new Uri("https://api.projectoxford.ai/emotion/v1.0/recognize");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiEndPoint);
            request.Content = streamContent;

            // Do an asynchronous POST.           
            string apiKey = "1dd1f4e23a5743139399788aa30a7153";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);
            HttpResponseMessage response = await httpClient.SendRequestAsync(request).AsTask();

            // Read response
            return await response.Content.ReadAsStringAsync();
        }


        // make the request using a bitmap image into parameters
        public async Task<string> MakeRequestBitmap(string filePath)
        {
            /*
            Windows.Storage.StorageFile sampleFile = await Windows.Storage.KnownFolders.DocumentsLibrary.GetFileAsync("photo.jpeg");
            BitmapImage img = new BitmapImage();
            img = await LoadImage(sampleFile);
            */
            //myImage.Source = img;

            //StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //String path = @"C:\Data\Users\DefaultAccount\Pictures\Pictures\photo.jpeg";
            //StorageFile photo = await StorageFile.GetFileFromPathAsync(path);
            //StorageFile photo = await localFolder.GetFileAsync("photo.jpeg");
            /*
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile photo =
                await storageFolder.GetFileAsync("photo.jpeg");
                */

            /*
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            String image = @"Pictures\photo.jpeg";
            StorageFile photo = await localFolder.GetFileAsync(image);
            */

            //StorageFile photo = await ApplicationData.Current.LocalFolder.TryGetItemAsync("photo.jpeg") as StorageFile;

            //StorageFile photo = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/portrait.jpeg"));

            StorageFile photo = await KnownFolders.PicturesLibrary.GetFileAsync("photo.jpeg");

            // Setup http content using stream of captured photo
            IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
            HttpStreamContent streamContent = new HttpStreamContent(stream);
            streamContent.Headers.ContentType = new Windows.Web.Http.Headers.HttpMediaTypeHeaderValue("application/octet-stream");

            // Setup http request using content
            Uri apiEndPoint = new Uri("https://api.projectoxford.ai/emotion/v1.0/recognize");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiEndPoint);
            request.Content = streamContent;

            // Do an asynchronous POST.           
            string apiKey = "1dd1f4e23a5743139399788aa30a7153";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);
            HttpResponseMessage response = await httpClient.SendRequestAsync(request).AsTask();

            // Read response
            return await response.Content.ReadAsStringAsync();
        }

        private static async Task<BitmapImage> LoadImage(StorageFile file)
        {
            BitmapImage bitmapImage = new BitmapImage();
            FileRandomAccessStream stream = (FileRandomAccessStream)await file.OpenAsync(FileAccessMode.Read);

            bitmapImage.SetSource(stream);

            return bitmapImage;
        }

    }
}
