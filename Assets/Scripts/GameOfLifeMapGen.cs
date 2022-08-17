using Assets.Scripts.Interfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameOfLifeMapGen : IMapGen
    {
        private readonly int _deathLimit;
        private readonly int _chance;
        private readonly int _birthLimit;

        public GameOfLifeMapGen(int birthLimit, int deathLimit, int chance)
        {
            _birthLimit = birthLimit;
            _deathLimit = deathLimit;
            _chance = chance;
        }

        public float[,] Generate(float[,] oldMap, int width, int height)
        {
            if (oldMap == null)
            {
                return PopulateInitialMap(width, height, _chance);
            }
            float[,] newMap = new float[width, height];
            float numberOfNeighbours;
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

        private static float CountLivingNeighbours(float[,] oldMap, int width, int height, int posX, int posY)
        {
            float neighb = 0;
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

        private static float[,] PopulateInitialMap(int width, int height, int chance)
        {
            float[,] newMap = new float[width, height];
            return SpawnRandomCells(newMap, width, height, chance);
        }

        private static float[,] SpawnRandomCells(float[,] map, int width, int height, int chance)
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