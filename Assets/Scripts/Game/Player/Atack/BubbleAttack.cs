using UnityEngine;

public class BubbleAttack : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform bubbleSpawnPoint;
    public float bubbleSpeed = 5f;
    public float floatAmplitude = 0.5f; 
    public float floatFrequency = 2f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            LaunchBubble();
        }
    }

    void LaunchBubble()
    {
        GameObject bubble = Instantiate(bubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);
        BubbleMovement bubbleMovement = bubble.GetComponent<BubbleMovement>();

        if (bubbleMovement != null)
        {
            bubbleMovement.Initialize(bubbleSpeed, floatAmplitude, floatFrequency);
        }   
    }
}