using System.IO;
using System.Net;
using MovieServiceBot.Model;
using System.Collections;

namespace MovieServiceBot.Services
{
    public class OmdbMovieService
    {
        const string BASE_URL = "http://www.omdbapi.com/?";

        private string MakeWebRequest(string query)
        {
            string apiUrl = string.Concat(BASE_URL, query);
            string content = string.Empty;

            try
            {
                WebRequest request = WebRequest.Create(apiUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                content = reader.ReadToEnd();
            }
            catch (System.Exception ex)
            {

            }           

            return content;
        }

        public string GetMovieByTitle(Hashtable inputs)
        {            
            string title = inputs["title"].ToString();
            string queryString = string.Concat("t=", title);

            return MakeWebRequest(queryString);
        }
    }
}