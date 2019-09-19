using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public enum InputScheme { arrowKey, WASD };
    public InputScheme input = InputScheme.WASD;

    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gather input from the correct place
        switch (input)
        {
            case InputScheme.arrowKey:
                horizontalInput = Input.GetAxis("HorizontalArrows");
                verticalInput = Input.GetAxis("VerticalArrows");
                break;
            case InputScheme.WASD:
                horizontalInput = Input.GetAxis("HorizontalWASD");
                verticalInput = Input.GetAxis("VerticalWASD");
                break;
        }

        // send the data to the methods
        gameObject.SendMessage("Move", verticalInput, SendMessageOptions.DontRequireReceiver);
        gameObject.SendMessage("Rotate", horizontalInput, SendMessageOptions.DontRequireReceiver);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            gameObject.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
        }
    }
}

