using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct CustomerStartedShopping : IEvent {
public CustomerStartedShopping(string CustomerId, string CartId){
this.CustomerId = CustomerId;
this.CartId = CartId;
}
		public string CustomerId { get; private set; }
		public string CartId { get; private set; }
	}
}
