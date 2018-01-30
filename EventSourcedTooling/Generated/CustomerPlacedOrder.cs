using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct CustomerPlacedOrder : IEvent {
public CustomerPlacedOrder(string CustomerId, string CartId, List<Product> Products, string OrderedAt){
this.CustomerId = CustomerId;
this.CartId = CartId;
this.Products = Products;
this.OrderedAt = OrderedAt;
}
		public string CustomerId { get; private set; }
		public string CartId { get; private set; }
		public List<Product> Products { get; set; }
		public string OrderedAt { get; private set; }
	}
	public struct Product : IEvent {
public Product(string SKU, string Quantity, string PriceInCents, string Currency){
this.SKU = SKU;
this.Quantity = Quantity;
this.PriceInCents = PriceInCents;
this.Currency = Currency;
}
		public string SKU { get; private set; }
		public string Quantity { get; private set; }
		public string PriceInCents { get; private set; }
		public string Currency { get; private set; }
	}
}
