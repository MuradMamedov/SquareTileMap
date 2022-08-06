﻿namespace Assets.Scripts.Interfaces
{
    public interface ICellsGenerator
    {
        int[,] Generate(int[,] oldMap, int width, int height);
        int[,] PopulateInitialMap(int width, int height, int chance);
    }
}