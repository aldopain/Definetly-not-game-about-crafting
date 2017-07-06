using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public List<GameObject> allItems;
	public GameObject ItemPattern;
	// Use this for initialization
	void Start () {
		getAllItems ();
	}

	void getAllItems(){
		List<List<string>> db = DatabaseManager.getDB (new string[]{"ID","CraftFrom","CraftedCount","Name","Enable"}, "Craft.db", "Objects");
		foreach (List<string> current in db) {
			GameObject toAdd = Instantiate (ItemPattern);
			Item it = toAdd.GetComponent<Item> ();
			it.inizialize (current);
			toAdd.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/" + it.GetID ());
			allItems.Add (toAdd);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
