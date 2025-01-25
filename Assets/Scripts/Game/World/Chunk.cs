using System.Collections.Generic;
using UnityEngine;


public class Chunk : MonoBehaviour
{
    public void Initialize(Vector2Int coord, Vector2 chunkSize, GameObject[] frontPrefabs, GameObject[] middlePrefabs, GameObject[] backPrefabs, float frontZ, float middleZ, float backZ)
    {
        GenerateObjects(frontPrefabs, chunkSize, frontZ);

        GenerateObjects(middlePrefabs, chunkSize, middleZ);

        GenerateObjects(backPrefabs, chunkSize, backZ);
    }

    private void GenerateObjects(GameObject[] prefabs, Vector2 chunkSize, float zPosition)
    {
        int objectCount = Random.Range(5, 15); 

        for (int i = 0; i < objectCount; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            Vector3 position = new Vector3(
                Random.Range(0, chunkSize.x),
                Random.Range(0, -2),//chunkSize.y
                zPosition
            );

            Instantiate(prefab, transform.position + position, Quaternion.identity, transform);
        }
    }
}