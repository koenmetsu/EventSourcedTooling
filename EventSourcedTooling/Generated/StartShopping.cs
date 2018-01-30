using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct StartShopping : ICommand {
public StartShopping(string CustomerId, string CartId, string StartTime){
this.CustomerId = CustomerId;
this.CartId = CartId;
this.StartTime = StartTime;
}
		public string CustomerId { get; private set; }
		public string CartId { get; private set; }
		public string StartTime { get; private set; }
	}
}
