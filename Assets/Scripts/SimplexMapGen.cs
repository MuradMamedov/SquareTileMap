using Assets.Scripts.Interfaces;
using System.Collections;

namespace Assets.Scripts
{
    public class SimplexMapGen : IMapGen
    {
        private System.Random _r = new System.Random(7789);
        public float[,] Generate(float[,] oldMap, int width, int height)
        {
            float[,] map = new float[width, height];
            for (int x = 0; x < height; x++)
                for (int y = 0; y < height; y++)
                {
                    float value = OpenSimplex2S.Noise2(_r.Next(), x, y);
                    map[x, y] = value;
                }
            return map;
        }
    }
}