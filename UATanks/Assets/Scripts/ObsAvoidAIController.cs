using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsAvoidAIController : MonoBehaviour
{
    public float avoidanceTime = 2.0f;
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
        if (avoidanceStage == 1)
        {
            motor.Rotate(-1 * data.rotateSpeed);

            if (CanMove(data.forwardSpeed))
            {
                avoidanceStage = 2;

                exitTime = avoidanceTime;
            }
            else if (avoidanceStage == 2)
            {
                // if we can move forward, do so
                if (CanMove(data.forwardSpeed))
                {
                    // Subtract from our timer and move
                    exitTime -= Time.deltaTime;
                    motor.Move(data.forwardSpeed);

                    // If we have moved long enough, return to chase mode
                    if (exitTime <= 0)
                    {
                        avoidanceStage = 0;
                    }
                }
                else
                {
                    // Otherwise, we can't move forward, so back to stage 1
                    avoidanceStage = 1;
                }
            }
        }
    }

    void DoChase()
    {
        motor.RotateTowards(target.position, data.rotateSpeed);
        if (CanMove(data.forwardSpeed))
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
