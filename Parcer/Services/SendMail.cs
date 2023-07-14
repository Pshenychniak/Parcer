using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Parcer.Services
{
    static public  class SendMail
    {

        static public void Send(string recipient,string path)
        {
            try
            {
                // Налаштування SMTP-сервера Google та облікових даних
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "vovan2828@gmail.com";
                string smtpPassword = "mwqvyjfpvttmzuky";

                // Створення об'єкта MailMessage
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("vovan2828@gmail.com");
                mail.To.Add(recipient);
                mail.Subject = "Список товарів";
                mail.Body = "Ваш список товарів";

                // Додавання вкладення
                Attachment attachment = new Attachment(path, MediaTypeNames.Application.Pdf);
                mail.Attachments.Add(attachment);

                // Створення об'єкта SmtpClient та налаштування параметрів
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.EnableSsl = true; // Використовувати SSL
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                // Відправка електронної пошти
                smtpClient.Send(mail);

                // Звільнення ресурсів вкладення
                attachment.Dispose();

                Console.WriteLine("Email відправлено успішно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Виникла помилка при відправці електронної пошти: " + ex.Message);
            }
        }
        static public bool IsValidEmail(string email)
        {
            // Регулярний вираз для перевірки електронної адреси
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            // Перевірка, чи відповідає рядок валідному формату email
            Match match = Regex.Match(email, pattern);

            return match.Success;
        }
    }
}
