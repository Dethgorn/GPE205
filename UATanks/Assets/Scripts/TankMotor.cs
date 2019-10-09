using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]

public class TankMotor : MonoBehaviour
{
    private TankData data;
    private CharacterController charactercontroller;
    private Transform tf;
    private float rotateX;
    private float moveX;

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
        charactercontroller = GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float speed)
    {
        // check the axis for the value
        if (speed < 0)
        {
            speed = speed * data.reverseSpeed;
        }
        else
        {
            speed = speed * data.forwardSpeed;
        }

        // math the vector
        Vector3 moveVector = tf.forward * speed * Time.deltaTime;

        // simple move
        charactercontroller.SimpleMove(moveVector);

    }

    public void Rotate(float speed)
    {
        // set up a vector
        Vector3 rotateVector = tf.up * speed * Time.deltaTime * data.rotateSpeed;
        // turn the tank
        tf.Rotate(rotateVector, Space.Self);
    }

    public bool RotateTowards(Vector3 target, float speed)
    {
        // find the target from current location
        Vector3 vectorToTarget = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);

        // rotate if needed
        if (targetRotation == transform.rotation)
        {
            return false;
        }
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, targetRotation, data.rotateSpeed * Time.deltaTime);
        return true;
    }
}
