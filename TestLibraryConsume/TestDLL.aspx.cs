using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clickatell.Services.API;
using Clickatell.Services.Data;

namespace TestLibraryConsume
{
    public partial class TestDLL : System.Web.UI.Page
    {
        //instantiate client
        Clickatell.Services.APIClient _apiClient;
        protected void Page_Load(object sender, EventArgs e)
        {

            //To use HTTP, uncomment the _apiClient = new HTTP(new HTTPCredentials("Username", "Password", "APIid")); line and comment the _apiClient = new REST(new RESTCredentials("AuthKey")); line
             
            //HTTP
            //_apiClient = new HTTP(new HTTPCredentials("Username", "Password", "APIid"));

            //REST
            _apiClient = new REST(new RESTCredentials("AuthKey"));

            //GetBalance
            GetBalance();

            //test Authenticate
            var result = _apiClient.Authenticate();
            Response.Write("_apiClient.Authenticate() = " + result.Result + "<br/><br/><br/>");

            //Send SMS
            SendSMS("Message Text Here", new[] { "MSISDN", "MSISDN" });

            //Stop
            StopMessage(new[] { "MessageAPIId", "MessageAPIId" });

            //Get Coverage
            GetCoverage(new[] { "MSISDN", "MSISDN" });

            //Get Message Charge
            MessageCharge(new[] { "MessageAPIId", "MessageAPIId" });

            //Get Message Status
            MessageStatus(new[] { "MessageAPIId", "MessageAPIId" });

            //GetBalance
            GetBalance();
        }

        public void SendSMS(string MessageText, string[] ToMSISDN)
        {
            //send SMS
            SendMessageResponse sendMessageResponse = _apiClient.SendMessage(new SendMessageRequest(MessageText, ToMSISDN));

            //loop through messages for results
            for (int i = 0; i < sendMessageResponse.Messages.Length; i += 1)
            {
                Message message = new Message();
                message = sendMessageResponse.Messages[i];
                Response.Write("Message:" + i.ToString() + "<br/>"
                    + "message.APIMessageID: " + message.APIMessageID + "<br/>"
                    + "message.To: " + message.To + "<br/><br/>");

            }
        }

        public void MessageCharge(string[] APIMessageID)
        {
            //getMessageCharge
            MessageChargeResponse messageChargeResponse = _apiClient.GetMessageCharge(new APIMessageRequest(APIMessageID));
            for (int i = 0; i < messageChargeResponse.MessageCharges.Length; i += 1)
            {
                MessageCharge messageCharge = new MessageCharge();
                messageCharge = messageChargeResponse.MessageCharges[i];
                Response.Write("messageCharge:" + i.ToString() + "<br/>"
                    + "messageCharge.APIMessageID : " + messageCharge.APIMessageID + "<br/>"
                    + "messageCharge.Charge.ToString(): " + messageCharge.Charge.ToString() + "<br/><br/>");

            }

        }

        public void MessageStatus(string[] APIMessageID)
        {
            //getMessageStatus
            MessageStatusResponse messageStatusResponse = _apiClient.GetMessageStatus(new APIMessageRequest(APIMessageID));
            for (int i = 0; i < messageStatusResponse.MessageStatuses.Length; i += 1)
            {
                MessageStatus messageStatus = new MessageStatus();
                messageStatus = messageStatusResponse.MessageStatuses[i];
                Response.Write("messageStatus:" + i.ToString() + "<br/>"
                    + "messageStatus.APIMessageID : " + messageStatus.APIMessageID + "<br/>"
                    + "messageStatus.Status : " + messageStatus.Status + "<br/>"
                    + "messageStatus.Description: " + messageStatus.Description + "<br/><br/>");

            }

        }

        public void StopMessage(string[] APIMessageID)
        {
            MessageStatusResponse messageStatusResponse = _apiClient.StopMessage(new APIMessageRequest(APIMessageID));
            for (int i = 0; i < messageStatusResponse.MessageStatuses.Length; i += 1)
            {
                MessageStatus messageStatus = new MessageStatus();
                messageStatus = messageStatusResponse.MessageStatuses[i];
                Response.Write("messageStatus:" + i.ToString() + "<br/>"
                    + "messageStatus.APIMessageID : " + messageStatus.APIMessageID + "<br/>"
                    + "messageStatus.Status : " + messageStatus.Status + "<br/>"
                    + "messageStatus.Description: " + messageStatus.Description + "<br/><br/>");

            }
        }

        public void GetBalance()
        {
            //GetBalance
            BalanceResponse balanceResponse = _apiClient.GetBalance();
            Response.Write("balanceResponse.Credit.ToString() = " + balanceResponse.Credit.ToString() + "<br/><br/><br/>");
        }

        public void GetCoverage(string[] MSISDN)
        {
            //GetCoverage
            MessagCoverageResponse messagCoverageResponse = _apiClient.GetCoverage(new MessageRequest(MSISDN));
            for (int i = 0; i < messagCoverageResponse.MessageCoverages.Length; i += 1)
            {
                MessageCoverage messageCoverage = new MessageCoverage();
                messageCoverage = messagCoverageResponse.MessageCoverages[i];
                Response.Write("messageCoverage:" + i.ToString() + "<br/>"
                    + "messageCoverage.Destination : " + messageCoverage.Destination + "<br/>"
                    + "messageCoverage.Routable.ToString(): " + messageCoverage.Routable.ToString() + "<br/><br/>");

            }

        }
    }
}