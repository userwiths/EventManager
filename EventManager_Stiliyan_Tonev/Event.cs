using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManager_Stiliyan_Tonev {
	public class Event {

		private string name;
		private string location;
		private DateTime start;
		private DateTime end;

		public Event (string name,string location,DateTime start,DateTime end)
		{
			this.name = name;
			this.location = location;
			this.start = start;
			this.end = end;
		}

		public string Name{
			get { return this.name; }
			set { this.name = value; }
		}

		public string Location {
			get { return this.location; }
			set { this.location = value; }
		}

		public DateTime Start{
			get { return this.start; }
			set { this.start = value; }
		}

		public DateTime End {
			get { return this.end; }
			set { this.end = value; }
		}

		public bool Before(Event a){
			return Start.CompareTo (a.Start) < 0;
		}

		public bool After(Event a){
			return Start.CompareTo (a.End) > 0;
		}

		public bool HasPassed(){
			return End.CompareTo (DateTime.Now)<0;
		}
		public bool HasStarted (){
			return Start.CompareTo (DateTime.Now) < 0;
		}

		public bool During (Event tempEvent)
		{
			//Check if the current event beggins OR ends during the given event.
			return (Start.CompareTo (tempEvent.Start) > -1 &&
				Start.CompareTo (tempEvent.End) < 1) ||
				(End.CompareTo (tempEvent.Start) > -1 &&
				End.CompareTo (tempEvent.End) < 1);
		}

        public bool During(DateTime date) {
            return Start.CompareTo(date) <= 0 && End.CompareTo(date) >= 0;
        }

		public override string ToString ()
		{
			return string.Format ("{0} will be at {1}, from {2} to {3}.", Name, Location, Start, End);
		}
	}
}
