using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;

namespace SOAPHolaMundo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Añadir servicios de Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.MapType<System.Xml.Linq.XElement>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                {
                    Type = "string",
                    Format = "xml"
                });
            });

            var app = builder.Build();

            // Habilitar archivos estáticos desde la carpeta wwwroot
            app.UseStaticFiles();

            // Habilitar Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            // Ruta para acceder al archivo index.html
            app.MapGet("/", async context =>
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html");
                if (File.Exists(filePath))
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.SendFileAsync(filePath);
                }
                else
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync("File not found");
                }
            });

            // Endpoint para recibir mensajes SOAP
            app.MapPost("/HelloWorld", async (HttpContext context, ILogger<Program> logger) =>
            {
                var requestBody = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();

                // Procesar el mensaje SOAP (Asegurándonos que es un SOAP)
                if (requestBody.Contains("<soapenv:Envelope"))
                {
                    // Simulamos la respuesta del servicio SOAP
                    string name = ExtractNameFromSoapRequest(requestBody);

                    string soapResponse = CreateSoapResponse(name);

                    // Escribir la respuesta SOAP
                    context.Response.ContentType = "text/xml";
                    await context.Response.WriteAsync(soapResponse);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Bad Request. Invalid SOAP format.");
                }
            });

            // Ejecutar la aplicación
            app.Run();
        }

        // Método para extraer el nombre del mensaje SOAP
        private static string ExtractNameFromSoapRequest(string soapRequest)
        {
            var xml = XElement.Parse(soapRequest);
            var name = xml.Descendants("{http://schemas.xmlsoap.org/soap/envelope/}Body")
                .Descendants("SayHello")
                .FirstOrDefault()
                ?.Value;
            return name ?? "Unknown";
        }

        // Método para generar la respuesta SOAP
        private static string CreateSoapResponse(string name)
        {
            return $@"
                <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:web='http://localhost/'>
                    <soapenv:Header/>
                    <soapenv:Body>
                        <web:SayHelloResponse>
                            <web:Message>Hello, {name}</web:Message>
                        </web:SayHelloResponse>
                    </soapenv:Body>
                </soapenv:Envelope>";
        }
    }
}