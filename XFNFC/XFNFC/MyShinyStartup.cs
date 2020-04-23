using Microsoft.Extensions.DependencyInjection;
using Shiny;
using System;
using System.Collections.Generic;
using System.Text;

namespace XFNFC
{
    public class MyShinyStartup:ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.UseNfc();
        }
    }
}
