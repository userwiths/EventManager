using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager_Stiliyan_Tonev
{
    public partial class CreateEvent : Form
    {

        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public CreateEvent(DateTime start,DateTime end,string name="",string location="")
        {
            InitializeComponent();

            textBox1.Text = name;
            textBox2.Text = location;
            this.start.Value = start;
            this.end.Value = end;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void create_Click(object sender, EventArgs e)
        {
            Name = textBox1.Text;
            Location = textBox2.Text;
            Start = start.Value;
            End = end.Value;

            if (Name == "" || Name == null || Location == "" || Location == null)
            {
                this.DialogResult = DialogResult.Abort;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }

            this.Close();
        }


    }
}
