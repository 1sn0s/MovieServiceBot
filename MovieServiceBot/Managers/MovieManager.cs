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
            return $"{currentMovie.Title}";
        }

        public string GetImdbRating()
        {
            return $"{currentMovie.imdbRating}";
        }

        public string GetMovieGenre()
        {
            return $"{currentMovie.Genre}";
        }

        public string GetMoviePlot()
        {
            return $"{currentMovie.Plot}";
        }

        public string GetMovieRunningTime()
        {
            return $"{currentMovie.Runtime}";
        }

        public string GetMovieReleaseDate()
        {
            return $"{currentMovie.Released}";
        }

        public string GetMovieActor()
        {
            return $"{currentMovie.Actors}";
        }

        public string GetMovieDirector()
        {
            return $"{currentMovie.Director}";
        }
    }
}