using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {
	public List<Item> inventory;
	public List<String> inventoryItemsIDs;
	public Item toCraft;
	List<String> needed;
	Rigidbody2D rb;
	public bool grounded;
	public float jumpForce;
	public float speed;
	public float upSpeed;

	void Start(){
		rb = gameObject.GetComponent<Rigidbody2D>();
		inventory = new List<Item>();
		inventoryItemsIDs = new List<String> ();
		grounded = true;
	}

	List<string> getRecipe(){
		return null;
	}

	bool isCraftPossible(Item itemToCraft){
		List<Item> buf = new List<Item> (inventory);
		if(!itemToCraft.IsRecipeEnable())
			return false;
		needed = itemToCraft.getRecipe ();
		List<String> invBuf = new List<String> (inventoryItemsIDs);
		for(int i = 0; i < needed.Count; i++){
			if (invBuf.Contains (needed [i])) {
				invBuf.Remove (needed [i]);
			} else
				return false;
		}
		return true;
	}

	void tryToCraft(){
		if (isCraftPossible (toCraft)) {
			Instantiate (toCraft);
		}
	}

	void FixedUpdate () {
		if (Input.GetKey (KeyCode.UpArrow) && grounded) {
			jump ();
			grounded = false;
		}
	}

	void jump(){
		rb.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			tryToCraft ();
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			rb.AddForce(new Vector2(-speed, upSpeed));
			//rb.AddForce (Vector2.left * 0.3f, ForceMode2D.Impulse);
			//rb.velocity = new Vector2(-5, 0);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			rb.AddForce(new Vector2(speed, upSpeed));
			//rb.velocity = new Vector2(5, 0);
		}
	}
}
