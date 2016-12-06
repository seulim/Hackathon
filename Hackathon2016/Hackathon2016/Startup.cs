using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hackathon2016.Startup))]
namespace Hackathon2016
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
