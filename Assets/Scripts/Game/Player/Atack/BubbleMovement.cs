using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    private float speed;
    private float amplitude;
    private float frequency;
    private Vector3 startPosition;
    private Transform target;
    private bool isEnemyCaptured = false; 
    private Transform capturedEnemy;
    private float floatHeight = 5f; 

    public void Initialize(float speed, float amplitude, float frequency)
    {
        this.speed = speed;
        this.amplitude = amplitude;
        this.frequency = frequency;

       
        FindClosestEnemy();
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isEnemyCaptured)
        {
           
            if (capturedEnemy != null)
            {
              
                Vector3 upwardMovement = Vector3.up * speed * Time.deltaTime;
                transform.position += upwardMovement;
                capturedEnemy.position += upwardMovement;
            }
        }
        else
        {
           
            float offset = Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position += (Vector3.right + new Vector3(0, offset, 0)) * speed * Time.deltaTime;

            if (target != null)
            {
               
                Vector3 direction = (target.position - transform.position).normalized;
                float offsetFloat = Mathf.Sin(Time.time * frequency) * amplitude;
                Vector3 floatDirection = direction + new Vector3(0, offsetFloat, 0);
                transform.position += floatDirection * speed * Time.deltaTime;
            }
        }
    }

    private void FindClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isEnemyCaptured)
        {
          
            capturedEnemy = collision.transform;
            isEnemyCaptured = true;

            Rigidbody2D enemyRigidbody = capturedEnemy.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                enemyRigidbody.isKinematic = true;
            }

            Collider2D enemyCollider = capturedEnemy.GetComponent<Collider2D>();
            if (enemyCollider != null)
            {
                enemyCollider.enabled = false;
            }

            target = null;
        }
    }
}