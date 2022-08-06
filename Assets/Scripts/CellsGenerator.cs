using Assets.Scripts.Interfaces;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CellsGenerator : ICellsGenerator
    {
        private readonly int _deathLimit;
        private readonly int _birthLimit;

        public CellsGenerator(int birthLimit, int deathLimit)
        {
            _birthLimit = birthLimit;
            _deathLimit = deathLimit;
        }

        public int[,] Generate(int[,] oldMap, int width, int height)
        {
            int[,] newMap = new int[width, height];
            int numberOfNeighbours;
            Parallel.For(0, width, (x, state) =>
            {
                for (int y = 0; y < height; y++)
                {
                    numberOfNeighbours = CountLivingNeighbours(oldMap, width, height, x, y);

                    newMap[x, y] = oldMap[x, y];
                    if (oldMap[x, y] == 1)
                    {
                        if (numberOfNeighbours < _deathLimit)
                        {
                            newMap[x, y] = 0;
                        }
                    }

                    if (oldMap[x, y] == 0)
                    {
                        if (numberOfNeighbours > _birthLimit)
                        {
                            newMap[x, y] = 1;
                        }
                    }
                }
            });

            return newMap;
        }

        private static int CountLivingNeighbours(int[,] oldMap, int width, int height, int posX, int posY)
        {
            int neighb = 0;
            BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

            foreach (var b in myB.allPositionsWithin)
            {
                if (b.x == 0 && b.y == 0) continue;
                int neighbX = posX + b.x;
                int neighbY = posY + b.y;
                if (neighbX >= 0 && neighbX < width && neighbY >= 0 && neighbY < height)
                {
                    neighb += oldMap[neighbX, neighbY];
                }
                else
                {
                    neighb += 1;
                }
            }

            return neighb;
        }

        public int[,] PopulateInitialMap(int width, int height, int chance)
        {
            int[,] newMap = new int[width, height];
            return SpawnRandomCells(newMap, width, height, chance);
        }

        public int[,] SpawnRandomCells(int[,] map, int width, int height, int chance)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = Random.Range(1, 101) < chance ? 1 : 0;
                }
            }
            return map;
        }
    }
}