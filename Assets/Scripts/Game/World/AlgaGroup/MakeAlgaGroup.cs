using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeAlgaGroup : MonoBehaviour
{
    [SerializeField] private GameObject[] algaPrefabs;
    [SerializeField] private float radius;
    [SerializeField] private float alt;
    //Esta funcion se encarga de generar un grupo de algas en una posicion aleatoria
    public void GenerateAlgaGroup()
    {
        int algaCount = Random.Range(5, 15);

        for (int i = 0; i < algaCount; i++)
        {
            GameObject alga = algaPrefabs[Random.Range(0, algaPrefabs.Length)];
           
            Vector3 position = new Vector3(
                Random.Range(transform.position.x - radius, transform.position.x + radius),
                0,
                Random.Range(transform.position.z - radius, transform.position.z + radius)
            );

            float scale = Random.Range(0.5f, 1.5f);
            alga.transform.localScale = new Vector3(scale, scale, scale);
           
            Instantiate(alga, transform.position + position, Quaternion.identity, transform);
        }
    }
    void Start()
    {
        GenerateAlgaGroup();
    }

    void Update()
    {
        
    }
}
