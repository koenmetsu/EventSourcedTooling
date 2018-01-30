using System.Collections.Generic;

namespace EventSourcedTooling {
	public struct RemoveProductFromCart : ICommand {
public RemoveProductFromCart(string CartId, string SKU, string RemoveTime){
this.CartId = CartId;
this.SKU = SKU;
this.RemoveTime = RemoveTime;
}
		public string CartId { get; private set; }
		public string SKU { get; private set; }
		public string RemoveTime { get; private set; }
	}
}
