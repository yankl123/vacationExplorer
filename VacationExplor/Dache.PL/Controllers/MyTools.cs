using Dache.DP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.Extensions.Configuration;

namespace Dache.PL.Controllers
{
    public class MyTools
    {
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        public static string el(string p, int n)
        {
            return new string('_', (60 - p.Trim().Length - n));
        }
        public static string EditLetter(Supplier supplier)
        {
            string contents = File.ReadAllText(@"Lpdfs/tuta.html");
            contents = contents.Replace("DATE", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
            contents = contents.Replace("NAME", supplier.Name);
            contents = contents.Replace("ADRESSE", supplier.Addrese);
            contents = contents.Replace("PHONE", supplier.Phone);
            contents = contents.Replace("EMAIL", supplier.Email);
            contents = contents.Replace("UN", supplier.UserName);
            contents = contents.Replace("PWD", new String('*', supplier.Password.Length));
            return contents;
        }

        public static void SendEmail(string to , string sub ,string body)
        {

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = config["Smtp:Username"];
            oMail.To = to;
            oMail.Subject = sub;
            oMail.HtmlBody = body;

            SmtpServer oServer = new SmtpServer("smtp.gmail.com");

            oServer.User = config["Smtp:Username"];
            oServer.Password = config["Smtp:Password"];

            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            SmtpClient oSmtp = new SmtpClient();
            oSmtp.Timeout = 300000;
            oSmtp.SendMail(oServer, oMail);

        }
    }   

}
