using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using apsys.identityserver.owin;
using IdentityServer3.AccessTokenValidation;
using log4net;
using log4net.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(apsys.project.web.Startup))]

namespace apsys.project.web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888
            this.ConfigureIdentityServerClient(app);

            var log4NetConfigurationFilePath = HostingEnvironment.MapPath("~/log4Net.config");
            XmlConfigurator.Configure(new System.IO.FileInfo(log4NetConfigurationFilePath));
            ILog filelogger = LogManager.GetLogger("FileLogger");

            app.UseCors(CorsOptions.AllowAll);
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            this.ConfigureIdentityServerClient(app);

            IKernel kernel = new StandardKernel();
            app.UseNinjectMiddleware(() => kernel);
            app.UseNinjectWebApi(config);
        }

        private void  ConfigureIdentityServerClient(IAppBuilder app)
        {
            app.ConfigureIdentityServerClients(
                cfg =>
                {


                }
            );

            var identityServerUrl = "http://localhost:52330/identity";
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = identityServerUrl,
            });
        }
    }
}
