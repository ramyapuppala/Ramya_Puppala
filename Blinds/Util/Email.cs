using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Blinds
{
    [SetUpFixture]
    public class Email
    {
        [OneTimeTearDown]
        public void sendEmail()
        {
            string reportPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            reportPath = Directory.GetParent(Directory.GetParent(reportPath).FullName).FullName;
            string folderPath = reportPath + "\\Reports\\";
            DirectoryInfo DirInfo = new DirectoryInfo(folderPath);

            FileInfo[] files = DirInfo.GetFiles();
            DateTime lastWrite = DateTime.MinValue;
            FileInfo lastWrittenFile = null;

            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime > lastWrite)
                {
                    lastWrite = file.LastWriteTime;
                    lastWrittenFile = file;
                }
            }

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;

            

            var fromAddress = new MailAddress("sender@gmail.com", "Sruthi");
            var toAddress = new MailAddress("sruti.receiver@gmail.com", "Sruthi");
            string fromPassword = "Yourpassword";


            MailMessage mail = new MailMessage();
            Attachment data = new Attachment(folderPath + lastWrittenFile, MediaTypeNames.Application.Octet);
            mail.Attachments.Add(data);
            mail.Subject = "Automation Test Report - Do not reply this automatic Email";
            mail.Body = "Hello,\n\nPlease open the attached Automation Report. screen shot are in location .Kind Regards\nSruthi";
            try
            {
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.UseDefaultCredentials = false;

                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("sruti.puppala@gmail.com", fromPassword);

                client.EnableSsl = true;

                client.Credentials = credentials;
                
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Automation Test Report - Do not reply this automatic Email",
                    Body = "Hello,\n\nPlease open the attached Automation Report. screen shot are in location .Kind Regards\nSruthi"
                })

                  

               
                {
                    client.Send(message);
                }




            }





            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
