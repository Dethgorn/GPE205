using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private TankMotor motor;
    public TankData data;


    // Start is called before the first frame update
    void Start()
    {
        motor = gameObject.GetComponent<TankMotor>();

        // failsafe if forget to attach data
        if (data == null)
        {
            data = gameObject.GetComponent<TankData>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        motor.Move(1.0f);
        motor.Rotate(1.0f);
        gameObject.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
    }
}
