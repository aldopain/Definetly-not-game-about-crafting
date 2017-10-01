using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	List<ItemInInventory> inventory;

	class ItemInInventory{
		public Item item;
		public int count;

		public ItemInInventory(Item i, int c){
			item = i;
			count = c;
		}
	}

	void Add(Item i, int c){
		ItemInInventory it = FindByID (i.GetID());
		if (it != null) {
			it.count += c;
		} else {
			inventory.Add (new ItemInInventory(i, c));
		}
	}

	bool Remove(string id, int c){
		ItemInInventory toRemove = FindByID (id);
		if (toRemove != null && toRemove.count >= c) {
			toRemove.count -= c;
			if (toRemove.count == 0)
				inventory.Remove (toRemove);
			return true;
		}
		return false;
	}

	ItemInInventory FindByID(string id){
		foreach (ItemInInventory i in inventory)
			if (i.item.GetID().Equals(id))
				return i;
		return null;
	}
}
