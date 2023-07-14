// See https://aka.ms/new-console-template for more information
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

Console.WriteLine("Hello, World!");


try
{
    // Налаштування SMTP-сервера Google та облікових даних
    string smtpServer = "smtp.gmail.com";
    int smtpPort = 587;
    string smtpUsername = "vovan2828@gmail.com";
    string smtpPassword = "qpxbrowhtqlyltmr";

    // Створення об'єкта MailMessage
    MailMessage mail = new MailMessage();
    mail.From = new MailAddress("vovan2828@gmail.com");
    mail.To.Add("vovazod@gmail.com");
    mail.Subject = "TEST2";
    mail.Body = "TEST2";

    // Додавання вкладення
    Attachment attachment = new Attachment("D:\\ITStep\\Мережеве\\Parcer\\Parcer\\bin\\Debug\\net6.0\\test1.xlsx", MediaTypeNames.Application.Pdf);
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
