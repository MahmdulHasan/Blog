using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyFirstWebBlog.Startup))]
namespace MyFirstWebBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
