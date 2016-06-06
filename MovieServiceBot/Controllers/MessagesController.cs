﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using MovieServiceBot.Managers;
using MovieServiceBot.Model;

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
                MovieLUIS userRequest = await LUIS.ProcessuserInput(message.Text);                
                string replyMessage = string.Empty;

                if(userRequest == null)
                {
                    replyMessage = "My friend Luis is sleeping. I need him to wake up.";
                } else
                {
                    ActionManager actionManager = new ActionManager();
                    replyMessage = actionManager.GetReplyMessage(userRequest);
                }               
                
                return message.CreateReplyMessage(replyMessage);
            }
            else
            {
                return HandleSystemMessage(message);
            }
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