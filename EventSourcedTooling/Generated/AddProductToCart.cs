using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct AddProductToCart : ICommand {
public AddProductToCart(string CartId, string SKU, string Price, string AddTime){
this.CartId = CartId;
this.SKU = SKU;
this.Price = Price;
this.AddTime = AddTime;
}
		public string CartId { get; private set; }
		public string SKU { get; private set; }
		public string Price { get; private set; }
		public string AddTime { get; private set; }
	}
}
