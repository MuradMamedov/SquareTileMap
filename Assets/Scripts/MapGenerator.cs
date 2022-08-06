using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Diagnostics;

public class MapGenerator : MonoBehaviour
{
    public Tilemap TopMap;
    
    public Tilemap BotMap;
    
    public RuleTile TopTile;
    
    public RuleTile BotTile;
    
    public Vector3Int TmpSize;

    [Range(0, 100)]
    public int InitChance;

    [Range(1, 8)]
    public int BirthCondition;
    
    [Range(1, 8)]
    public int DeathCondition;

    [Range(1, 10)]
    public int InterationsCount;


    private int _count = 0;
    private int[,] _grid;
    private ICellsGenerator _cellsGenerator;
    private IAssetsService _assetsService;

    void Start()
    {
        _cellsGenerator = new CellsGenerator(BirthCondition, DeathCondition);
        _assetsService = new AssetsServices();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Tick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Clear(true);
        }

        if (Input.GetMouseButton(2))
        {
            string fileName = "tmapXY_" + _count;
            var gridObject = GameObject.Find("Grid");
            _assetsService.SaveAssetMap(gridObject, fileName);
            _count++;
        }
    }

    public void Tick()
    {
        var timer = new Stopwatch();

        Clear(false);
        int width = TmpSize.x;
        int height = TmpSize.y;

        if (_grid == null)
        {
            _grid = _cellsGenerator.PopulateInitialMap(width, height, InitChance);
        }

        timer.Start();
        for (int i = 0; i < InterationsCount; i++)
        {
            _grid = _cellsGenerator.Generate(_grid, width, height);
        }
        timer.Stop();
        UnityEngine.Debug.Log($"Elapsed time calc: {timer.ElapsedMilliseconds}");

        timer.Start();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_grid[x, y] == 1)
                    TopMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), TopTile);
                BotMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), BotTile);
            }
        }
        timer.Stop();
        UnityEngine.Debug.Log($"Elapsed time draw: {timer.ElapsedMilliseconds}");
    }

    public void Clear(bool complete)
    {
        TopMap.ClearAllTiles();
        BotMap.ClearAllTiles();
        if (complete)
        {
            _grid = null;
        }
    }
}
