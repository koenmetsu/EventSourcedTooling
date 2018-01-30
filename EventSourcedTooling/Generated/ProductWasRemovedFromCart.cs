using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct ProductWasRemovedFromCart : IEvent {
public ProductWasRemovedFromCart(string CustomerId, string CartId, string SKU, string RemovedAt){
this.CustomerId = CustomerId;
this.CartId = CartId;
this.SKU = SKU;
this.RemovedAt = RemovedAt;
}
		public string CustomerId { get; private set; }
		public string CartId { get; private set; }
		public string SKU { get; private set; }
		public string RemovedAt { get; private set; }
	}
}
