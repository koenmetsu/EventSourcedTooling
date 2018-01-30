using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct ProductWasAddedToCart : IEvent {
public ProductWasAddedToCart(string CustomerId, string CartId, string SKU, string Price, string AddedAt){
this.CustomerId = CustomerId;
this.CartId = CartId;
this.SKU = SKU;
this.Price = Price;
this.AddedAt = AddedAt;
}
		public string CustomerId { get; private set; }
		public string CartId { get; private set; }
		public string SKU { get; private set; }
		public string Price { get; private set; }
		public string AddedAt { get; private set; }
	}
}
