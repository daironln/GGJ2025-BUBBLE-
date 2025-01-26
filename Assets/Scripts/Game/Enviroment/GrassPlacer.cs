using UnityEngine;

public class GrassPlacer : MonoBehaviour
{
    public GameObject grassPrefab; 
    public Terrain terrain;
    public int numberOfGrass = 100; 

    void Start()
    {
        PlaceGrass();
    }

    void PlaceGrass()
    {
        if (grassPrefab == null || terrain == null)
        {
            Debug.LogError("Asigna el prefab de hierba y el terreno.");
            return;
        }

        TerrainData terrainData = terrain.terrainData;

        for (int i = 0; i < numberOfGrass; i++)
        {
           
            float xPosition = Random.Range(0, terrainData.size.x);
            float zPosition = Random.Range(0, terrainData.size.z);
            float yPosition = terrain.SampleHeight(new Vector3(xPosition, 0, zPosition));

            Vector3 position = new Vector3(xPosition, yPosition, zPosition);

          
            Instantiate(grassPrefab, position, Quaternion.identity, this.transform);
        }
    }
}
