using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strategyCamera : MonoBehaviour
{

    private int ScrollWidth { get { return 15; } }
    private float ScrollSpeed { get { return 25; } }
    private float RotateAmount { get { return 10; } }
    private float RotateSpeed { get { return 100; } }
    private float MinCameraHeight { get { return 10; } }
    private float MaxCameraHeight { get { return 40; } }


    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
            MoveCamera();
            RotateCamera();
    }

    private void MoveCamera()
    {

        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        //horizontal camera movement
        if (xpos >= 0 && xpos < ScrollWidth)
        {
            movement.x -= ScrollSpeed;
        }
        else if (xpos <= Screen.width && xpos > Screen.width - ScrollWidth)
        {
            movement.x += ScrollSpeed;
        }

        //vertical camera movement
        if (ypos >= 0 && ypos < ScrollWidth)
        {
            movement.z -= ScrollSpeed;
        }
        else if (ypos <= Screen.height && ypos > Screen.height - ScrollWidth)
        {
            movement.z += ScrollSpeed;
        }

        //make sure movement is in the direction the camera is pointing
        //but ignore the vertical tilt of the camera to get sensible scrolling
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        //away from ground movement
        movement.y -= ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        //calculate desired camera position based on received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit away from ground movement to be between a minimum and maximum distance
        if (destination.y > MaxCameraHeight)
        {
            destination.y = MaxCameraHeight;
        }
        else if (destination.y < MinCameraHeight)
        {
            destination.y = MinCameraHeight;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ScrollSpeed);
        }
    }

    private void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        //detect rotation amount if ALT is being held and the Right mouse button is down
        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1))
        {
            destination.x -= Input.GetAxis("Mouse Y") * RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * RotateAmount;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * RotateSpeed);
        }
    }
}
