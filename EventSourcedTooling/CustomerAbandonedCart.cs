using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct CustomerAbandonedCart{
public CustomerAbandonedCart(string CustomerId, string CartId){
this.CustomerId = CustomerId;
this.CartId = CartId;
}
		public string CustomerId { get; set; }
		public string CartId { get; set; }
	}
}
