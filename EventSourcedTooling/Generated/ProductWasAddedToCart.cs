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
		public string CustomerId { get; set; }
		public string CartId { get; set; }
		public string SKU { get; set; }
		public string Price { get; set; }
		public string AddedAt { get; set; }
	}
}
