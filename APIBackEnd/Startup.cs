using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using APIBackEnd.Models;
using System.Text.Json;
using Microsoft.OpenApi.Models;

namespace APIBackEnd
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
            //Adicionando o Contexto da API junto a um banco de dados em memória
            services.AddDbContext<APIContext>(opt => opt.UseInMemoryDatabase("APIDatabase"));
            services.AddControllers();

            //Adicionando o swagger para melhor testar a API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "APIBackEnd", 
                    Version = "v1",
                    Description = "API do Desafio Ativa Investimentos",
                    Contact = new OpenApiContact
                    {
                        Name = "Lucas Clemente",
                        Email = "lucas.cmb03@gmail.com",
                        Url = new Uri("https://github.com/lucascmb")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Seedando dados iniciais no BD em memória
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase(databaseName: "APIDatabase").Options;

            using (var context = new APIContext(options))
            {
                context.Database.EnsureCreated();

                var jsonString = File.ReadAllText("SeedFiles/DataFundos.json");
                var fund = JsonSerializer.Deserialize<List<Fundo>>(jsonString);

                context.Fundos.AddRange(fund);

                context.SaveChanges();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            //Configurando o swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIBackEnd V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
