using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace miroir04
{
    class EmotionApi
    {
        //ATTRIBUTS
        private int anger;
        private int contempt;
        private int disgust;
        private int fear;
        private int happiness;
        private int neutral;
        private int sadness;
        private int surprise;

        //CONSTRUCTOR
        public EmotionApi()
        {
            //Webcam webcam = new Webcam();

        }

        //METHODS
        public string VoidMethod()
        {
            return "42";
        }

        public async Task<int[]> CheckEmotions(string picturePath)
        {
            string charlotte = "Charlotte";
            string responseToRequest = await MakeRequest(charlotte);

            int[] emotions = new int[] { anger, contempt, disgust, fear, happiness, neutral, sadness, surprise };
            return emotions;
        }

        public async Task<string> MakeRequest(string filePath)
        {

            //Webcam webcam = new Webcam();
            //string initialisationCamera = await webcam.initVideo();
            //string prisePhoto = await webcam.takePhoto();

            // Capture image from camera
            /*CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(500, 500);
            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);*/
            StorageFile photo = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/portrait.jpeg"));
            //StorageFile photo = await webcam.CaptureIamge();

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
            string responseContent = await response.Content.ReadAsStringAsync();


            // Parse the Json code
            //var myJsonString = "Charlotte : [{firstObject: {Id: \"42\", age: \"22\"}, secondObject: {nbr: \"37\", chiffre : \"27\"}}]";
            //var myJsonString = "{Charlotte: [\"premier\", \"deuxieme\"]}";
            //var myJsonString = "{Charlotte: [premier: {\"la\",\"li\"}, deuxieme: {\"lou\",\"le\"}]}";
            //MyClass02 items04 = JsonConvert.DeserializeObject<MyClass02>(myJsonString);


            //var myJsonString02 = "{\"Name\": \"Apple\",\"ExpiryDate\": \"2008-12-28T00:00:00\",\"Price\": \"3.99\",\"Sizes\": [\"Small\",\"Medium\",\"Large\"]}";
            //RootObject items03 = JsonConvert.DeserializeObject<RootObject>(myJsonString02);

            //var items02 = JsonConvert.DeserializeObject<List<MyClass02>>(myJsonString);

            //var result = JsonConvert.DeserializeObject<List<Dictionary<string, Dictionary<string, string>>>>(myJsonString);

            //items03.Name

            //Array id = result.ToArray();

            //string age = result.Find(42);
            //var items01 = JsonConvert.DeserializeObject<List<MyClass01>>(responseContent);

            //dynamic stuff = JsonConvert.DeserializeObject(myJsonString);
            //string age = stuff.firstObject.age;

            //string firstItem01 = items01.First().top.ToString();
            //var firstItem02 = items02.first
            //var result = items02.resu
            //Array lala = items02.First().firsObject[1];
            //string retour = lala.GetValue(1).ToString();

            // Display response
            //string retour = firstItem02 + "-------" + responseContent;
            //return items03.Name.ToString();
            //responseContent = "{Charlotte: " + responseContent + "}";
            //return items03.Sizes[2].ToString() + "-------" + responseContent;
            return responseContent;
        }
    }

    public class MyClass02
    {
        public MyClass02bis[] Charlotte = new MyClass02bis[2];
    }
    public class MyClass02bis
    {
        public string[] o = new string[2];
    }

    class RootObject
    {
        public string Name;
        public DateTime ExpiryDate;
        public string Price;
        public string[] Sizes = new string[3];
    }

    public class MyClass01
    {
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
        public int heigth { get; set; }
    }
}
