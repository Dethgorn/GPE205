using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorAIController : MonoBehaviour
{
    public enum AISense { Patrol, Listen, Chase};
    public AISense aiSense;
    public float aiSenseRadius;
    public float avoidanceTime = 1.0f;
    public float closeEnough = 1.0f;
    public Transform target;
    public Transform[] waypoints;
    private Transform targCheck;
    private Transform targCheck2;

    private int avoidanceStage = 0;
    private int currentWaypoint = 0;
    private float exitTime;
    private TankMotor motor;
    private TankData data;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.multiplayer)
        {
            // get a target
            if (target == null)
            {
                targCheck = GameObject.FindGameObjectWithTag("Player").transform;
                targCheck2 = GameObject.FindGameObjectWithTag("Player2").transform;
                if (Vector3.Distance(targCheck.position, tf.position) < Vector3.Distance(targCheck2.position, tf.position))
                {
                    target = GameObject.FindGameObjectWithTag("Player").transform;
                }
                else
                {
                    target = GameObject.FindGameObjectWithTag("Player2").transform;
                }
            }
        }
        else
        {
            // get a target
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
        if (aiSense == AISense.Patrol)
        {
            if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                if (motor.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed))
                {
                    // Do nothing
                }
                else
                {
                    // can we move?
                    if (CanMove(2.0f))
                    {
                        motor.Move(1.0f);
                    }
                    else
                    {
                        avoidanceStage = 1;

                    }
                }

                // check distance to waypoint
                if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) <= (closeEnough * closeEnough))
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
            // check for changes
            if (Vector3.Distance(target.position, tf.position) <= aiSenseRadius)
            {
                ChangeState(AISense.Listen);
                
            }
        }
        else if (aiSense == AISense.Listen)
        {
            // spin and look
            motor.Rotate(1.0f);

            // check for change
            if (CanSee())
            {
                ChangeState(AISense.Chase);
            }
            else if (Vector3.Distance(target.position, tf.position) > aiSenseRadius)
            {
                ChangeState(AISense.Patrol);
            }
        }
        else if (aiSense == AISense.Chase)
        {
            motor.RotateTowards(target.position, data.rotateSpeed);
            if (CanMove(2.0f))
            {
                motor.Move(1.0f);
            }
            else
            {
                avoidanceStage = 1;

            }
            gameObject.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
            // check for change
            if (!CanSee())
            {
                ChangeState(AISense.Listen);
            }
        }
        
    }
    public void ChangeState(AISense newMode)
    {
        // Change our state
        aiSense = newMode;
    }

    bool CanSee()
    {
        RaycastHit hit;
        // check for obstacles using player tag
        if (Physics.Raycast(tf.position, tf.forward, out hit))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }
        return true;
    }
    void DoAvoidance()
    {
        switch (avoidanceStage)
        {
            case 1:
                // turn left
                motor.Rotate(-1.0f);

                if (CanMove(2.0f))
                {
                    avoidanceStage = 2;
                    // set timer to go forward
                    exitTime = avoidanceTime;
                }
                break;
            case 2:
                // if we can move forward, do so
                if (CanMove(2.0f))
                {
                    // Subtract from our timer and move
                    exitTime -= Time.deltaTime;
                    motor.Move(1.0f);

                    // If we have moved long enough, return to chase mode
                    if (exitTime <= 0)
                    {
                        avoidanceStage = 0;
                    }
                }
                else
                {
                    avoidanceStage = 1;
                }
                break;
        }
    }
    bool CanMove(float speed)
    {
        RaycastHit hit;
        // check for obstacles using player tag
        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }
        return true;
    }

}
