using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsAvoidAIController : MonoBehaviour
{
    public float avoidanceTime = 1.0f;
    public enum AttackMode { Chase };
    public AttackMode attackMode;
    public Transform target;

    private TankMotor motor;
    private TankData data;
    private Transform tf;
    private int avoidanceStage = 0;
    private float exitTime;



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
        // get a target
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (attackMode == AttackMode.Chase)
        {
            if (avoidanceStage != 0)
            {
                DoAvoidance();
                
            }
            else
            {
                DoChase();
            }
        }
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

    void DoChase()
    {
        // turn to target then chase
        motor.RotateTowards(target.position, data.rotateSpeed);
        if (CanMove(2.0f))
        {
            motor.Move(1.0f);
        }
        else
        {
            avoidanceStage = 1;
            
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
