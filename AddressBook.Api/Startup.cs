using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Api.Filters;
using AddressBook.Api.MessageHub;
using AddressBook.Api.Middleware;
using AddressBook.Api.Validations;
using AddressBook.DataAccess.EFShared;
using AddressBook.DataAccess.Persistence;
using AddressBook.Domain.Persistence;
using AddressBook.Domain.Service;
using AddressBook.Domain.Service.Implementation;
using AddressBook.Model.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AddressBook.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => {
                                options.EnableEndpointRouting = false;
                                options.Filters.Add(typeof(ValidateModelStateFilter));
                 })
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation();

            services.AddEntityFrameworkNpgsql().AddDbContext<AddressBookDbContext>(
                opt => opt.UseNpgsql(Configuration.GetConnectionString("AddressBookDbConnection"), 
                b => b.MigrationsAssembly("AddressBook.DataAccess")));

            services.AddTransient<IValidator<Contact>, ContactValidator>();
            services.AddTransient<IValidator<Address>, AddressValidator>();
            services.AddTransient<IValidator<PhoneNumber>, PhoneNumberValidator>();

            services.AddScoped<IContactsDataAccess, ContactsDataAccess>();
            services.AddScoped<IPhoneNumbersDataAccess, PhoneNumbersDataAccess>();
            services.AddScoped<IAddressBookService, AddressBookService>();

            services.AddScoped<IAddressBookBroadcaster, AddressBookBroadcaster>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ContactsHub>("/hubs/contacts");
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc();
        }
    }
}
