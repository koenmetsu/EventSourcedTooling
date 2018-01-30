using System.Collections.Generic;

namespace EventSourcedTooling {
	public class AddProductToCart{
public AddProductToCart(string CartId, string SKU, string Price, string AddTime){
this.CartId = CartId;
this.SKU = SKU;
this.Price = Price;
this.AddTime = AddTime;
}
		public string CartId { get; set; }
		public string SKU { get; set; }
		public string Price { get; set; }
		public string AddTime { get; set; }
	}
}
