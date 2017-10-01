using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandscapeGenerator {
	public const int maxHeight = 100;
	public const int lenght = 200;

	public const int GRASS = 10; 
	public const int GROUND = 1;
	public const int RIGHT_CORNER_GRASS =  -5;
	public const int LEFT_CORNER_GRASS = 5;

	const int CAVE_START_Y = 50;
	const int LAND_START_Y = 80;

	int[,] landscape = new int[maxHeight, lenght];
	int[] caveUpHeights = new int[lenght];
	int[] caveDownHeights = new int[lenght];
	int[] heights = new int[lenght];
	System.Random rng = new System.Random();

	void Start () {
		generateLandscape ();
	}

	void generateCaveHeights(){
		makeHeights(caveUpHeights, 20, 2, 25);
		makeHeights(caveDownHeights, 20, 2, 25);
	}

	void makeHeights(int[] heightsArr, int pillarCount, int minH, int maxH){
		makePillarHeights(heightsArr, pillarCount, minH, maxH);
		makeAllNonPillarHeights(heightsArr, pillarCount);
	}

	void makePillarHeights(int[] heightsArr, int count, int minH, int maxH){
		int step = lenght/(count - 1);
		heightsArr[0] = rng.Next(minH, maxH);
		for(int i = 1; i < count; i++){
			heightsArr[i * step - 1] = rng.Next(minH, maxH);
		}
		if(count * step - 1 != lenght - 1)
			heightsArr[lenght - 1] = rng.Next(minH, maxH);
	}

	void makeAllNonPillarHeights(int[] heightsArr, int pillarCount){
		int step = lenght/(pillarCount - 1);
		makeNonPillarHeights(heightsArr, 0, step - 1);
		for(int i = 1; i < pillarCount - 1; i++){
			makeNonPillarHeights(heightsArr, step * i - 1, step * (i + 1) - 1);
		}
		if(step * (pillarCount - 1) != lenght - 1)
			makeNonPillarHeights(heightsArr, step * (pillarCount - 2), lenght - 1);
	}

	void makeNonPillarHeights(int[] heightsArr, int start, int finish){
		for(int i = 0; i < finish - start; i++){
			heightsArr[start + i] = heightsArr[start] - i*(heightsArr[start]-heightsArr[finish])/(finish - start) + rng.Next(0, 0);
		}
	}

	void inizializeLandscapeMatrix(){
		for(int j = 0; j < lenght; j++){
			for(int i = 0; i < heights[j]; i++)
				landscape[i + LAND_START_Y, j] = GROUND;
		}
		for(int j = 0; j < lenght; j++){
			for(int i = 0; i < caveDownHeights[j]; i++)
				landscape[i, j] = GROUND;
		}
		for(int j = 0; j < lenght; j++){
			for(int i = 0; i < caveUpHeights[j]; i++)
				landscape[CAVE_START_Y - i, j] = GROUND;
		}
		CellularAutomata generator = new CellularAutomata ();
		int[,] caveSystem = generator.getMatrix ();
		for(int i = 0; i < LAND_START_Y - CAVE_START_Y; i++)
			for(int j = 0; j < lenght; j++)
				landscape[i + CAVE_START_Y, j] = caveSystem[i, j];
	}

	void testAlgorithm(){
		for(int j = 1; j < (lenght-1); j++){
			if ((landscape [heights [j] + LAND_START_Y, j + 1] == 0) && (landscape [heights [j] + LAND_START_Y, j - 1] == GROUND))
				landscape [heights [j] + LAND_START_Y, j] = LEFT_CORNER_GRASS;
			else if ((landscape [heights [j] + LAND_START_Y, j + 1] == GROUND) && (landscape [heights [j] + LAND_START_Y, j - 1] == 0))
				landscape [heights [j] + LAND_START_Y, j] = RIGHT_CORNER_GRASS;
		}
	}

	void testAlgorithm2(){
		int j = 0;
		int i = (lenght - 1);
		if(landscape[heights[j] + LAND_START_Y, j + 1] == GROUND)
			landscape[heights[j] + LAND_START_Y, j] = RIGHT_CORNER_GRASS;
		if(landscape[heights[i] + LAND_START_Y, i - 1] == GROUND)
			landscape[heights[i] + LAND_START_Y, i] = LEFT_CORNER_GRASS;
	} 

	void testAlgorithm3(){
		for(int j = 0; j < lenght; j++){
			if((heights[j] != 0) && (landscape[heights[j] + LAND_START_Y, j] == 0))
				landscape[heights[j] + LAND_START_Y - 1, j] = GRASS;
		}
	}

	public int[,] generateLandscape(){
		makeHeights(heights, lenght/25 + 1, 2, 20);
		generateCaveHeights ();
		inizializeLandscapeMatrix();
		testAlgorithm();
		testAlgorithm2();
		testAlgorithm3();
		return landscape;
	}
}
