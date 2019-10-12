using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseFleeAIController : MonoBehaviour
{


    public enum AttackMode { Chase, ChaseAndFire, Flee, Rest };
    public AttackMode attackMode;
    public float aiSenseRadius;
    public float avoidanceTime = 1.0f;
    public float fleeDistance = 1.0f;
    public float restingHealRate;
    public float stateEnterTime;
    public Transform target;

    private int avoidanceStage = 0;
    private float exitTime;
    private TankData data;
    private TankMotor motor;
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
        if (attackMode == AttackMode.Chase)
        {
            DoChase();
            // check for changes
            if (data.health < (data.maxHealth * .5f))
            {
                ChangeState(AttackMode.Flee);
                stateEnterTime = Time.time;
            }
            else if (Vector3.Distance(target.position, tf.position) <= aiSenseRadius)
            {
                ChangeState(AttackMode.ChaseAndFire);
            }
        }
        else if (attackMode == AttackMode.Flee)
        {
            if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                DoFlee();
            }
            // check for changes
            if (Time.time >= stateEnterTime + 10)
            {
                ChangeState(AttackMode.Rest);
            }

        }
        else if (attackMode == AttackMode.ChaseAndFire)
        {
            DoChaseFire();
            //check for changes
            if (data.health < (data.maxHealth * .5f))
            {
                ChangeState(AttackMode.Flee);
            }
        }
        else if (attackMode == AttackMode.Rest)
        {
            DoRest();
            // check for changes
            if (data.health >= data.maxHealth)
            {
                ChangeState(AttackMode.Chase);
            }
            else if (Vector3.Distance(target.position, tf.position) <= aiSenseRadius)
            {
                ChangeState(AttackMode.Flee);
            }
        }
    }
    
    void DoChase()
    {
        // Rotate towards the target
        motor.RotateTowards(target.position, data.rotateSpeed);
        // Move forward
        motor.Move(1f);
    }

    void DoChaseFire()
    {
        // Rotate towards the target
        motor.RotateTowards(target.position, data.rotateSpeed);
        // Move forward
        motor.Move(1f);
        gameObject.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
    }

    void DoFlee()
    {
        // get target location relative to our own location
        Vector3 vectorToTarget = target.position - tf.position;

        // calculate the vector pointing away from the target
        // normalize it keep it frame by frame then add fleeDist var
        Vector3 vectorAwayFromTarget = -1 * vectorToTarget;
        vectorAwayFromTarget.Normalize();
        vectorAwayFromTarget *= fleeDistance;

        // now turn
        Vector3 fleePosition = vectorAwayFromTarget + tf.position;
        motor.RotateTowards(fleePosition, data.rotateSpeed);
        // make sure the path is clear, then run or avoid
        if (CanMove(2.0f))
        {
            motor.Move(1.5f);
        }
        else
        {
            avoidanceStage = 1;

        }
    }

    public void ChangeState(AttackMode newMode)
    {

        // Change our state
        attackMode = newMode;

        // save the time we changed states
        stateEnterTime = Time.time;
    }
    public void DoRest()
    {
        // Increase our health. Remember that our increase is "per second"!
        data.health += restingHealRate * Time.deltaTime;

        // But never go over our max health
        data.health = Mathf.Min(data.health, data.maxHealth);
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
