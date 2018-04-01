using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkAnalysis
{
    public class PostPerUser
    {
        public long userId { get; set; }
        public long postId { get; set; }
        public int likes { get; set; }
        public PostPerUser(long pId, long uId, int vLikes)
        {
            userId = uId;
            postId = pId;
            likes = vLikes;
        }
    }

    public class PostLikeUser
    {
        public long userId { get; set; }
        public long postId { get; set; }
        public long userLikedId { get; set; }
        public PostLikeUser(long uId, long pId, long ulId)
        {
            userId = uId;
            postId = pId;
            userLikedId = ulId;
        }
    }

    public class FlatPost
    {
        public int id { get; set; }
        public int from_id { get; set; }
        public int owner_id { get; set; }
        public long date { get; set; }
        public string text { get; set; }

        public string post_type { get; set; }
        public int can_delete { get; set; }
        public int can_pin { get; set; }
        public int likes { get; set; }

        public FlatPost(int vId, int vFrom_id, int vOwner_id, long vDate, string vText, int vLikes)
        {
            id = vId;
            from_id = vFrom_id;
            owner_id = vOwner_id;
            date = vDate;
            text = vText;
            likes = vLikes;
        }

    }

    public class FlatFriends
    {
        public long mUserId { get; set; }
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int sex { get; set; }
        public string bdate { get; set; }
        public long cityId { get; set; }
        public string cityTitle { get; set; }
        public long countryId { get; set; }
        public string countryTitle { get; set; }
        public string status { get; set; }
        public int followers_count { get; set; }
        public string home_town { get; set; }
        public string music { get; set; }
        public string movies { get; set; }
        public string tv { get; set; }
        public string books { get; set; }
        public string games { get; set; }
        public FlatFriends(long mUserId1, int id1, string first_name1, string last_name1, int sex1, string bdate1, City city1, Country country1, string status1
                            , int followers_count1, string home_town1, string music1, string movies1, string tv1, string books1, string games1)
        {
            mUserId = mUserId1;
            id = id1;
            first_name = first_name1;
            last_name = last_name1;
            sex = sex1;
            bdate = bdate1;
            if (city1 != null)
            {
                cityId = city1.id;
                cityTitle = city1.title;
            }
            if (country1 != null)
            {
                countryId = country1.id;
                countryTitle = country1.title;
            }
            status = status1;
            followers_count = followers_count1;
            home_town = home_town1;
            music = music1;
            movies = movies1;
            tv = tv1;
            books = books1;
            games = games1;
    }


    }

}
