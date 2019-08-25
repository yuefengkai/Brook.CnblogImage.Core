using System;
using MetaWeblogClient;

namespace Brook.CnblogImage.Core
{
    public static class BlogInfoHelper
    {
        public static BlogConnectionInfo GetBlogInfo() {

            var blogid = Environment.GetEnvironmentVariable("ASPNETCORE_CNBLOG_BLOGID");//"xxxxxx";
            var blogurl = $"https://www.cnblogs.com/{blogid}";
            var metaweblogurl = $"https://rpc.cnblogs.com/metaweblog/{blogid}";
            var username = Environment.GetEnvironmentVariable("ASPNETCORE_CNBLOG_USERNAME");//"xxxxxx";
            var password = Environment.GetEnvironmentVariable("ASPNETCORE_CNBLOG_PASSWORD");//"xxxxxx";
            var token = Environment.GetEnvironmentVariable("ASPNETCORE_TOKEN");//"xxxxxx";

            if (string.IsNullOrEmpty(blogid) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password)||
                string.IsNullOrEmpty(token))
            {
                throw new Exception($"Environment->ASPNETCORE_CNBLOG_BLOGID,ASPNETCORE_CNBLOG_USERNAME,ASPNETCORE_CNBLOG_PASSWORD,ASPNETCORE_TOKEN Is Not Be Null");
            }

            var info = new BlogConnectionInfo(blogurl, metaweblogurl, blogid, username, password);

            return info;
        }
    }
}
