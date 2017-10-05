using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour{
	public GameObject[] groundBlocks;
	public GameObject[] cornerBlocks;
	public GameObject[] grassBlocks;
	public GameObject[] oreBlocks;
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
			toCreate = Instantiate(cornerBlocks [rng.Next (0, 2)]);
			break;
		case LandscapeGenerator.RIGHT_CORNER_GRASS:
			toCreate = Instantiate(cornerBlocks [rng.Next  (3, 5)]);
			break;
		case 21:
			toCreate = Instantiate(oreBlocks [0]);
			print ("0");
			break;
		case 22:
			toCreate = Instantiate(oreBlocks [1]);
			print ("1");
			break;
		case 23:
			toCreate = Instantiate(oreBlocks [2]);
			print ("2");
			break;
		case 24:
			toCreate = Instantiate(oreBlocks [3]);
			print ("3");
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
