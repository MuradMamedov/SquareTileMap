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

                    if (oldMap[x, y] == 1)
                    {
                        if (numberOfNeighbours < _deathLimit) newMap[x, y] = 0;

                        else
                        {
                            newMap[x, y] = 1;

                        }
                    }

                    if (oldMap[x, y] == 0)
                    {
                        if (numberOfNeighbours > _birthLimit) newMap[x, y] = 1;

                        else
                        {
                            newMap[x, y] = 0;
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
                if (posX + b.x >= 0 && posX + b.x < width && posY + b.y >= 0 && posY + b.y < height)
                {
                    neighb += oldMap[posX + b.x, posY + b.y];
                }
                else
                {
                    neighb++;
                }
            }

            return neighb;
        }

        public int[,] PopulateInitialMap(int width, int height, int chance)
        {
            int[,] newMap = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    newMap[x, y] = Random.Range(1, 101) < chance ? 1 : 0;
                }
            }
            return newMap;
        }
    }
}