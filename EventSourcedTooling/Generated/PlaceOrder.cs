using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct PlaceOrder : ICommand {
public PlaceOrder(string CartId, string CustomerId, string OrderTime){
this.CartId = CartId;
this.CustomerId = CustomerId;
this.OrderTime = OrderTime;
}
		public string CartId { get; private set; }
		public string CustomerId { get; private set; }
		public string OrderTime { get; private set; }
	}
}
