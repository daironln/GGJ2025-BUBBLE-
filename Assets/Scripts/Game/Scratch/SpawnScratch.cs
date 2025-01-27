using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScratch : MonoBehaviour
{
    public GameObject[] trashPrefab;
    public float spawnInterval = 5f;
    public Vector2 mapSize = new Vector2(10f, 10f);

    private void Start()
    {
        StartCoroutine(SpawnTrashRoutine());
    }

    private IEnumerator SpawnTrashRoutine()
    {
        while (true)
        {
            SpawnTrash();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnTrash()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-mapSize.x / 2, mapSize.x / 2),
            0f,
            0f
        );

        Instantiate(trashPrefab[Random.Range(0, trashPrefab.Length)], randomPosition, Quaternion.identity);
    }
}
