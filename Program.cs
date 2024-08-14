using System;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Paso 1: Obtener la dirección IP de la máquina
            string ipAddress = GetLocalIPAddress();

            // Paso 2: Formatear los datos de la IP
            string messageBody = $"La dirección IP de la máquina es: {ipAddress}";
            Console.WriteLine(messageBody);

            // Paso 3: Configurar el cliente SMTP
            SmtpClient client = new SmtpClient("smtp.tuservidor.com")
            {
                Port = 587, // Cambia el puerto si es necesario
                Credentials = new NetworkCredential("tuusuario@dominio.com", "tucontraseña"),
                EnableSsl = true,
            };

            // Paso 4: Configurar el mensaje de correo electrónico
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("tuusuario@dominio.com"),
                Subject = "Información de IP",
                Body = messageBody,
                IsBodyHtml = false,
            };
            mailMessage.To.Add("destinatario@dominio.com");

            // Paso 5: Enviar el correo
            client.Send(mailMessage);

            Console.WriteLine("Correo enviado exitosamente.");            
        }
        catch (Exception ex)
        {
            // Paso 6: Manejo de excepciones
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }
    }

    static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No se encontró ninguna dirección IPv4 en la máquina.");
    }
}
