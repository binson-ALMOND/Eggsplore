using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [Header("Komponenty Unity")] 
    public Tilemap tilemap;

    public Transform player;
    
    [Header("Paleta kafelków")]
    public TileBase waterTile;
    public TileBase sandTile;
    public TileBase grassTile;
    public TileBase mountainTile;

    [Header("Ustawienia świata")] 
    [Tooltip("Zasięg generowania kafelków wokół ptaka")]
    public int viewDistance = 35;
    public float scale = 20f;
    

    private float seedX;
    private float seedY;
    private Vector3Int lastPlayerTilePos;
    void Start()
    {
        seedX = Random.Range(0f,99999f);
        seedY = Random.Range(0f,99999f);

        if (player != null)
        {
            player.position = Vector3.zero;
        }

        GenerateTilesAroundPlayer();

    }

    void Update()
    {
        if (player == null) return;
        
        Vector3Int currentPlayerTilePos = tilemap.WorldToCell(player.position);

        if (currentPlayerTilePos != lastPlayerTilePos)
        {
            lastPlayerTilePos = currentPlayerTilePos;
            GenerateTilesAroundPlayer();
        }
    }
    public void GenerateTilesAroundPlayer()
    {
        Vector3Int playerCell = tilemap.WorldToCell(player.position);
        
        
        for(int x = playerCell.x - viewDistance; x <= playerCell.x + viewDistance; x++)
        {
            for (int y = playerCell.y - viewDistance; y <= playerCell.y+viewDistance; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                if (tilemap.HasTile(tilePos)) continue;
                
                float xCoord = (float)x / scale + seedX;
                float yCoord = (float)y / scale + seedY;
                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);
                
                TileBase selectedTile = AssignTile(noiseValue);
                tilemap.SetTile(new Vector3Int(x,y,0), selectedTile);
            }            
        }
    }

    private TileBase AssignTile(float value)
    {
        if(value<0.3f) return waterTile;
        if(value<0.38f) return sandTile;
        if(value<0.7f) return grassTile;
        return mountainTile;
    }

    
}
