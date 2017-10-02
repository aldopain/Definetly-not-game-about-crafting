using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    private Int32 id;
    private Double chance;
    private Int32 minHeight;
    private Int32 maxHeight;
    private Int32 minAmount;
    private Int32 maxAmount;

    public Spawner(List<String> currentOre)
    {
        Int32.TryParse(currentOre[0], out id);
        Double.TryParse(currentOre[1], out chance);
        Int32.TryParse(currentOre[2], out minHeight);
        Int32.TryParse(currentOre[3], out maxHeight);
        Int32.TryParse(currentOre[4], out minAmount);
        Int32.TryParse(currentOre[5], out maxAmount);
    }

    public void TryToSpawn(Point pointOfSpawn, Int32[,] landscape)
    {
        using(System.Random rng = new System.Random())
        {
            if(((Double)rng.Next(100))/100 <= this.chance)
            {
                this.Spawn(pointOfSpawn, landscape);
            }
        }
    }

    private void Spawn(Point pointOfSpawn, Int32[,] landscape)
    {
        landscape[pointOfSpawn.X][pointOfSpawn.Y] = this.id;
        List<Point> cells = new List<Point>;
        List<Point> spawnedCells = new List<Point>;
        Int32 amount = (new System.Random()).Next(this.maxAmount - this.minAmount) + this.minAmount;
        Int32[] step = {0, 1, -1};
        Boolean communismBroke = false;
        spawnedCells.Add(pointOfSpawn);
        for(Int32 i = 0; i < amount; i++)
        {
            for(Int32 k = 0; k < spawnedCells.Length; k++)
            {
                
                foreach (Int32 itemX in step)
                {
                    foreach (Int32 itemY in step)
                    {
                        if(itemX != 0 && itemY != 0)
                        {
                            try
                            {
                             cells.Add(new Point(pointOfSpawn.X + itemX, pointOfSpawn.Y + itemY));
                            }
                            catch (System.Exception)
                            {
                            }
                        }
                    }
                }
                cells.RemoveAll(x => landscape[x.X][x.Y] != 1);
                if(cells.IsEmpty)
                {
                    communismBroke = true;
                    break;
                }
                using(System.Random rng = new System.Random())
                {
                    Point nextPoint = cells[rng.Next(cells.Length - 1)];
                }
                landscape[nextPoint.X][nextPoint.y] = this.id;
                spawnedCells.Add(nextPoint);
            }
            if(communismBroke)
            {
                //Understandable, have a great day
                break;
            }
        }
    }
}
