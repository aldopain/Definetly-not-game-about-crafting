﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//клеточный автомат для генерации пещер
public class CellularAutomata {

	const int height = 30;
	const int lenght = 200;
	int[,] cave = new int[height, lenght];
	float spawnChance = 0.48f;
	int neighborAmountToSpawn = 4;
	int neighborAmountToDie = 3;
	int stepCount = 16;
	System.Random rng = new System.Random();

	//инициализация единицами с вероятностью spawnChance для каждой ячейки
	void inizialize(){
		for(int i = 0; i < height; i++)
			for(int j = 0; j < lenght; j++)
				if(rng.NextDouble() <= spawnChance)
					cave[i, j] = 1;
	}

	//возвращает элемент по индексу [y, x], если он есть
	int getElement(int x, int y){
		try{
			return cave[y, x];
		}catch(Exception e){
			return -1;   
		}
	}

	//считает количество соседей клетки [y, x]
	int countNeighbors(int x, int y){
		int[] iter = {1, -1, 0};
		int count = 0;
		for(int i = 0; i < 3; i++)
			for(int j = 0; j < 3; j++)
				if(!(j == 0 && i == 0) && getElement(x + iter[i], y + iter[j]) == 1)
					count++;
		return count;
	}

	//одна итерация клеточного автомата
	void iterationStep(){
		int[,] nextIterationCave = new int[height, lenght];
		for(int i = 0; i < height; i++)
			for(int j = 0; j < lenght; j++){
				int neighbors = countNeighbors(j, i);
				if(cave[i, j] == 1 && neighbors < neighborAmountToDie)
					nextIterationCave[i, j] = 0;
				else if(cave[i, j] == 0 && neighbors > neighborAmountToSpawn)
					nextIterationCave[i, j] = 1;
				else
					nextIterationCave[i, j] = cave[i, j];
			}
		cave = nextIterationCave;
	}

	//возвращает матрицу после stepCount итераций iterationStep()
	public int[,] getMatrix(){
		inizialize();
		for(int i = 0; i < stepCount; i++){
			iterationStep();
		}
		return cave;
	}
}
