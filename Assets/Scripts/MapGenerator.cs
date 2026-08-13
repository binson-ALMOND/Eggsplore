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

    [Header("Proceduralne Obiekty")] 
    public GameObject treePrefab;
    public GameObject eggPrefab;

    [Range(0, 100)] public float treeChance = 3f;
    [Range(0, 100)] public float eggChance = 0.08f;
    
    private float seedX;
    private float seedY;
    private Vector3Int lastPlayerTilePos;
    void Awake()
    {
        seedX = Random.Range(0f,99999f);
        seedY = Random.Range(0f,99999f);
        GenerateTilesAroundPlayer();

        if (player != null && !player.CompareTag("MainCamera"))
        {
            SetPlayerToSafePosition();
            
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

    private void SetPlayerToSafePosition()
    {
        Vector3Int centerCell = Vector3Int.zero;

        for (int x = centerCell.x - viewDistance; x <= centerCell.x + viewDistance; x++)
        {
            for (int y = centerCell.y - viewDistance; y <= centerCell.y + viewDistance; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                if (tilemap.GetTile(tilePos) == grassTile)
                {
                    Vector3 worldPos = tilemap.GetCellCenterWorld(tilePos);
                    
                    Collider2D hit = Physics2D.OverlapCircle(worldPos, 0.2f);
                    if (hit == null || hit.transform == player)
                    {
                        player.position = worldPos;
                        lastPlayerTilePos = tilemap.WorldToCell(player.position);


                        if (Camera.main != null)
                        {
                            Camera.main.transform.position = new Vector3(worldPos.x, worldPos.y, Camera.main.transform.position.z);
                        }
                        
                        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null) spriteRenderer.enabled = true;
                        return;
                    }
                }
            }
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

                if (selectedTile == grassTile)
                {
                    float rand = Random.Range(0f, 100f);
                    Vector3 spawnPos = tilemap.GetCellCenterWorld(tilePos);

                    if (rand < eggChance)
                    {
                        Instantiate(eggPrefab, spawnPos, Quaternion.identity, transform);
                        
                    } else if (rand < eggChance + treeChance)
                    {
                        Instantiate(treePrefab, spawnPos, Quaternion.identity, transform);
                    }
                }
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
