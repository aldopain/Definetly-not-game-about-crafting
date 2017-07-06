using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour{
	public GameObject[] groundBlocks;
	public GameObject[] cornerBlocks;
	public GameObject[] grassBlocks;
	System.Random rng = new System.Random();
	const float size = 0.2f;
	Vector3 startPosition = new Vector3(-4f, -2f, 0f);

	void createBlock(int value, int x, int y){
		GameObject toCreate = null;
		switch(value){
		case LandscapeGenerator.GROUND:
			toCreate = Instantiate (groundBlocks [rng.Next (0, 2)]);
			break;
		case LandscapeGenerator.GRASS:
			toCreate = Instantiate(grassBlocks [rng.Next  (0, 2)]);
			break;
		case LandscapeGenerator.LEFT_CORNER_GRASS:
			toCreate = cornerBlocks [rng.Next (0, 2)];
			print ("lol");
			break;
		case LandscapeGenerator.RIGHT_CORNER_GRASS:
			toCreate = cornerBlocks [rng.Next  (3, 5)];
			break;
		}
		if (value != 0) {
			toCreate.transform.position = new Vector3 (size*x, size*y, 0) + startPosition;
		}
	}

	void Start(){
		int[,] landscape = new LandscapeGenerator ().generateLandscape ();
		for (int i = 0; i < LandscapeGenerator.maxHeight; i++)
			for (int j = 0; j < LandscapeGenerator.lenght; j++) 
				createBlock (landscape [i, j], j, i);
	}
}
