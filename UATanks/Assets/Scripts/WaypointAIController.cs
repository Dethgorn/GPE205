using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAIController : MonoBehaviour
{
    private TankMotor motor;
    public TankData data;

    public Transform[] waypoints;
    public float closeEnough = 1.0f;

    private int currentWaypoint = 0;


    // Start is called before the first frame update
    void Start()
    {
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);

        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed))
        {
            // Do nothing
        }
        else
        {
            motor.Move(1.0f);
        }
        // it would be better to store transform in a var in Start!
        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - transform.position) <= (closeEnough * closeEnough))
        {
            // dont go outside the array
            if (currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
            }
            else
            {
                currentWaypoint = 0;
            }

        }
    }
}
