using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMTP_Test_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var client = new SmtpClient(textBoxServer.Text, int.Parse(textBoxPort.Text));
            client.EnableSsl = checkBoxSSL.Checked;

            client.Credentials = new System.Net.NetworkCredential(textBoxAccount.Text, textBoxPassword.Text);

            var message = new MailMessage(textBoxFrom.Text, textBoxTo.Text, textBoxSubject.Text, textBoxBody.Text);
            MessageBox.Show(string.Format("Sending an e-mail message to {0} by using SMTP host {1} port {2}.",
         message.To.ToString(), client.Host, client.Port));

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    m += "\n" + ex.Message;
                }

                MessageBox.Show(m);
            }
        }
    }
}
