using System;
using System.Net;

namespace Webserver
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            // Create a listener.
            HttpListener listener = new HttpListener();
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            string prefixes = "http://localhost:8080/../../Content/"; // Fortsätt härifrån
            // Add the prefixes.
            listener.Prefixes.Add(prefixes);
            

            listener.Start();
            Console.WriteLine("Listening...");

            // Note: The GetContext method blocks while waiting for a request.
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;

            // Obtain a response object.
            HttpListenerResponse response = context.Response;

            // Construct a response.
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            // You must close the output stream.
            output.Close();
            listener.Stop();
        }
    }
}
