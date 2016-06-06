using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieServiceBot.Model;

namespace MovieServiceBot.Managers
{
    public class ActionManager
    {

        static MovieManager thisMovie;

        public string GetReplyMessage(MovieLUIS userData)
        {
            string replyMessage = "Sorry, I don't understand what you just said. I am still learning.";

            var topScoringIntent = userData.intents[0];
            if (topScoringIntent.intent == "None")
            {
                return replyMessage;
            }

            var actions = topScoringIntent.actions;
            if (actions.Count() > 0)
            {
                var action = actions[0];
                switch (action.name)
                {
                    case "GetRating":
                        replyMessage = GetRating(action.parameters);
                        break;
                    case "GetGenre":
                        replyMessage = GetGenre(action.parameters);
                        break;
                    case "GetActor":
                        replyMessage = GetActor(action.parameters);
                        break;
                    case "GetPlot":
                        replyMessage = GetPlot(action.parameters);
                        break;
                    case "GetDirector":
                        replyMessage = GetDirector(action.parameters);
                        break;
                    case "GetReleaseDate":
                        replyMessage = GetReleaseDate(action.parameters);
                        break;
                    case "GetDuration":
                        replyMessage = GetRunningTime(action.parameters);
                        break;
                    default:
                        replyMessage = "Sorry, I don't have an answer for that currently.";
                        break;
                }
            }
            return replyMessage;
        }

        /// <summary>
        /// Gets the Imdb rating
        /// </summary>
        /// <param name="parameters">Contains the title parameter</param>
        /// <returns>reply with the rating of the title</returns>
        private string GetRating(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieManager currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = $"The imdb rating for {currentMovie.GetMovieTitle()} is {currentMovie.GetImdbRating()}";
            }

            return reply;
        }

        /// <summary>
        /// Gets the Genre of the movie
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>reaply with the Genre of the movie</returns>
        private string GetGenre(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieManager currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMovieGenre();
            }

            return reply;
        }

        private string GetPlot(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieManager currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMoviePlot();
            }

            return reply;
        }

        private string GetRunningTime(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieManager currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMovieRunningTime();
            }

            return reply;
        }

        private string GetReleaseDate(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieManager currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMovieReleaseDate();
            }

            return reply;
        }

        private string GetActor(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieManager currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMovieActor();
            }

            return reply;
        }

        private string GetDirector(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieManager currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMovieDirector();
            }

            return reply;
        }

        private MovieManager GetCurrentMovie(Parameter[] parameters)
        {
            var title = parameters[0];
            if (title == null || title.value == null)
            {
                if (thisMovie == null)
                {
                    return null;
                }
            }
            else
            {
                string searchTitle = title.value[0].entity;
                thisMovie = new MovieManager(searchTitle);
            }

            return thisMovie;
        }
    }
}