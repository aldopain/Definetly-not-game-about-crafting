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

	//надо переделать
	void getAllItems(){
		//TODO
		List<List<string>> db = null;
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
