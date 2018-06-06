// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PortoGo.WebApi.DependencyResolution
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataHandler;
    using Microsoft.Owin.Security.DataHandler.Encoder;
    using Microsoft.Owin.Security.DataHandler.Serializer;
    using Microsoft.Owin.Security.DataProtection;
    using PortoGO.DB;
    using PortoGO.DB.Domain;
    using StructureMap;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using StructureMap.Web;
    using System.Data.Entity;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Http.Controllers;

    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
               scan =>
               {
                   scan.TheCallingAssembly();
                   scan.AssembliesFromApplicationBaseDirectory(assembly => !assembly.FullName.StartsWith("System.Web"));
                   scan.AddAllTypesOf<IHttpModule>();
                   scan.WithDefaultConventions();
               });

            For<IUserStore<User>>().Use<UserStore<User>>();
            For<IRoleStore<IdentityRole, string>>().Use<RoleStore<IdentityRole>>();
            For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);
            For<DbContext>().Use(() => new PortoGoContext());
            For<HttpRequestMessage>()
  .AlwaysUnique()
  .Use(ctx => ctx.GetInstance<HttpContext>().Items["MS_HttpRequestMessage"] as HttpRequestMessage);
           // For<IIdentity>().HttpContextScoped().Use(c => c.GetInstance<HttpRequestMessage>().GetRequestContext().Principal.Identity);

            //For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
            For<ISecureDataFormat<AuthenticationTicket>>().Use<SecureDataFormat<AuthenticationTicket>>();
            For<IDataSerializer<AuthenticationTicket>>().Use<TicketSerializer>();
            For<IDataProtector>().Use(() => new DpapiDataProtectionProvider().Create("ASP.NET Identity"));
            For<ITextEncoder>().Use<Base64UrlTextEncoder>();
        }

        #endregion
    }
}