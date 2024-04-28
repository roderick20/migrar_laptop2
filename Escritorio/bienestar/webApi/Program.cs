using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

[CompilerGenerated]
internal class Program
{
	private static void _003CMain_003E_0024(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddRazorPages();
		SwaggerGenServiceCollectionExtensions.AddSwaggerGen(builder.Services, (Action<SwaggerGenOptions>)delegate(SwaggerGenOptions setup)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected O, but got Unknown
			//IL_006c: Expected O, but got Unknown
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Expected O, but got Unknown
			//IL_00f9: Expected O, but got Unknown
			OpenApiSecurityScheme val = new OpenApiSecurityScheme
			{
				BearerFormat = "JWT",
				Name = "JWT Authentication",
				In = (ParameterLocation)1,
				Type = (SecuritySchemeType)1,
				Scheme = "Bearer",
				Description = "Token generado en el path /Login (POST)",
				Reference = new OpenApiReference
				{
					Id = "Bearer",
					Type = (ReferenceType)6
				}
			};
			OpenApiLicense license = new OpenApiLicense
			{
				Name = "Microdata",
				Url = new Uri("https://www.microdata.com.pe/")
			};
			OpenApiInfo val2 = new OpenApiInfo
			{
				Version = "v1",
				Title = "CMAC Bienestar 2023",
				Description = "Sistema Bienestar - Módulo Uniformes",
				License = license
			};
			SwaggerGenOptionsExtensions.SwaggerDoc(setup, "v1", val2);
			SwaggerGenOptionsExtensions.AddSecurityDefinition(setup, val.Reference.Id, val);
			OpenApiSecurityRequirement val3 = new OpenApiSecurityRequirement();
			((Dictionary<OpenApiSecurityScheme, IList<string>>)val3).Add(val, (IList<string>)Array.Empty<string>());
			SwaggerGenOptionsExtensions.AddSecurityRequirement(setup, val3);
		});
		JwtBearerExtensions.AddJwtBearer(builder.Services.AddAuthentication("Bearer"), (Action<JwtBearerOptions>)delegate(JwtBearerOptions options)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Expected O, but got Unknown
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = (SecurityKey)new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:BienestarKey"] ?? "")),
				ValidateIssuer = false,
				ValidateAudience = false
			};
		});
		builder.Services.AddCors(delegate(CorsOptions options)
		{
			options.AddPolicy("CMACBienestarPolicy", delegate(CorsPolicyBuilder app)
			{
				app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
			});
		});
		NewtonsoftJsonMvcBuilderExtensions.AddNewtonsoftJson(builder.Services.AddControllers());
		ServiceCollectionExtensions.AddAutoMapper(builder.Services, new Assembly[1] { typeof(Program).Assembly });
		builder.Services.AddScoped<IUniformeRepository, UniformeRepository>();
		builder.Services.AddScoped<ITallaRepository, TallaRepository>();
		builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
		builder.Services.AddScoped<IExtrasRepository, ExtrasRepository>();
		builder.Services.AddScoped<IRegionRepository, RegionRepository>();
		builder.Services.AddScoped<IBannerRepository, BannerRepository>();
		builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
		builder.Services.AddScoped<IReporteRepository, ReporteRepository>();
		builder.Services.AddScoped<IColaboradorRHRepository, ColaboradorRHRepository>();
		builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
		builder.Services.AddScoped<ITrabajadorRepository, TrabajadorRepository>();
		builder.Services.AddScoped<IRolRepository, RolRepository>();
		builder.Services.AddScoped<IPeriodoRepository, PeriodoRepository>();
		builder.Services.AddScoped<IKardexRepository, KardexRepository>();
		builder.Services.AddScoped<ILoggerRepository, LoggerRepository>();
		WebApplication webApplication = builder.Build();
		if (webApplication.Environment.IsDevelopment())
		{
			SwaggerBuilderExtensions.UseSwagger((IApplicationBuilder)webApplication, (Action<SwaggerOptions>)null);
			SwaggerUIBuilderExtensions.UseSwaggerUI((IApplicationBuilder)webApplication, (Action<SwaggerUIOptions>)null);
			webApplication.UseDeveloperExceptionPage();
		}
		webApplication.UseCors("CMACBienestarPolicy");
		webApplication.UseAuthentication();
		webApplication.UseAuthorization();
		webApplication.MapControllers();
		webApplication.MapRazorPages();
		webApplication.Run();
	}
}
