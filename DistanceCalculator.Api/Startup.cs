using DistanceCalculator.Business;
using DistanceCalculator.Business.DistanceCalculator;
using DistanceCalculator.Business.Features.Airport.Queries;
using DistanceCalculator.Business.Infrastructure;
using DistanceCalculator.Business.Integrations.AirportService;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DistanceCalculator.Api
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
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "DistanceCalculator.Api", Version = "v1" });
			});
			
			services.AddMediatR(typeof(GetDistance));

			services.AddTransient<IAirportClientFactory, AirportClientFactory>();
			services.AddTransient<IAirportService, AirportService>();
			services.AddHttpClient(AirportClientSettings.ClientName);
			services.Configure<AirportClientSettings>(Configuration.GetSection(AirportClientSettings.ClientName));

			services.AddTransient<IDistanceCalculator, Business.DistanceCalculator.DistanceCalculator>();

			services.AddFluentValidation();
			services.AddAutoMapper(typeof(BusinessLayer));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DistanceCalculator.Api v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}