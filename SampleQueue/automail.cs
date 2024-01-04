using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    class automail
    {
        string style = "", stage = "";
        string smtpAddress = "103.15.48.25";
        int portNumber = 25;
        bool enableSSL = false;

        string emailFrom = "alert.mess@allianceone.com.vn";
        string password = "5RLXcy]5fL}=@#!@";
        string emailTo = "vancho@allianceoneapparel.com";//"it@allianceone.com.vn";
        string subject = "Urgent Sample Queue";
        public automail(string _style, string _stage)
        {
            style = _style; stage = _stage;
        }

        public void SenMail()
        {
            try
            {
                string body = @" MER sent to you 1 urgent Sample Queue <br>
                            - Style : " + style + @".<br>
                            - Stage : " + stage + @".<br>
                            Please check it and make your dicision about this sample";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        MessageBox.Show("sent mail");
                        smtp.SendCompleted += smtp_SendCompleted;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("sent mail fail    " + ex.ToString());
            }
        }
        void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true || e.Error != null)
            {
                throw new Exception(e.Cancelled ? "EMail sedning was canceled." : "Error: " + e.Error.ToString());
            }
        }
    }
}
