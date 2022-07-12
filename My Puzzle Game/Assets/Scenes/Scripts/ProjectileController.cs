using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float speed = 2;
    private float leftBound = 10;
    private float rightBound = -15;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.rotation = GameObject.Find("Enemy Spitter").transform.rotation;

        if (transform.position.x > leftBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < rightBound)
        {
            Destroy(gameObject);
        }
    }
    
}
