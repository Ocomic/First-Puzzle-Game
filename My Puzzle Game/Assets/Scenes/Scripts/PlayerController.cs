using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Documenattion
    private float speed = 1.5f;
    private Rigidbody playerRb;
    public bool clicked = false;
    public bool released = false;
    public bool playerClick = false;
    public bool gameOver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        
        //Get left mousebutton down
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");
            clicked = true;
            
        }

        //Get left mousebutton up
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Release primary button.");
            released = true;
            clicked = false;
                        
        }

        //when mousebutton is released check if position is on player   
        if (released == true)
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    CurrentClickedGameObject(raycastHit.transform.gameObject);
                }
                
            }
            
        }
                      

    }
    //New methode clicked game object is player then let player run
    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.tag == "Player" && clicked)
        {
            playerClick = true;
        }
        
        if (playerClick == true)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
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
        //Game Over collisions with enemies
        else if (collision.gameObject.CompareTag("Enemy")
                 ^ collision.gameObject.CompareTag("Hunter")
                 ^ collision.gameObject.CompareTag("Crawler")
                 ^ collision.gameObject.CompareTag("Follow")
                 ^ collision.gameObject.CompareTag("Patrol"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            
        }
    }
}
