using Ans.Net10.Common.Services;
using Ans.Net10.Web.Middlewares;
using Ans.Net10.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Ans.Net10.Web
{

	public static class LibWebStartup
	{

		/* functions */


		public static IMvcBuilder Add_AnsNet10Web(
			this WebApplicationBuilder builder,
			IConfiguration configuration)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			var options1 = configuration.GetLibWebOptions();


			var culture1 = new CultureInfo(options1.Culture);
			CultureInfo.DefaultThreadCurrentCulture = culture1;
			CultureInfo.DefaultThreadCurrentUICulture = culture1;


			/*
			 * Swagger
			 * (если при запросе SwaggerUI отображается ошибка, необходимо обновить кеш браузера)
			 */

			if (options1.Swagger?.Length > 0)
			{
				builder.Services.AddEndpointsApiExplorer();
				builder.Services.AddSwaggerGen(o =>
				{
					foreach (var item1 in options1.Swagger)
					{
						var a1 = item1.Split('|');
						o.SwaggerDoc(a1[0], new OpenApiInfo { Title = a1[1] });
					}
					o.EnableAnnotations();
					o.IncludeXmlComments(Path.Combine(
						AppContext.BaseDirectory,
						$"{Assembly.GetEntryAssembly().GetName().Name}.xml"));
				});
			}


			/*
			 * singleton
			 */

			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			if (options1.MailService == null)
				builder.Services.AddSingleton<IMailerService, FakeMailerService>();
			else
				builder.Services.AddSingleton<IMailerService, AnsMailerService>(
					x => new(options1.MailService));

			builder.Services.AddSingleton<SitemapProvider>();


			/*
			 * scoped
			 */

			builder.Services.AddScoped<IViewRenderService, AnsViewRenderService>();
			builder.Services.AddScoped<CurrentContext>();


			/*
			 * Commons
			 */

			builder.Services.AddResponseCaching();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddHttpClient();

			if (options1.UseSessions)
			{
				builder.Services.AddDistributedMemoryCache();
				builder.Services.AddSession();
			}

			builder.Services.AddCors(o =>
			{
				o.AddPolicy(_Consts.CORS_ALLOW_ALL, p =>
				{
					p.AllowAnyOrigin();
					p.AllowAnyHeader();
					p.AllowAnyMethod();
				});
			});


			/*
			 * IMvcBuilder
			 */

			var mvc1 = builder.Services.AddMvc(o =>
			{
				o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
					_ => Common.Resources.Form.Text_ValueIsRequired);
				foreach (var item1 in _Consts.CACHE_PROFILES)
					o.CacheProfiles.Add(item1.Key, item1.Value);
			});

			mvc1.AddJsonOptions(o =>
			{
				//o.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
				//o.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
				o.JsonSerializerOptions.DefaultIgnoreCondition
					= JsonIgnoreCondition.WhenWritingDefault; // .WhenWritingNull;
			});

			if (options1.UseRuntimeCompilation)
				mvc1.AddRazorRuntimeCompilation();

			return mvc1;
		}


		/* methods */


		public static void Use_AnsNet10Web(
			this WebApplication app,
			IConfiguration configuration)
		{
			var options1 = configuration.GetLibWebOptions();


			/*
			 * Swagger
			 * (если при запросе SwaggerUI отображается ошибка, необходимо обновить кеш браузера)
			 */

			if (options1.Swagger?.Length > 0)
			{
				app.UseSwagger();
				app.UseSwaggerUI(o =>
				{
					var s1 = app.Environment.IsDevelopment()
						? "/swagger/" : null;
					foreach (var item1 in options1.Swagger)
					{
						var a1 = item1.Split('|');
						o.SwaggerEndpoint($"{s1}{a1[0]}/swagger.json", a1[1]);
					}
					o.DefaultModelRendering(
						Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
					o.DefaultModelsExpandDepth(5);
					o.DefaultModelExpandDepth(5);
				});
			}


			/*
			 * Sitemap
			 */

			var sitemap1 = app.Services.GetService<SitemapProvider>();
			var node1 = new MapNodesItem("ans", "Ans", null);
			node1.TreePages.AppendChilds(
				new MapPagesItem("errors", "Errors", null,
					new MapPagesItem("httperrors", "Http Errors", null)));
			sitemap1.TreeNodes.AppendChild(node1);


			/*
			 * Commons
			 */

			app.UseHttpsRedirection();
			app.UseResponseCaching();
			app.UseRouting();
			app.MapRazorPages();
			app.MapControllers();

			if (!app.Environment.IsDevelopment())
				app.UseHsts();

			if (options1.UseDeveloperMode)
			{
				app.UseDeveloperExceptionPage();
				app.UseStatusCodePages();
			}
			else
			{
				var path1 = options1.Errors?.ServerErrorPath ?? "/Ans/Errors/ServerError";
				var path2 = options1.Errors?.HttpErrorPath ?? "/Ans/Errors/HttpErrors";
				app.UseExceptionHandler(path1);
				app.UseStatusCodePagesWithReExecute(path2, "?code={0}");
				app.UseMiddleware<AnsHttpExceptionHandler>();
			}

			if (options1.Mimetypes?.Length > 0)
			{
				var provider1 = new FileExtensionContentTypeProvider();
				foreach (var item1 in options1.Mimetypes)
				{
					var a1 = item1.Split('|');
					provider1.Mappings[a1[0]] = a1[1];
				}
				app.UseStaticFiles(new StaticFileOptions
				{
					ContentTypeProvider = provider1,
				});
			}
			else
			{
				app.UseStaticFiles();
			}

			if (options1.UseSessions)
				app.UseSession();

			app.UseCors(options1.CorsProfile ?? _Consts.CORS_ALLOW_ALL);

			if (options1.Routes?.Length > 0)
				app.AddRoutes(options1.Routes);
		}

	}

}
