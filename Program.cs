using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Threading;
using CsvHelper;

namespace vkAnalysis
{
    public class jsResponse
    {
        public string responseBody;
    }

    class Program
    {
        static void Main(string[] args)
        {

            /*if (File.Exists("Likes.csv"))
            {
                File.Delete("Likes.csv");
            }

            if (File.Exists("Friends.csv"))
            {
                File.Delete("Friends.csv");
            }
            if (File.Exists("Posts.csv"))
            {
                File.Delete("Posts.csv");
            }

            Console.WriteLine("Files were deleted");

            Log("Files were deleted", "log.txt");

            String oauthUrl = "https://oauth.vk.com/authorize?client_id=6289594&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.69&state=123456";

            String urlFormLogin = "https://vk.com/login?act";
            String respond = login(urlFormLogin, oauthUrl);*/

            OAuth oauth = new OAuth("3697615", "AlVXZFMUqyrnABp8ncuU", "89020489767", "Otsosi123321", "8194", "password", "https://oauth.vk.com/token");
            oauth.getResponseParent();

            List<FlatPost> flatPostUser = getAllPostLikeFriendmUser(Convert.ToInt64(oauth.mUser), oauth.access_token);
            writePostsFile(flatPostUser, Convert.ToInt64(oauth.mUser));

            /*GetFriends friendsReq = new GetFriends("https://api.vk.com/method/friends.get", oauth.token, oauth.mUser);
            friendsReq.GetResponse();
            string[] mUserFriends = (string[])friendsReq.Deserialize();*/
        }

        public static string login(string formURL, string oauthUrl)
        {

            /*string URLauth = string.Format("https://oauth.vk.com/token?grant_type={0}&client_id={1}&client_secret={2}&username={3}&password={4}&scope={5}"
                , grant_type, client_id, client_secret, username, password, scope);

            string pageSource;
            WebRequest getRequest = WebRequest.Create(URLauth);
            getRequest.ContentType = "text/html";

            WebResponse getResponse = getRequest.GetResponse();
            using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            {
                pageSource = sr.ReadToEnd();
            }


            Log("Token request:" + pageSource, "log.txt");

            var jss = new JavaScriptSerializer();
            var vkAuthData = jss.Deserialize<Dictionary<string, string>>(pageSource);

            string access_token;
            if (!vkAuthData.TryGetValue("access_token", out access_token))
            {
                return null;
            }

            string user_id;
            if (!vkAuthData.TryGetValue("user_id", out user_id))
            {
                return null;
            }
            */
            //getDatabaseEntities(access_token);


            //Find friends of the main user
            /*string parameters = "user_id=" + user_id;
            string friendsRespond = getMethodData("friends.get", parameters, access_token, "5.69", "yes");

            var vkFriendsOfmUser = new RootObject();

            Log("Friends request:" + friendsRespond, "log.txt");

            try
            {
                vkFriendsOfmUser = jss.Deserialize<RootObject>(friendsRespond);
            }
            catch
            {
                Log("Friends request error happened", "log.txt");
            }
            string[] mUserFriends = vkFriendsOfmUser.response.items;

            long[] blackListUsers = new long[] { 654744, 677563, 778979, 915588, 1139002, 1641744, 1708797, 1743686, 1969305, 2008650
            , 2146774, 2154065, 2225313, 2320600, 2420112, 2649801, 2697192, 2730570, 2737078, 2754273, 3031572, 3051852, 3497616, 3613954, 3619933, 3627544,
            3681689, 3685090, 3892368, 4042095, 4218379, 4230915, 4287327, 4338025, 4404886, 4750739, 4954844, 5173669, 5249677, 5386314, 5390870, 5560844, 5715664
            , 5740814, 5962635, 6092338, 6104996, 6318700, 6347644, 6776711, 6834232, 6882206, 7179477, 7217610, 7358682, 7374652, 7445449, 7494722, 7548356
            , 7742288, 7818480, 7912658, 8524712, 8598252, 8635995, 8665597};
            */
            /*for (int i = 0; i < mUserFriends.Count(); i++)
            {
                if (Convert.ToInt64(mUserFriends[i]) > 28600575)
                {
                    List<FlatPost> flatPostUser = getAllPostLikeFriendmUser(Convert.ToInt64(mUserFriends[i]), access_token);
                    writePostsFile(flatPostUser, Convert.ToInt64(mUserFriends[i]));
                }
            }
            //add main user in scope of processing
            List<FlatPost> flatPostUser = getAllPostLikeFriendmUser(Convert.ToInt64(user_id), access_token);
            writePostsFile(flatPostUser, Convert.ToInt64(user_id));

            return pageSource;*/
            return "";
        }

        public static void getDatabaseEntities(string access_token)
        {
            string parameters = "need_all=1&count=1000";
            string citiesRespond = getMethodData("database.getCountries", parameters, access_token, "5.69", "yes");


        }
        public static List<FlatPost> getAllPostLikeFriendmUser(long mUser, string access_token)
        {
            var jss = new JavaScriptSerializer();
            int count = 1;
            int iteration = 0;
            int offset = 0;
            int itemsCount = 100;
            var vkPostsOfmUser = new RootPostsObject();
            List<RootPostsObject> vkAllPostsOfmUser = new List<RootPostsObject>();
            List<Items[]> vkAllPostsItemsOfmUser = new List<Items[]>();


            List<FlatPost> flatPost = new List<FlatPost>();

            string parameters = "user_id=" + mUser;
            string friendsRespond = getMethodData("friends.get", parameters, access_token, "5.69", "yes");

            if (friendsRespond.Contains("error_code"))
            {
                return null;
            }

            Log("friends request:" + friendsRespond, "log.txt");

            var vkFriendsOfmUser = new RootObject();

            try
            {
                vkFriendsOfmUser = jss.Deserialize<RootObject>(friendsRespond);
            }
            catch
            {
                Log("Friends request error happened", "log.txt");
            }

            string[] mUserFriends = vkFriendsOfmUser.response.items;

            Console.WriteLine("mUserFriends count={0}", mUserFriends.Count());

            while (count > 0 && itemsCount >= 100)
            {

                List<PostPerUser> postUser = new List<PostPerUser>();
                Console.WriteLine("mUser={0}", mUser);
                Console.WriteLine("iteration={0}", iteration);


                Log("mUser:" + mUser, "log.txt");
                Log("iteration:" + iteration, "log.txt");

                parameters = "owner_id=" + mUser + "&count=100&offset=" + (iteration * 100).ToString();
                string wallRespond = getMethodData("wall.get", parameters, access_token, "5.69", "yes");

                Log("Wall request:" + wallRespond, "log.txt");


                /*try
                {
                    vkPostsOfmUser = jss.Deserialize<RootPostsObject>(wallRespond);
                }
                catch
                {
                    Log("Posts request error happened", "log.txt");
                }*/


                //vkAllPostsOfmUser.Add(vkPostsOfmUser);
                //vkAllPostsItemsOfmUser.Add(vkPostsOfmUser.response.items);

                /*for (int i = 0; i < vkPostsOfmUser.response.items.Count(); i++)
                {
                    postUser.Add(new PostPerUser(vkPostsOfmUser.response.items.ElementAt(i).id, mUser, vkPostsOfmUser.response.items.ElementAt(i).likes.count));
                    flatPost.Add(new FlatPost(vkPostsOfmUser.response.items.ElementAt(i).id, vkPostsOfmUser.response.items.ElementAt(i).from_id
                                , vkPostsOfmUser.response.items.ElementAt(i).owner_id, vkPostsOfmUser.response.items.ElementAt(i).date
                                , vkPostsOfmUser.response.items.ElementAt(i).text, vkPostsOfmUser.response.items.ElementAt(i).likes.count));
                }*/

                itemsCount = vkPostsOfmUser.response.items.Count();
                count = vkPostsOfmUser.response.count;
                offset = iteration * 100;
                iteration++;

                //save posts into a file 

                // get Likes for each Post for a current user

                //getAllLikes(mUser, access_token, postUser);

            }

            if(mUserFriends.Count() > 99)
            {
                int countTot = mUserFriends.Count();
                decimal loopsDec = mUserFriends.Count() / 100;
                int loops = Convert.ToInt32(Math.Truncate(loopsDec));

                //List<string[]> mUserFriendsList = new List<string[]>();

                for (int i = 0; i < loops; i++ ){
                    string[] mUserFriendsList = new string[100];
                    int end = Math.Min(countTot, (i * 100) + 100);
                    int counter = 0;
                    for (int j = i * 100; j < end; j++)
                    {
                        mUserFriendsList[counter] = mUserFriends[j];
                        counter++;
                    }
                    getAllFriends(mUser, mUserFriendsList, access_token);
                }
            }
            else
            {
                getAllFriends(mUser, mUserFriends, access_token);
            }

            return flatPost;

        }

        public static void writePostsFile(List<FlatPost> flatPost, long mUser) {
            if(flatPost == null)
            {
                Console.WriteLine("Empty posts object for user={0}", mUser);
                return;
            }
            FileStream fs2 = null;
            try
            {
                using (fs2 = new FileStream(@"Posts.csv", FileMode.Append))
                {
                    using (var writer2 = new StreamWriter(fs2))
                    {
                        using (CsvWriter csvWriter2 = new CsvWriter(writer2))
                        {
                            csvWriter2.Configuration.QuoteAllFields = true;

                            csvWriter2.WriteRecords(flatPost);
                        }
}
                }

            }
            catch(System.IO.IOException IOEx)
            {
            }
            finally
            {
                if (fs2 != null)
                {
                    fs2.Close();
                    using (fs2 = new FileStream(@"Posts.csv", FileMode.Append))
                    {
                        using (var writer2 = new StreamWriter(fs2))
                        {
                            using (CsvWriter csvWriter2 = new CsvWriter(writer2))
                            {
                                csvWriter2.Configuration.QuoteAllFields = true;

                                csvWriter2.WriteRecords(flatPost);
                            }
                        }
                    }
                }
            }
        }

        public static void getAllLikes(long mUser, string access_token, List<PostPerUser> postUser)
        {
            

            var jss = new JavaScriptSerializer();
            List<PostLikeUser> postLikeUser = new List<PostLikeUser>();

            int startFrom = 0;
            if(mUser == 3613954)
            {
                startFrom = postUser.FindIndex(a => a.postId <= 1589);
            }

            if(startFrom == -1)
            {
                return;
            }

            for (int i = startFrom; i < postUser.Count(); i++)
            {
                if (postUser[i].likes > 0)
                {
                    string parameters = "user_id=" + postUser[i].userId + "&count=100&type=post&item_id=" + postUser[i].postId + "&owner_id=" + mUser;
                    string likeUserRespond = getMethodData("likes.getList", parameters, access_token, "5.69", "yes");
                    var vkLikesOfmUser = new RootObject();

                    try
                    {
                        vkLikesOfmUser = jss.Deserialize<RootObject>(likeUserRespond);
                    }
                    catch
                    {
                        Log("Likes request error happened", "log.txt");
                    }

                    int itemCount = vkLikesOfmUser.response.items.Count();

                    for (int j = 0; j < itemCount; j++)
                    {
                        postLikeUser.Add(new PostLikeUser(postUser[i].userId, postUser[i].postId, Convert.ToInt64(vkLikesOfmUser.response.items[j])));
                    }
                }
                else
                {
                    Console.WriteLine("mUser={0}, comment {1} has no likes - deleted", mUser, postUser[i].postId);
                }

            }

            FileStream fs = null;
            try
            {
                using (fs = new FileStream("Likes.csv", FileMode.Append))
                {
                    using (var writer = new StreamWriter(fs))
                    {
                        using (CsvWriter csvWriter = new CsvWriter(writer))
                        {
                            csvWriter.Configuration.QuoteAllFields = true;
                            csvWriter.WriteRecords(postLikeUser);
                        }
                    }
                }

            }
            catch (System.IO.IOException IOEx)
            {
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    using (fs = new FileStream("Likes.csv", FileMode.Append))
                    {
                        using (var writer = new StreamWriter(fs))
                        {
                            using (CsvWriter csvWriter = new CsvWriter(writer))
                            {
                                csvWriter.Configuration.QuoteAllFields = true;
                                csvWriter.WriteRecords(postLikeUser);
                            }
                        }
                    }
                }
            }

        }

        public static void getAllFriends(long mUser, string[] mUserFriends, string access_token)
        {
            var jss = new JavaScriptSerializer();
            string smUserFriendsInline = string.Join(",", mUserFriends);
            string parameters = "user_ids=" + smUserFriendsInline + "&fields=sex,bdate,city,country,home_town,status,followers_count,common_count,music, movies, tv, books, games";
            string friendsRespond = getMethodData("users.get", parameters, access_token, "5.69", "yes");
            var vkFriendsOfmUser = new RootFriendsObject();

            try
            {
                vkFriendsOfmUser = jss.Deserialize<RootFriendsObject>(friendsRespond);
            }
            catch
            {
                Log("Friends request error happened", "log.txt");
            }

            List<FlatFriends> flatFriends = new List<FlatFriends>();
            for (int i = 0; i < vkFriendsOfmUser.response.Count(); i++)
            {
                flatFriends.Add( new FlatFriends(
                                mUser
                                , vkFriendsOfmUser.response.ElementAt(i).id
                                , vkFriendsOfmUser.response.ElementAt(i).first_name
                                , vkFriendsOfmUser.response.ElementAt(i).last_name
                                , vkFriendsOfmUser.response.ElementAt(i).sex
                                , vkFriendsOfmUser.response.ElementAt(i).bdate
                                , vkFriendsOfmUser.response.ElementAt(i).city
                                , vkFriendsOfmUser.response.ElementAt(i).country
                                , vkFriendsOfmUser.response.ElementAt(i).status
                                , vkFriendsOfmUser.response.ElementAt(i).followers_count
                                , vkFriendsOfmUser.response.ElementAt(i).home_town
                                , vkFriendsOfmUser.response.ElementAt(i).music
                                , vkFriendsOfmUser.response.ElementAt(i).movies
                                , vkFriendsOfmUser.response.ElementAt(i).tv
                                , vkFriendsOfmUser.response.ElementAt(i).books
                                , vkFriendsOfmUser.response.ElementAt(i).games
                                ));
            }

            


            FileStream fs = null;
            try
            {
                using (fs = new FileStream("Friends.csv", FileMode.Append))
                {
                    using (var writer = new StreamWriter(fs))
                    {
                        using (CsvWriter csvWriter2 = new CsvWriter(writer))
                        {
                            csvWriter2.Configuration.QuoteAllFields = true;

                            csvWriter2.WriteRecords(flatFriends);
                        }
                    }
                }

            }
            catch (System.IO.IOException IOEx)
            {
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    using (fs = new FileStream("Friends.csv", FileMode.Append))
                    {
                        using (var writer = new StreamWriter(fs))
                        {
                            using (CsvWriter csvWriter2 = new CsvWriter(writer))
                            {
                                csvWriter2.Configuration.QuoteAllFields = true;

                                csvWriter2.WriteRecords(flatFriends);
                            }
                        }
                    }
                }
            }


        }

        public static string getMethodData(string method, string parameters, string token, string apiVersion, string useToken)
        {
            Thread.Sleep(500);
            string URL = string.Format("https://api.vk.com/method/{0}", method);
            string formParams;
            if (useToken == "yes")
            {
                formParams = string.Format("?access_token={0}&v={1}", token, apiVersion);
            }
            else
            {

                formParams = string.Format("?v={0}", apiVersion);
            }
            if(parameters != null)
            {
                formParams = formParams + "&" + parameters;
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL + formParams);
            req.ContentType = "text/html";
            string pageSource;
            WebResponse getResponse = null;

            Boolean isOver = false;
            while (!isOver)
            {
                try
                {
                    getResponse = req.GetResponse();
                    isOver = true;
                }
                catch(System.Net.WebException e)
                {
                    isOver = false;

                }
            }

            using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            {
                pageSource = sr.ReadToEnd();
            }

            return pageSource;
        }

        public static void Log(string logMessage, string filePath)
        {
            try
            {
                using (StreamWriter logFileWrite = File.AppendText("log1.txt"))
                {
                    logFileWrite.Write("\r\nLog Entry : ");
                    logFileWrite.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                    logFileWrite.WriteLine("  :");
                    logFileWrite.WriteLine("  :{0}", logMessage);
                    logFileWrite.WriteLine("-------------------------------");

                    logFileWrite.Close();
                }
            }
            catch (System.IO.IOException IOEx)
            {
                using (StreamWriter logFileWrite = File.AppendText(String.Format("log{0}.txt", DateTime.Now.ToLongDateString())))
                {
                    logFileWrite.Write("\r\nLog Entry : ");
                    logFileWrite.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                    logFileWrite.WriteLine("  :");
                    logFileWrite.WriteLine("  :{0}", logMessage);
                    logFileWrite.WriteLine("-------------------------------");

                    logFileWrite.Close();
                }
            }

        }

    }



}

