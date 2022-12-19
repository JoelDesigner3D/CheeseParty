using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public MenuController menuController;
    public AudioSource crunch;
    public AudioSource soundTrap;


    private Rigidbody rigidBody;
    private int count;
    private float movementX;
    private float movementY;


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Starting");

        rigidBody = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rigidBody.AddForce(movement * speed);

    }

    void OnMove(InputValue movementValue)
    {
       Vector2 movementVector = movementValue.Get<Vector2>();

       movementX = movementVector.x;
       movementY = movementVector.y;

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTrigger");

        if (other.gameObject.CompareTag("Cheese"))
        {
            Debug.Log("OnTrigger Cheese");
            other.gameObject.SetActive(false);
            count += 1;
   
            crunch.Play();
            
           
        } else if (other.gameObject.CompareTag("Hamburger"))
        {
            Debug.Log("OnTrigger Hamburger");
            other.gameObject.SetActive(false);
            count += 5;
            crunch.Play();

        } else if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("OnTrigger Finish");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        SetCountText();

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            count -= 1;
            SetCountText();
            soundTrap.Play();

        } else if(collision.gameObject.CompareTag("Enemy")){

            EndGame();

        }

    }
    

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            // winTextObject.SetActive(true);
            //menuController.WinGame();
        }
    }

    void EndGame()
    {
       
        menuController.LoseGame();
       // gameObject.SetActive(false);

    }
}
