using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour {
	public string id;
	private string recipe;
	private int craftCount;
	private string itemName;
	private bool isRecipeEnable;

	// Use this for initialization
	void Start () {
		
	}

	public List<String> getRecipe(){
		List<String> idWithCounts = new List<String> ();
		List<String> recipeItemsIDs = new List<String> ();
		char[] recArr = recipe.ToCharArray ();
		String buf = "";
		for (int i = 0; i < recArr.Length; i++) {
			if (recArr [i] != ' ')
				buf += recArr [i];
			else {
				idWithCounts.Add (buf);
				buf = "";
			}
		}
		foreach (String current in idWithCounts) {
			int count = 1;
			String id = "";
			int i = 0;
			bool f = true;
			while(i < current.Length && f){
				if (current [i] != 'x')
					id += current [i];
				else
					f = false;
				i++;
			}
			if(i < current.Length - 1){
				count = Int32.Parse (current.Substring (i));
			}
			for(i = 0; i < count; i++)
				recipeItemsIDs.Add (id);
		}
		foreach (String a in recipeItemsIDs)
			print (a);
		return recipeItemsIDs;
	}

	public string GetID(){
		return id;
	}

	public bool IsRecipeEnable (){
		return isRecipeEnable;
	}

	public void inizialize(List<string> data){
		id = data [0];
		recipe = data [1];
		craftCount = Int32.Parse(data [2]);
		name = itemName = data [3];
		isRecipeEnable = data [4].Equals ("1") ? true : false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
