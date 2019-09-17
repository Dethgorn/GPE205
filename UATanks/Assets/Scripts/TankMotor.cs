using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{
    private CharacterController charactercontroller;
    private Transform tf;
    private float moveX;

    // Start is called before the first frame update
    void Start()
    {
        charactercontroller = GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float speed)
    {
        // declare and initialize
        Vector3 moveVector = tf.forward * speed * Time.deltaTime;

        // simple move
        charactercontroller.SimpleMove(moveVector);
    }

    public void Rotate(float speed)
    {
        moveX = Input.GetAxis("Horizontal");
        
        if (moveX > 0.0f)
        {
            // set it up to spin right
             Vector3 rotateVector = tf.up * speed * Time.deltaTime;
            // turn the tank
            tf.Rotate(rotateVector, Space.Self);
        }
        else if (moveX < 0.0f)
        {
            // set it up to spin left
            Vector3 rotateVector = tf.up * speed * Time.deltaTime;
            // turn the tank
            tf.Rotate(-rotateVector, Space.Self);
        }
    }
}
