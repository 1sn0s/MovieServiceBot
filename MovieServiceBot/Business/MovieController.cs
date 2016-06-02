using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace MovieServiceBot.Controllers
{
    public class MovieController
    {
        Movie currentMovie;
        const string url = "http://www.omdbapi.com/?";

        public MovieController(Movie movie)
        {
            String title = movie.Title;
            string apiUrl = string.Concat(url, "t=", title);

            WebRequest request = WebRequest.Create(apiUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string content = reader.ReadToEnd();

            currentMovie = JsonConvert.DeserializeObject<Movie>(content);
        }

        public string GetImdbRating()
        {
            return $"The imdb rating for {currentMovie.Title} is {currentMovie.imdbRating}";
        }

        public string GetMovieGenre()
        {
            return $"{currentMovie.Genre}";
        }
    }
}