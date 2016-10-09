using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using MovieServiceBot.Managers;
using MovieServiceBot.Model;
using System.Net.Http;
using System.Net;
using System;

namespace MovieServiceBot
{
    //[BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to itMana
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity message)
        {
            var response = Request.CreateResponse(HttpStatusCode.Accepted);
            ConnectorClient client = new ConnectorClient(new Uri(message.ServiceUrl));
            if (message.Type == ActivityTypes.Message)
            {
                MovieLUIS userRequest = await LUIS.ProcessuserInput(message.Text);                
                string replyMessage = string.Empty;                
                if(userRequest == null)
                {
                    replyMessage = "My friend Luis is sleeping. I need him to wake up.";
                } else
                {
                    ActionManager actionManager = new ActionManager();
                    replyMessage = await actionManager.GetReplyMessage(userRequest);
                }
                //message.Language = "en";
                Activity r = message.CreateReply(replyMessage);
                await client.Conversations.ReplyToActivityAsync(r);
                //return message.CreateReplyMessage(replyMessage);
            }
            else
            {
                //return HandleSystemMessage(message);
                await client.Conversations.ReplyToActivityAsync(HandleSystemMessage(message));
            }            
            return response;
        }
        

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.Ping)
            {
                Activity reply = message.CreateReply();
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