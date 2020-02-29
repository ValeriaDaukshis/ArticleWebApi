using ArticleProject.DataAccess;
using ArticleProject.DataAccess.ArticlesData;
using ArticleProject.DataAccess.UsersData;
using ArticleProject.Models;
using ArticleProject.Services;
using ArticleProject.Services.ArticleService;
using ArticleProject.Services.UserServices;
using ArticlesProject.DataAccess;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ArticlesProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddMvc().AddFluentValidation(fv => {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
            });
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMapping());
                mc.AddProfile(new CategoryMapping());
                mc.AddProfile(new ArticleMapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoriesContext, CategoriesContext>();
            services.AddTransient<IUsersContext, UsersContext>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IArticleContext, ArticleContext>();
            services.AddTransient<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.Configure<Settings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
