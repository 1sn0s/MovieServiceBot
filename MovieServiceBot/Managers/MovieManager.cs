using Newtonsoft.Json;
using MovieServiceBot.Model;
using MovieServiceBot.Services;
using System.Collections;

namespace MovieServiceBot.Managers
{
    public class MovieManager
    {
        Movie currentMovie;        

        public MovieManager(string title)
        {
            string movieDataJSON = GetMovieByTitle(title);
            currentMovie = DeserializeMovie(movieDataJSON);
        }

        private Movie DeserializeMovie(string json)
        {
            return JsonConvert.DeserializeObject<Movie>(json);
        }

        private string GetMovieByTitle(string title)
        {
            OmdbMovieService omdbService = new OmdbMovieService();
            Hashtable parameters = new Hashtable();
            parameters.Add("title", title);
            string content = omdbService.GetMovieByTitle(parameters);
            return content;
        }

        public string GetMovieTitle()
        {
            return currentMovie.Title.ToString();
        }

        public string GetImdbRating()
        {
            return currentMovie.imdbRating.ToString();
        }

        public string GetMovieGenre()
        {
            return currentMovie.Genre.ToString();
        }

        public string GetMoviePlot()
        {
            return currentMovie.Plot.ToString();
        }

        public string GetMovieRunningTime()
        {
            return currentMovie.Runtime.ToString();
        }

        public string GetMovieReleaseDate()
        {
            return currentMovie.Released.ToString();
        }

        public string GetMovieActor()
        {
            return currentMovie.Actors.ToString();
        }

        public string GetMovieDirector()
        {
            return currentMovie.Director.ToString();
        }
    }
}