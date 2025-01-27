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
    private float speedRedirect;

    private Animator _anim;


    [SerializeField] private float timer = 4.5f;

    public void Initialize(float speed, float amplitude, float frequency, float speedRedirect)
    {
        this.speed = speed;
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.speedRedirect = speedRedirect;

       
        FindClosestEnemy();
    }

    void Start()
    {
        startPosition = transform.position;

        _anim = GetComponent<Animator>();

        timer = Random.Range(3.5f, 5.6f);
    }

    void Update()
    {

        if(timer > 0)
        {

            timer -= Time.deltaTime;
        }

        if(timer <= 0 && !isEnemyCaptured)
        {
            Destroy(gameObject);

            Debug.Log("Bubble Destroyed");

            timer = Random.Range(3.5f, 5.6f);
        }

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
            transform.position += (Vector3.up + new Vector3(0, offset, 0)) * speed * Time.deltaTime;

            if (target != null)
            {
               
                Vector3 direction = (target.position - transform.position).normalized;
                float offsetFloat = Mathf.Sin(Time.time * frequency) * amplitude;

                Vector3 floatDirection = speedRedirect * direction + new Vector3(0, offsetFloat, 0);
                floatDirection.x = Mathf.Sign(direction.x) * Mathf.Abs(floatDirection.x);

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

            Debug.Log("Captured");

            _anim.SetTrigger("captured");

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
        else if(collision.CompareTag("Obstacle") && !isEnemyCaptured)
        {
            Debug.Log("Bubble Chock");
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Killer") && !isEnemyCaptured)
        {
            Debug.Log("Bubble Chock");
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Killer") && isEnemyCaptured)
        {
            Debug.Log("Bubble And Enemy Destryed");

            Score.Instance.AddScore();




            Destroy(gameObject);
            Destroy(capturedEnemy.gameObject);
        }
    }
}