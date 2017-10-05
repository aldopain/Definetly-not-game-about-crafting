using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner
{
    private Int32 id;
    private Double chance;
    private Int32 minHeight;
    private Int32 maxHeight;
    private Int32 minAmount;
    private Int32 maxAmount;
    public Int32[,] landscape;

    public Spawner(List<String> currentOre, Int32[,] landscape)
    {
        Int32.TryParse(currentOre[0], out id);
        Double.TryParse(currentOre[1], out chance);
        Int32.TryParse(currentOre[2], out minHeight);
        Int32.TryParse(currentOre[3], out maxHeight);
        Int32.TryParse(currentOre[4], out minAmount);
        Int32.TryParse(currentOre[5], out maxAmount);
        this.landscape = landscape;
    }

	public int[,] TryToSpawn(Vector2 pointOfSpawn)
    {
        System.Random rng = new System.Random();
        Double kek = ((Double)rng.Next(100)) / 100;
        Thread.Sleep(1);
		if (kek <= this.chance)
        {
           this.Spawn(pointOfSpawn);
        }
		return landscape;
    }

    public void GetInfo()
    {
        //print("id = {0}, chance = {1}, minHeight = {2}, maxHeight = {3}, minAmount = {4}, maxAmount = {5}", id, chance, minHeight, maxHeight, minAmount, maxAmount);
    }

    public Int32 GetMaxHeight()
    {
        return this.maxHeight;
    }

    public Int32 GetMinHeight()
    {
        return this.minHeight;
    }

    private void Spawn(Vector2 pointOfSpawn)
    {
		landscape[(int)pointOfSpawn.x, (int)pointOfSpawn.y] = this.id;
        List<Vector2> cells = new List<Vector2>();
        List<Vector2> spawnedCells = new List<Vector2>();
        Int32 amount = (new System.Random()).Next(this.maxAmount - this.minAmount) + this.minAmount;
        Int32[] step = {0, 1, -1};
        Boolean communismBroke = false;
        spawnedCells.Add(pointOfSpawn);
        for(Int32 i = 0; i < amount - 1; i++)
        {
            for(Int32 k = 0; k < spawnedCells.Count; k++)
            {
                foreach (Int32 itemX in step)
                {
                    foreach (Int32 itemY in step)
                    {
                        if(itemX != 0 && itemY != 0)
                        {
                            if (spawnedCells[k].x + itemX > 0 && spawnedCells[k].x + itemX < landscape.GetLength(0) - 1 && spawnedCells[k].y + itemY >= minHeight - 1 && spawnedCells[k].y + itemY <= maxHeight - 1)
                            {
								if (landscape[(int)spawnedCells[k].x + itemX, (int)spawnedCells[k].y + itemY] == 1)
                                {
                                    cells.Add(new Vector2(spawnedCells[k].x + itemX, spawnedCells[k].y + itemY));
                                }
                            }
                        }
                    }
                }
				cells.RemoveAll(x => landscape[(int)x.x, (int)x.y] != 1);
                
                if(cells.Count == 0)
                {
                    communismBroke = true;
                    break;
                }
                
            }
            if (communismBroke)
            {
                //Understandable, have a great day
                break;
            }
            System.Random rng = new System.Random();
            Vector2 nextVector2 = cells[rng.Next(cells.Count - 1)];
			landscape[(int)nextVector2.x, (int)nextVector2.y] = this.id;
            spawnedCells.Add(nextVector2);
            
        }
    }
}
