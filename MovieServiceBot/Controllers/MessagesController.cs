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
                string replyMessage = "Sorry, I don't have an answer for that.";

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
                        default: replyMessage = "Sorry, I cannot answer that currently.";
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

        /// <summary>
        /// Gets the Imdb rating
        /// </summary>
        /// <param name="parameters">Contains the rating, title parameters</param>
        /// <returns>reply with the rating of the title</returns>
        private string GetRating(Parameter[] parameters)
        {
            MovieController thisMovie;
            var rating = parameters[0];
            var title = parameters[1];
            if(!(rating != null && title != null))
            {
                return $"Invalid input";
            }

            Movie search = new Movie();
            search.Title = title.value[0].entity;
            thisMovie = new MovieController(search);

            return thisMovie.GetImdbRating();
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