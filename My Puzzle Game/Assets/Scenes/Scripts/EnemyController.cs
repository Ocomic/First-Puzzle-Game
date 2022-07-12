using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //Documenattion

    public float speed;
    public Transform[] moveSpots;
    public float waitTime;
    private int randomSpot;
    public Transform target;
    Rigidbody rigBody;
    public GameObject projectilePrefab;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    // field for Crawler Nav Mesh Agent to move on walls
    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Start Patrol Path Enemy movement with coroutine
        if (gameObject.CompareTag("Patrol"))
        {
            StartCoroutine(Move());
        }

        rigBody = GetComponent<Rigidbody>();

        // Invoke Projectiles projectile
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);

    }

    // Update is called once per frame
    void Update()
    {
        // Follow just moves forward like the Player
        if (gameObject.CompareTag("Follow"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        }
        // Crawler moves on Walls and Ceiling
        if (gameObject.CompareTag("Crawler"))
        {
            navMeshAgent.destination = movePositionTransform.position;

        }
        // Hunter moves in player direction
        if (gameObject.CompareTag("Hunter"))
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            rigBody.MovePosition(pos);
            transform.LookAt(target);
        }

        //Spitter shoots projectiles
        if (gameObject.CompareTag("Spitter"))
        {
            
        }


    }

    public void SpawnObject()
    {
        //Spitter shoots projectiles
        if (gameObject.CompareTag("Spitter"))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            if(stopSpawning)
            {
                CancelInvoke("SpawnObject");
            }
        }
    }

    //detect collisions
    private void OnCollisionEnter(Collision collision)
    {
        // turn around when hitting an object tagged "Wall"
        if (collision.gameObject.CompareTag("Wall"))

        {
            Vector3 rotationToAdd = new Vector3(0, 180, 0);
            transform.Rotate(rotationToAdd);
        }
        
    }

    //Patrol Path Methode - move Enemy between an array of patrol Paths
    IEnumerator Move()
    {
        while (true)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
            while (Vector3.Distance(transform.position, moveSpots[randomSpot].position) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}





