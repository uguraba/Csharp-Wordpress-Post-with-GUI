using System;
using CookComputing.XmlRpc;

/*
    DateTime: 27.02.2016 22:45 GMT+2
    Github: https://github.com/uguraba
    Twitter: https://twitter.com/uguraba
*/

namespace Wordpress_Post
{
    class Wordpress
    {
        //public XmlRpcClientProtocol clientProtocol;
        //public IcreatePost _post;
        public postInfo _blogPost;

        public struct customField
        {
            public string key;
            public string value;
        }

        public struct postInfo
        {
            public string[] categories;
            public string title;
            public string description;
            public string mt_allow_comments;
            public string mt_keywords;
            public DateTime dateCreated;
            public string mt_allow_pings;
        }

        public interface IcreatePost
        {
            [XmlRpcMethod("metaWeblog.newPost")]
            string NewPost(int blogId, string strUserName, string strPassword, postInfo content, int publish);
        }

        public void setPost(string _title, string _content, string _comment, string _category, string  _tag, DateTime _publishDate, string _ping)
        {
            _blogPost = default(postInfo);
            _blogPost.title = _title;
            _blogPost.description = _content;
            _blogPost.mt_allow_comments = _comment; //According to received string, comments are allowed or not.
            _blogPost.categories = _category.Split(','); //Categories must be split with "," also if category does not exist, it will not create.
            _blogPost.mt_keywords = _tag; //Tags are split with "," but it xml-rpc do this for us.
            _blogPost.dateCreated = _publishDate;
            _blogPost.mt_allow_pings = _ping; //According to received string, ping is allowed or not.
        }

        public string sendPost(string _url, string _username, string _password)
        {
            XmlRpcClientProtocol clientProtocol;
            IcreatePost _post = (IcreatePost)XmlRpcProxyGen.Create(typeof(IcreatePost));
            clientProtocol = (XmlRpcClientProtocol)_post;
            clientProtocol.Url = "http://" + _url + "/xmlrpc.php";
            string _postID = "";
            try
            {
                _postID = _post.NewPost(1, _username, _password, _blogPost, 1);               
            }
            catch (Exception ex)
            {
                _postID = "Error";
            }
            return _postID;
        }
    }
}