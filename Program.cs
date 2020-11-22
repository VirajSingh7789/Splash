using System;
using System.IO;
using System.IO.Ports;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TrashRecycle
{
    public static class Program
    {
        public static void Main()
        {
            string imageFilePath = "C:\\Users\\virag\\Desktop\\HackGSU\\frame0.jpg";
            MakePredictionRequest(imageFilePath).Wait();
            //string imageFilePath = "C:\\Users\\virag\\Desktop\\HackGSU\\trash\\yep\\defabottle2.png";
            //MakePredictionRequest(imageFilePath).Wait();
            //while (x < 5)
            //{
            //    MakePredictionRequest(imageFilePath).Wait();
            //    Task.Delay(5000).Wait();
            //    x = x + 1;
            //    imageFilePath = string.Format("c:\\users\\virag\\desktop\\hackgsu\\frame{0}.jpg", x);
            //}
            Console.WriteLine("\n\nHit ENTER to exit...");
            Console.ReadLine();
        }

        public static async Task MakePredictionRequest(string imageFilePath)
        {
            var client = new HttpClient();
            int recycle = 1;
            SerialPort port;

            // Request headers - replace this example key with your valid Prediction-Key.
            client.DefaultRequestHeaders.Add("Prediction-Key", "0f555d505ca743328e7a976a5e4683af");

            // Prediction URL - replace this example URL with your valid Prediction URL.
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/da3704c4-e739-4d54-8dbd-a0c2b557be61/classify/iterations/Trash%20or%20Recycle/image";

            HttpResponseMessage response;

            // Request body. Try this sample with a locally stored image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);
                string longass = await response.Content.ReadAsStringAsync();
                string subOne = longass.Substring(longass.IndexOf("tagName"));
                string subTwo = subOne.Substring(subOne.IndexOf(":"));
                string subThree = subTwo.Substring(subTwo.IndexOf(":") + 1,subTwo.IndexOf("}") - 1);
                Console.WriteLine(subThree);
                if(subThree.Equals("\"Trash\"") || subThree.Equals("\"Glass\""))
                    recycle = 0;
                Console.WriteLine(recycle);
                port = new SerialPort("COM3",9600);
                port.Open();
                port.WriteLine(recycle.ToString());
                port.Close();
            }
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }

}
//.getValue(0)["tagName"]  
//await response.Content.ReadAsStringAsync()