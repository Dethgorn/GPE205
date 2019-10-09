using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseFleeAIController : MonoBehaviour
{
    public enum AttackMode { Chase, Flee };
    public AttackMode attackMode;
    public float fleeDistance = 1.0f;
    public Transform target;
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
            // Rotate towards the target
            motor.RotateTowards(target.position, data.rotateSpeed);
            // Move forward
            motor.Move(1f);
        }
        if (attackMode == AttackMode.Flee)
        {
            // get target location relative to our own location
            Vector3 vectorToTarget = target.position - tf.position;

            // calculate the vector pointing away from the target
            // normalize it keep it frame by frame then add fleeDist var
            Vector3 vectorAwayFromTarget = -1 * vectorToTarget;
            vectorAwayFromTarget.Normalize();
            vectorAwayFromTarget *= fleeDistance;

            // now turn and flee
            Vector3 fleePosition = vectorAwayFromTarget + tf.position;
            motor.RotateTowards(fleePosition, data.rotateSpeed);
            motor.Move(1f);
        }
    }
}
