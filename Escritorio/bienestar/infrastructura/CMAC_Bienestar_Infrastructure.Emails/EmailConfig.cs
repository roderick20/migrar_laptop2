using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_Core.ViewModels.Exceptions;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Emails;

public class EmailConfig
{
	private readonly string cuentaServidorCorreo;

	private readonly string passwordCorreo;

	private readonly string displayName;

	private readonly string rutaAdjuntos;

	private readonly SmtpClient smtpClient;

	public EmailConfig(IConfiguration configuration)
	{
		cuentaServidorCorreo = configuration.GetSection("EmailConf:cuentaCorreo").Value ?? throw new ValueNullException("cuentaServidorCorreo");
		passwordCorreo = configuration.GetSection("EmailConf:passwordCorreo").Value ?? throw new ValueNullException("passwordCorreo");
		displayName = configuration.GetSection("EmailConf:displayName").Value ?? throw new ValueNullException("passwordCorreo");
		rutaAdjuntos = configuration.GetSection("EmailConf:rutaAdjuntos").Value ?? throw new ValueNullException("passwordCorreo");
		smtpClient = new SmtpClient
		{
			Host = (configuration.GetSection("EmailConf:servidorCorreo").Value ?? throw new ValueNullException("servidorCorreo")),
			Port = int.Parse(configuration.GetSection("EmailConf:puertoServidorCorreo").Value ?? throw new ValueNullException("puertoServidorCorreo")),
			EnableSsl = bool.Parse(configuration.GetSection("EmailConf:ssl").Value ?? throw new ValueNullException("ssl")),
			DeliveryMethod = SmtpDeliveryMethod.Network,
			Credentials = new NetworkCredential(cuentaServidorCorreo, passwordCorreo)
		};
	}

	public bool EnviarCorreo(EmailVM email)
	{
		bool result = true;
		try
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			MailMessage mailMessage = new MailMessage();
			mailMessage.To.Add(new MailAddress(email.destinatarios));
			mailMessage.From = new MailAddress(cuentaServidorCorreo, displayName);
			mailMessage.Subject = email.asunto;
			mailMessage.Body = email.mensaje;
			mailMessage.IsBodyHtml = true;
			mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(WebUtility.HtmlDecode(email.mensaje), null, "text/html"));
			mailMessage.BodyEncoding = Encoding.UTF8;
			mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
			smtpClient.Send(mailMessage);
			smtpClient.SendCompleted += delegate(object send, AsyncCompletedEventArgs ev)
			{
				if (ev.Cancelled || ev.Error != null)
				{
					result = false;
				}
				else
				{
					mailMessage.Dispose();
				}
			};
			return result;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.ToString(), ex);
		}
	}

	public bool EnviarCorreo(EmailVM email, Stream streamAdjunto, string nombreArchivoAdjunto)
	{
		bool result = true;
		try
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			MailMessage mailMessage = new MailMessage();
			mailMessage.To.Add(new MailAddress(email.destinatarios));
			mailMessage.From = new MailAddress(cuentaServidorCorreo);
			mailMessage.Subject = email.asunto;
			mailMessage.Body = email.mensaje;
			mailMessage.IsBodyHtml = true;
			mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(WebUtility.HtmlDecode(email.mensaje), null, "text/html"));
			mailMessage.BodyEncoding = Encoding.UTF8;
			mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
			mailMessage.Attachments.Add(new Attachment(streamAdjunto, nombreArchivoAdjunto, "application/pdf"));
			smtpClient.Send(mailMessage);
			smtpClient.SendCompleted += delegate(object send, AsyncCompletedEventArgs ev)
			{
				if (ev.Cancelled || ev.Error != null)
				{
					result = false;
				}
				else
				{
					mailMessage.Dispose();
				}
			};
			return result;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.ToString(), ex);
		}
	}
}
