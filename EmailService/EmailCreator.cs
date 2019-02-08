using EmailService.Interfaces;
using Microsoft.Win32.SafeHandles;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;

namespace EmailService
{
    /// <summary>
    /// Fluent interface based email sender
    /// </summary>
    public class EmailCreator : IEmail1, IEmail2, IEmail3, IEmail4, IEmail5, IEmail6, IEmail7, IEmail8, IDisposable
    {
        private readonly MailMessage _mailMessage = new MailMessage();

        private const string PdfExtension = ".pdf";
        private const string OctetExtension = ".octet";
        private const string SoapExtension = ".xml";
        private const string ZipExtension = ".zip";
        private const string RtfExtension = ".rtf";

        private string _smtpClient;
        private int _port;
        private string _username;
        private string _password;

        private EmailCreator(string fromAddress)
        {
            _mailMessage.From = new MailAddress(fromAddress);
        }

        public static IEmail1 From(string fromAddress)
        {
            return new EmailCreator(fromAddress);
        }

        public IEmail2 To(params string[] toAddresses)
        {
            foreach (var toAddress in toAddresses)
            {
                if (!string.IsNullOrEmpty(toAddress))
                {
                    _mailMessage.To.Add(new MailAddress(toAddress));
                }
            }

            return this;
        }

        public IEmail3 CC(params string[] ccAddresses)
        {
            foreach (var ccAddress in ccAddresses)
            {
                if (!string.IsNullOrEmpty(ccAddress))
                {
                    _mailMessage.CC.Add(new MailAddress(ccAddress));
                }
            }

            return this;
        }

        public IEmail4 BCC(params string[] bccAddresses)
        {
            foreach (var bccAddress in bccAddresses)
            {
                if (!string.IsNullOrEmpty(bccAddress))
                {
                    _mailMessage.Bcc.Add(new MailAddress(bccAddress));
                }
            }

            return this;
        }

        IEmail5 IEmail3.BCC(params string[] bccAddresses)
        {
            foreach (var bccAddress in bccAddresses)
            {
                if (!string.IsNullOrEmpty(bccAddress))
                {
                    _mailMessage.Bcc.Add(new MailAddress(bccAddress));
                }
            }

            return this;
        }

        IEmail5 IEmail4.CC(params string[] ccAddresses)
        {
            foreach (var ccAddress in ccAddresses)
            {
                if (!string.IsNullOrEmpty(ccAddress))
                {
                    _mailMessage.CC.Add(new MailAddress(ccAddress));
                }
            }

            return this;
        }

        public IEmail6 Subject(string subject)
        {
            _mailMessage.Subject = subject;

            return this;
        }


        /// <summary>
        /// MediaTypeNames
        /// </summary>
        /// <param name="body"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public IEmail7 Body(string body, string mediaType = "")
        {
            switch (mediaType)
            {
                case MediaTypeNames.Text.Html:
                    _mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Html));
                    break;
                case MediaTypeNames.Text.Xml:
                    _mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Xml));
                    break;
                case MediaTypeNames.Text.Plain:
                    _mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Plain));
                    break;
                case MediaTypeNames.Text.RichText:
                    _mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.RichText));
                    break;
                default:
                    _mailMessage.Body = body;
                    break;
            }

            return this;
        }

        /// <summary>
        /// Send multiple files
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        public IEmail7 Attachment(params string[] filePaths)
        {
            foreach (var file in filePaths)
            {
                if (!string.IsNullOrEmpty(file))
                {
                    var attachment = new Attachment(file);
                    _mailMessage.Attachments.Add(attachment);
                }
            }
            return this;
        }

        /// <summary>
        /// To generate pdf file and send it
        /// </summary>
        /// <param name="memoryStreams"></param>
        /// <returns></returns>
        public IEmail7 Attachment(params GenFile[] memoryStreams)
        {
            foreach (var file in memoryStreams)
            {
                if (file.MemoryStream != null)
                {
                    string mediaType = "";
                    string fileExtension = "";

                    switch (file.MediaType)
                    {
                        case MediaTypeNames.Application.Pdf:
                            mediaType = MediaTypeNames.Application.Pdf;
                            fileExtension = PdfExtension;
                            break;
                        case MediaTypeNames.Application.Octet:
                            mediaType = MediaTypeNames.Application.Octet;
                            fileExtension = OctetExtension;
                            break;
                        case MediaTypeNames.Application.Rtf:
                            mediaType = MediaTypeNames.Application.Rtf;
                            fileExtension = RtfExtension;
                            break;
                        case MediaTypeNames.Application.Soap:
                            mediaType = MediaTypeNames.Application.Soap;
                            fileExtension = SoapExtension;
                            break;
                        case MediaTypeNames.Application.Zip:
                            mediaType = MediaTypeNames.Application.Zip;
                            fileExtension = ZipExtension;
                            break;
                    }

                    if (!string.IsNullOrEmpty(mediaType))
                    {
                        var attachment = new Attachment(file.MemoryStream, string.Format("{0}{1}", file.FileName, fileExtension), mediaType);
                        _mailMessage.Attachments.Add(attachment);
                    }
                }
            }
            return this;
        }

        public void Send()
        {
            //_smtpClient = ConfigurationManager.AppSettings["SmtpClient"];
            //_port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            //_username = ConfigurationManager.AppSettings["Username"];
            //_password = ConfigurationManager.AppSettings["Password"];

            _smtpClient = "mx.agservice.lt";
            _port = 25;
            _username = "prog@agservice.lt";
            _password = "pr0g!";

            var smtpClient = new SmtpClient(_smtpClient)
            {
                Port = _port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_username, _password)
            };

            smtpClient.Send(_mailMessage);

            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _handle.Dispose();
                _mailMessage.Attachments.Dispose();
                _mailMessage.AlternateViews.Dispose();
                _mailMessage.Dispose();
            }

            _disposed = true;
        }
    }
}
