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
    public AudioSource soundFlower;
    public AudioSource soundRespown;
    public Transform respownPoint;

    public float turnSpeed = 120.0f;
    private float jumpSpeed = 20.0f;
    private float horizontalInput;
    private float forwardInput;
    private float jumpInput;

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

    private void FixedUpdate()
    {
        // Keys management
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");

        // Translate the vehicle 
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // Rotate the vehicle
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);
        // Jump the vehicle
        transform.Translate(Vector3.up * Time.deltaTime * jumpInput * jumpSpeed);
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
            count += 2;
            crunch.Play();

        } else if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("OnTrigger Finish");
            //count += 3;
            soundFlower.Play();
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        SetCountText();

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            SetCountText();
            soundTrap.Play();

        } else if(collision.gameObject.CompareTag("Enemy")){

            rebootLevel();

        }

    }
    

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 15)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    void rebootLevel()
    {
        soundRespown.Play();
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.Sleep();
        transform.position = respownPoint.position;

    }
}
