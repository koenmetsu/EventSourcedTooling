using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct ProductWasRemovedFromCart{
public ProductWasRemovedFromCart(string CustomerId, string CartId, string SKU, string RemovedAt){
this.CustomerId = CustomerId;
this.CartId = CartId;
this.SKU = SKU;
this.RemovedAt = RemovedAt;
}
		public string CustomerId { get; set; }
		public string CartId { get; set; }
		public string SKU { get; set; }
		public string RemovedAt { get; set; }
	}
}
