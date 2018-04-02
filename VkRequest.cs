using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace vkAnalysis
{
    abstract class VkRequest
    {
        public string url { get; set; }
        public string respond { get; set; }
        public string status { get; set; }
        public string parameters { get; set; }
        public string access_token { get; set; }
        public string mUser { get; set; }
        public Boolean useToken { get; set; }
        public string token { get; set; }
        public string method { get; set; }
        public const string apiVersion = "5.69";
        abstract public void SaveFile();
        abstract public Object Deserialize();
        public string GetResponse()
        {
            Thread.Sleep(500);

            //string URL = string.Format("https://api.vk.com/method/{0}", method);
            string formParams;
            if (useToken == true)
            {
                formParams = string.Format("access_token={0}&v={1}", token, apiVersion);
            }
            else
            {
                formParams = string.Format("v={0}", apiVersion);
            }

            formParams = "?" + formParams;

            if (parameters != null)
            {
                formParams = formParams + "&" + parameters;
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url + formParams);
            req.ContentType = "text/html";
            WebResponse getResponse = null;

            Boolean isOver = false;
            while (!isOver)
            {
                try
                {
                    getResponse = req.GetResponse();
                    isOver = true;
                }
                catch (System.Net.WebException e)
                {
                    isOver = false;

                }
            }

            using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            {
                respond = sr.ReadToEnd();
            }

            return respond;
        }
    }

    class OAuth : VkRequest
    {

        public string grantType { get; set; }
        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string scope { get; set; }

        public OAuth(string vClientId, string vClientSecret, string vUsername, string vPassword, string vScope, string vGrantType, string vUrl)
        {
            clientId = vClientId;
            clientSecret = vClientSecret;
            username = vUsername;
            password = vPassword;
            scope = vScope;
            url = vUrl;
            grantType = vGrantType;
            parameters = string.Format("grant_type={0}&client_id={1}&client_secret={2}&username={3}&password={4}&scope={5}",
                grantType, clientId, clientSecret, username, password, scope);
        }

        override public void SaveFile()
        {

        }

        override public Object Deserialize()
        {
            var jss = new JavaScriptSerializer();
            var vkAuthData = jss.Deserialize<Dictionary<string, string>>(base.respond);
            var accessToken = "";
            string userId;
            vkAuthData.TryGetValue("access_token", out accessToken);
            vkAuthData.TryGetValue("user_id", out userId);
            if(userId != null)
            {
                base.mUser = userId;
            }
            return accessToken;

        }

        public void getResponseParent()
        {
            base.useToken = false;
            base.GetResponse();
            base.token = Deserialize().ToString();
        }
    }

    class GetFriends : VkRequest
    {

        public string grantType { get; set; }
        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string scope { get; set; }

        public GetFriends(string vUrl, string vToken, string vMUser)
        {
            url = vUrl;
            useToken = true;
            token = vToken;
            mUser = vMUser;
        }
        override public void SaveFile()
        {

        }

        override public Object Deserialize()
        {
            var jss = new JavaScriptSerializer();
            var vkFriendsOfmUser = new RootObject();

            try
            {
                vkFriendsOfmUser = jss.Deserialize<RootObject>(respond);
            }
            catch
            {
                //Log("Friends request error happened", "log.txt");
            }
            string[] mUserFriends = vkFriendsOfmUser.response.items;

            return mUserFriends;

        }
    }
}