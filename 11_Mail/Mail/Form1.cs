using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;


namespace Mail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void SendMessage()
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(tbFrom.Text);
                mm.To.Add(new MailAddress(tbTo.Text));
                mm.Subject = tbSubject.Text;
                mm.Body = tbMessage.Text;
                var client = new SmtpClient("smtp.yandex.ru", 25);
                client.Credentials = new NetworkCredential("k.kataeva@westmedica.com", "password");
                client.EnableSsl = true;
                client.Timeout = 2000;
                client.Send(mm);

            }
            catch (ArgumentException)
            {
                MessageBox.Show("You must type from and to e-mail addresses", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
