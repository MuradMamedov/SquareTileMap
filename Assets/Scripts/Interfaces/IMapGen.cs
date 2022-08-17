namespace Assets.Scripts.Interfaces
{
    public interface IMapGen
    {
        float[,] Generate(float[,] oldMap, int width, int height);
    }
}