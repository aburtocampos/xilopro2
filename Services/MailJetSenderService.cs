using Mailjet.Client;
using Newtonsoft.Json.Linq;
using xilopro2.Config;
using Microsoft.AspNetCore.Identity.UI.Services;
using Mailjet.Client.Resources;

namespace xilopro2.Services
{
    public class MailJetSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;
     //   public OpcionesMailJet _opcionesMailJet;




        public MailJetSenderService(IConfiguration configuration/*, OpcionesMailJet opcionesMailJet*/)
        {
            _configuration = configuration;
           // _opcionesMailJet = opcionesMailJet;
        }


        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
           // _opcionesMailJet = _configuration.GetSection("MailJet").Get<OpcionesMailJet>();

            /*MailjetClient client = new MailjetClient(_opcionesMailJet.ApiKey, _opcionesMailJet.SecretKey)
            {
                Version = ApiVersion.V3_1,
            };*/

            MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("5763d155f397c9f1bab2fe19e689e362"), Environment.GetEnvironmentVariable("8c0f95ec5381c8a68233946ec3749b06"))
            {
                Version = ApiVersion.V3_1,
            };

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.Messages, new JArray {
                new JObject {
                    { "From", new JObject {
                            { "Email", "eabucam@gmail.com" /*_opcionesMailJet.emailSender*/ },
                            { "Name", "Sistema Xilotepelt" }
                        }
                    },
                    { "To",
                        new JArray {
                            new JObject {
                                { "Email", email },
                                { "Name", "Management" }
                            } }
                    },
                    { "Subject", subject },
                    { "HTMLPart", htmlMessage }
                }
            });

            MailjetResponse response = await client.PostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }




    }
}
