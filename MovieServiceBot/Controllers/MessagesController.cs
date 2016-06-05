using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using MovieServiceBot.Controllers;

namespace MovieServiceBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                MovieLUIS userData = await LUIS.ProcessuserInput(message.Text);                
                string replyMessage = "Sorry, I don't understand what you just said. I am still learning.";

                if(userData == null)
                {
                    replyMessage = "My friend Luis is sleeping. He needs to wake up.";
                    message.CreateReplyMessage(replyMessage);
                }
                var topScoringIntent = userData.intents[0];
                if (topScoringIntent.intent == "None")
                {
                    return message.CreateReplyMessage(replyMessage);
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
                        default: replyMessage = "Sorry, I don't have an answer for that currently.";
                            break;
                    }
                }
                return message.CreateReplyMessage(replyMessage);
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }


        static MovieController thisMovie;

        /// <summary>
        /// Gets the Imdb rating
        /// </summary>
        /// <param name="parameters">Contains the title parameter</param>
        /// <returns>reply with the rating of the title</returns>
        private string GetRating(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieController currentMovie = GetCurrentMovie(parameters);
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
            MovieController currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMovieGenre();
            }

            return reply;
        }

        private string GetPlot(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieController currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMoviePlot();
            }

            return reply;
        }

        private string GetRunningTime(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieController currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMovieRunningTime();
            }

            return reply;
        }

        private string GetReleaseDate(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieController currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMovieReleaseDate();
            }

            return reply;
        }

        private string GetActor(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieController currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMovieActor();
            }

            return reply;
        }

        private string GetDirector(Parameter[] parameters)
        {
            string reply = "Sorry, I don't understand that.";
            MovieController currentMovie = GetCurrentMovie(parameters);
            if (null != currentMovie)
            {
                reply = currentMovie.GetMovieDirector();
            }

            return reply;
        }

        private MovieController GetCurrentMovie(Parameter[] parameters)
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
                Movie search = new Movie();
                search.Title = title.value[0].entity;
                thisMovie = new MovieController(search);
            }

            return thisMovie;
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}