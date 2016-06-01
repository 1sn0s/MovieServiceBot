using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MovieServiceBot.Controllers
{
    public class LUIS
    {
        private const string luisUri = "https://api.projectoxford.ai/luis/v1/application?id=647de274-1012-47a7-ae29-ba00608fa8fa&subscription-key=7f730da314ac448d9061ec6280fb1757&q=";
        public static async Task<MovieLUIS> ProcessuserInput(string userInput)
        {
            using (var client = new HttpClient())
            {
                string uri = string.Concat(luisUri, userInput);
                HttpResponseMessage msg = await client.GetAsync(uri);

                if (msg.IsSuccessStatusCode)
                {
                    var jsonResponse = await msg.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<MovieLUIS>(jsonResponse);
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }
    }


    public class MovieLUIS
    {
        public string query { get; set; }
        public Intents[] intents { get; set; }
        public Entity[] entities { get; set; }
    }

    public class Intents
    {
        public string intent { get; set; }
        public float score { get; set; }
        public Action[] actions { get; set; }
    }

    public class Action
    {
        public bool triggered { get; set; }
        public string name { get; set; }
        public Parameter[] parameters { get; set; }
    }

    public class Parameter
    {
        public string name { get; set; }
        public bool required { get; set; }
        public Value[] value { get; set; }
    }

    public class Value
    {
        public string entity { get; set; }
        public string type { get; set; }
        public float score { get; set; }
    }

    public class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }

}