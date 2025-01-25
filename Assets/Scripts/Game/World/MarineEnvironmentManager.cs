using System.Collections.Generic;
using UnityEngine;

public class MarineEnvironmentManager : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public Vector2 chunkSize = new Vector2(20f, 10f); 

  
    public GameObject[] frontLayerPrefabs;
    public GameObject[] middleLayerPrefabs;
    public GameObject[] backLayerPrefabs;

   
    public float frontLayerZ = -1f;
    public float middleLayerZ = 0f;
    public float backLayerZ = 1f;

    private Dictionary<Vector2Int, Chunk> loadedChunks = new Dictionary<Vector2Int, Chunk>();
    private Vector2Int currentChunk;

    private void Start()
    {
        currentChunk = GetChunkCoord(player.position);
        LoadChunksAround(currentChunk);
    }

    private void Update()
    {
        Vector2Int playerChunk = GetChunkCoord(player.position);

        if (playerChunk != currentChunk)
        {
            currentChunk = playerChunk;
            LoadChunksAround(currentChunk);
        }
    }

    private Vector2Int GetChunkCoord(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / chunkSize.x),
            Mathf.FloorToInt(position.y / chunkSize.y)
        );
    }

    private void LoadChunksAround(Vector2Int centerChunk)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int chunkCoord = new Vector2Int(centerChunk.x + x, centerChunk.y + y);
                if (!loadedChunks.ContainsKey(chunkCoord))
                {
                    CreateChunk(chunkCoord);
                }
            }
        }

        List<Vector2Int> chunksToRemove = new List<Vector2Int>();

        foreach (var chunk in loadedChunks)
        {
            if (Vector2Int.Distance(chunk.Key, centerChunk) > 1.5f)
            {
                Destroy(chunk.Value.gameObject);
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var chunkCoord in chunksToRemove)
        {
            loadedChunks.Remove(chunkCoord);
        }
    }

    private void CreateChunk(Vector2Int chunkCoord)
    {
        GameObject chunkObject = new GameObject($"Chunk_{chunkCoord.x}_{chunkCoord.y}");
        chunkObject.transform.position = new Vector3(chunkCoord.x * chunkSize.x, chunkCoord.y * chunkSize.y, 0f);

        Chunk chunk = chunkObject.AddComponent<Chunk>();
        chunk.Initialize(chunkCoord, chunkSize, frontLayerPrefabs, middleLayerPrefabs, backLayerPrefabs, frontLayerZ, middleLayerZ, backLayerZ);

        loadedChunks.Add(chunkCoord, chunk);
    }
}