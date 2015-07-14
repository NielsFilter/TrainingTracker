using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrainingTracker.Startup))]
namespace TrainingTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
