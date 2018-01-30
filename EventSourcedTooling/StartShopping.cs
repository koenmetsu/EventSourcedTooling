using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct StartShopping{
public StartShopping(string CustomerId, string CartId, string StartTime){
this.CustomerId = CustomerId;
this.CartId = CartId;
this.StartTime = StartTime;
}
		public string CustomerId { get; set; }
		public string CartId { get; set; }
		public string StartTime { get; set; }
	}
}
