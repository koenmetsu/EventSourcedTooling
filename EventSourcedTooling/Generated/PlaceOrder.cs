using System.Collections.Generic;

namespace EventSourcedTooling {
	public class PlaceOrder{
public PlaceOrder(string CartId, string CustomerId, string OrderTime){
this.CartId = CartId;
this.CustomerId = CustomerId;
this.OrderTime = OrderTime;
}
		public string CartId { get; set; }
		public string CustomerId { get; set; }
		public string OrderTime { get; set; }
	}
}
