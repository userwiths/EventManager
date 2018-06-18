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
    public partial class MainGUI : Form
    {
       // private DateTime marked;
        private EventManager manager;

        public MainGUI()
        {
            InitializeComponent();
            manager = new EventManager();
        }

        private void MainGUI_Load(object sender, EventArgs e)
        {
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.RemoveAllBoldedDates();
            foreach (var item in manager.Events)
            {
                monthCalendar1.AddBoldedDate(item.Start);
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            var onThatDate = manager.During(monthCalendar1.SelectionStart);
            checkedListBox1.Items.Clear();
            monthCalendar1.RemoveAllBoldedDates();
            if (onThatDate.Count!=0)
            {
                monthCalendar1.AddBoldedDate(monthCalendar1.SelectionStart);
                foreach (var item in onThatDate)
                {
                    checkedListBox1.Items.Add(item.ToString());
                    monthCalendar1.AddBoldedDate(item.End);
                }
            }
        }

        private void create_Click(object sender, EventArgs e)
        {
            CreateEvent create = new CreateEvent(monthCalendar1.SelectionStart, monthCalendar1.SelectionStart);
            create.ShowDialog();
            
            if(create.DialogResult == DialogResult.OK) {
                manager.AddEvent(new Event(create.Name, create.Location, create.Start, create.End));
            }
            if (manager.Messages.Count > 0) {
                MessageBox.Show(manager.Messages[0], "Message", MessageBoxButtons.OKCancel);
                manager.Messages.RemoveAt(0);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            var onThatDate = manager.During(monthCalendar1.SelectionStart);
            foreach (string item in checkedListBox1.CheckedItems)
            {
                for (int i = 0; i < onThatDate.Count; i++)
                {
                    if (item == onThatDate[i].ToString()) {
                        manager.RemoveEvent(onThatDate[i]);
                        if (manager.Messages.Count > 0)
                        {
                            MessageBox.Show(manager.Messages[0], "Message", MessageBoxButtons.OKCancel);
                            manager.Messages.RemoveAt(0);
                        }
                    }
                }
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            var onThatDate = manager.During(monthCalendar1.SelectionStart);
            CreateEvent create;
            foreach (string item in checkedListBox1.CheckedItems)
            {
                for (int i = 0; i < onThatDate.Count; i++)
                {
                    if (item == onThatDate[i].ToString())
                    {
                        create = new CreateEvent(onThatDate[i].Start, onThatDate[i].End, onThatDate[i].Name, onThatDate[i].Location);
                        create.ShowDialog();
                        if (create.DialogResult == DialogResult.OK)
                        {
                            manager.EditEvent(onThatDate[i], new Event(create.Name, create.Location, create.Start, create.End));
                        }
                        if (manager.Messages.Count > 0)
                        {
                            MessageBox.Show(manager.Messages[0], "Message", MessageBoxButtons.OKCancel);
                            manager.Messages.RemoveAt(0);
                        }
                    }
                }
            }
        }

        private void MainGUI_Leave(object sender, EventArgs e)
        {
            manager.SaveAll();
        }

        private void MainGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            manager.SaveAll();
        }

        private void export_Click(object sender, EventArgs e)
        {
            Export exp = new Export(manager.Save);
            exp.ShowDialog();
        }


    }
}
