using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkAnalysis
{

    public class VkGeneralRespond
    {
        public int count { get; set; }
        public string[] items { get; set; }
    }

    public class VkPostsRespond
    {
        public int count { get; set; }
        public Items[] items { get; set; }
    }

    public class VkFriendsRespond
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int sex { get; set; }
        public string bdate { get; set; }
        public City city { get; set; }
        public Country country { get; set; }
        public string status { get; set; }
        public int followers_count { get; set; }
        public string home_town { get; set; }
        public string music { get; set; }
        public string movies { get; set; }
        public string tv { get; set; }
        public string books { get; set; }
        public string games { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string title { get; set; }
    }
    public class Country
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class RootPostsObject
    {

        public VkPostsRespond response { get; set; }

    }
    public class RootFriendsObject
    {

        public VkFriendsRespond[] response { get; set; }

    }

    public class RootObject
    {

        public VkGeneralRespond response { get; set; }
    }

    public class Photo
    {
        public int id { get; set; }
        public int album_id { get; set; }
        public int owner_id { get; set; }
        public string photo_75 { get; set; }
        public string photo_130 { get; set; }
        public string photo_604 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string text { get; set; }
        public long date { get; set; }
    }

    public class Comments
    {
        public int count { get; set; }
        public Boolean groups_can_post { get; set; }
        public int can_post { get; set; }
    }


    public class Link
    {
        public string url { get; set; }
        public string title { get; set; }
        public string caption { get; set; }
        public string description { get; set; }
        public Photo photo { get; set; }
    }


    public class Video
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public string description { get; set; }
        public long date { get; set; }
        public int comments { get; set; }
        public int views { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string photo_130 { get; set; }
        public string photo_320 { get; set; }
        public string photo_800 { get; set; }
        public string access_key { get; set; }
        public int can_add { get; set; }
    }

    public class Attachments
    {
        public string type { get; set; }
        public Link link { get; set; }
        public Photo photo { get; set; }
    }

    public class PostSource
    {
        public string type { get; set; }
        public string platform { get; set; }
    }

    public class Likes
    {
        public int count { get; set; }
        public int user_likes { get; set; }
        public int can_like { get; set; }
        public int can_publish { get; set; }
    }

    public class Reposts
    {
        public int count { get; set; }
        public int user_reposted { get; set; }
    }

    public class Views
    {
        public int count { get; set; }
    }
    public class Items
    {

        public int id { get; set; }
        public int from_id { get; set; }
        public int owner_id { get; set; }
        public long date { get; set; }
        public string text { get; set; }
        public string title { get; set; }

        public string post_type { get; set; }
        public int can_delete { get; set; }
        public int can_pin { get; set; }
        public Attachments[] attachments { get; set; }
        public PostSource post_source { get; set; }
        public Comments comments { get; set; }

        public Likes likes { get; set; }
        public Reposts reposts { get; set; }
        public Views views { get; set; }
    }


}
