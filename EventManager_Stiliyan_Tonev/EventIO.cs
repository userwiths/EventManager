using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Windows.Forms;

namespace EventManager_Stiliyan_Tonev {
	public class EventIO {
		private static string conf = ".eventmanage.conf";
		private string fsave = "";
		private List<Event> events;

        public EventIO()
        {
            events = new List<Event>();
            
            if (!File.Exists(conf))
            {
                OpenFileDialog dialog = new OpenFileDialog();
                var writer = new StreamWriter(conf);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    writer.Write(dialog.FileName);
                    fsave = dialog.FileName;
                }
                else
                {
                    writer.Write("default0.save");
                    fsave = "default0.save";
                }
                writer.Close();
            }
            else
            {
                var read = new StreamReader(conf);
                fsave = read.ReadLine().Trim(new char[] { '\n', '\r', '\t', ' ' });
                read.Close();
            }

            if (!File.Exists(fsave)) {
                File.Create(fsave);
            }

            Load();
            Events.Sort(SortRule);
        }

        public string Save {
            get { return fsave; }
        }
		public List<Event> Events {
			get { return this.events; }
			set { this.events = value; }
		}

		public Event this [int index] {
			get { return index < events.Count ? events [index] : null; }
			set { if (index > events.Count) { return; } events [index] = value; }
		}

		public void SaveAll(){
			StreamWriter writer = new StreamWriter (fsave,false);
			foreach (var item in Events) {
				SaveEvent (item,writer);
			}
			writer.Flush ();
			writer.Close ();
		}

		public void SaveEvent(Event evn,StreamWriter writer){
			writer.Write (evn.Name);
			writer.Write(writer.NewLine);
			writer.Write (evn.Location);
			writer.Write (writer.NewLine);
			writer.Write (evn.Start.ToString ("dd/MM/yyyy HH:mm"));
			writer.Write (writer.NewLine);
			writer.Write (evn.End.ToString ("dd/MM/yyyy HH:mm"));
			writer.Write (writer.NewLine);
			writer.Write (writer.NewLine);
		}

		public void Load(){
			StreamReader read = new StreamReader (fsave);
			Event temp;
			while (!read.EndOfStream) {
                temp = LoadEvent(read);
                if (temp != null)
                {
                    Events.Add(temp);
                }
			}
			read.Close ();
		}

		public Event LoadEvent(StreamReader read){
			string name = "";
			string location = "";
			DateTime start;
			DateTime stop;

			try {
				name = read.ReadLine ();
				location = read.ReadLine ();
				DateTime.TryParseExact (read.ReadLine (), "dd/MM/yyyy HH:mm", null,
						DateTimeStyles.None, out start);
				DateTime.TryParseExact (read.ReadLine (), "dd/MM/yyyy HH:mm", null,
						DateTimeStyles.None, out stop);
				//Read the pending new line so the reader is ready for the next read.
				read.ReadLine ();
			}catch(IOException e){
				return null;
			}

			if(name=="" || location=="" || start==null || stop==null){
				return null;
			}
			return new Event (name, location, start, stop);
		}

		public void Export(string fname){
			File.Copy (fsave,fname);
		}

		public int SortRule(Event a,Event b){
			if(a.Before (b)){
				return 1;
			}else if(a.After (b)){
				return -1;
			}else{
				return 0;
			}
		}

		public override string ToString ()
		{
			string result = "";
			foreach (var item in events) {
				//Print and move to next line.
				result += item.ToString () + Environment.NewLine;

				//Add another empty line so different events don't stick to each other.
				result += Environment.NewLine;
			}
			return result;
		}
	}
}
