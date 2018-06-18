using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManager_Stiliyan_Tonev {
	public class EventManager:EventIO {
		private List<String> messages;

		public EventManager ():base()
		{
			messages = new List<string> ();
            this.RemovePassed();
		}

		public List<string> Messages{
			get { return messages; }
			set { this.messages = value; }
		}

		public void AddEvent(Event evn){
			if(Contains (evn)){
				Messages.Add ("Event: "+evn.Name+" is alreay in the list.");
				return;
			}
			Events.Add (evn);
		}

		public void RemoveEvent(Event evn){
			if(!Contains (evn)){
				Messages.Add ("Event: "+evn.Name+" can not be found in the list.");
				return;
			}
			Events.Remove (evn);
		}

		public void EditEvent(Event old,Event nw){
			if(!Contains (old)){
				Messages.Add ("Event: "+old.Name+" Could not be found.");
				return;
			}
			if(Events.Contains (nw)){
				Messages.Add ("Event: "+nw.Name+" is already present.");
				return;
			}
            Events.Remove(old);
            Events.Add(nw);
		}

		private void RemovePassed(){
			var now = DateTime.Now;
			Events = Events.Where (x=> x.End.CompareTo (now)>-1).ToList ();
		}

		public bool Contains(Event evn){
			return Name (evn.Name).Count != 0;
		}

		public List<Event> StartBefore (DateTime date){
            return Events.Where (x => x.Start.CompareTo (date) == -1).ToList ();
        }

		public List<Event> EndBefore (DateTime date){
            return Events.Where (x => x.End.CompareTo (date) == -1).ToList ();
        }

		public List<Event> StartAfter (DateTime date){
            return Events.Where (x => x.Start.CompareTo (date) == 1).ToList ();
        }

		public List<Event> EndAfter (DateTime date){
            return Events.Where (x => x.End.CompareTo (date) == 1).ToList ();
        }

		public List<Event> StartOn (DateTime date){
            return Events.Where (x => x.Start.CompareTo (date) == 0).ToList ();
        }

		public List<Event> EndOn (DateTime date){
            return Events.Where (x => x.End.CompareTo (date) == 0).ToList ();
        }

		public List<Event> During (DateTime startDate, DateTime endDate){
            return Events.Where(x => (x.Start.CompareTo(startDate) > -1 && x.Start.CompareTo(endDate) < 1) ||
                (x.End.CompareTo(endDate) < 1 && x.End.CompareTo(startDate)>-1)).ToList();
		}

        public List<Event> During(DateTime date) {
            return Events.Where(x => x.During(date)).ToList();
        }

		public List<Event> At (string location){
            return Events.Where (x => x.Location == location).ToList ();
        }

        public List<Event> Name(string name)
        {
            return Events.Where(x => x.Name == name).ToList();
        }

	}
}
