namespace RecepiesApp.Services.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using PubNubMessaging.Core;

    public class Notifier : INotifier
    {
        private const string PubKey = "pub-c-e9356020-7135-4618-b2fc-b9cac8473e7a";
        private const string SubKey = "sub-c-f019d17c-3f3c-11e4-9bf1-02ee2ddab7fe";
        private string response;

        public string Notify(string user, object message)
        {

            var pubnub = new Pubnub(PubKey, SubKey);
            pubnub.Publish(user, message, SuccessCB, ErrorCB);
            return this.response;
        }

        private void SuccessCB(object obj)
        {
            this.response = obj.ToString();
        }

        private void ErrorCB(PubnubClientError obj)
        {
            this.response = obj.DetailedDotNetException.Message;
        }
    }
}