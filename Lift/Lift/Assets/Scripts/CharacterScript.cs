using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;

    [SerializeField]
    private float speed = 6.0f;                                //var to set the speed of the player movement
    [SerializeField]
    private float rotationSpeed = 1;                           //var to set the rotation speed of the player
    [SerializeField]
    private float rayDistance = 100f;                          //var to set the distance of the raycast
    [SerializeField]
    private float liftCallButtonAnimOffset = 0.5f;            //offset value for lift call button animation
    [SerializeField]
    private float liftFloorOffsetButton = 0.015f;              //offset value for lift navigation button animation
    private float rotX = 0;                                    //var to hold the mouse x value
    private float rotY = 0;                                    //var to hold the mouse y value
     
    private Rigidbody rigBody;                                 //reference of the rigidbody

    private Vector3 mousePos = Vector3.zero;        //var to store the mouse pointer value
    private Vector3 objPos;                         //var to store the mouse pointer value in relation to the world co-ordinates

    private RaycastHit hit;
    private Ray ray;

    [SerializeField]
    private Camera cameraObject;         //reference to the camera object    

    [SerializeField]
    private LiftManager L_Manager;       //reference to the liftmanager script
    [SerializeField]
    private Lift_GameManager gamemanager;            //reference to the gamemanager script

    // Start is called before the first frame update
    void Start()
    {
        rigBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        ShootRaycast();
    }

    /// <summary>
    /// This method is reponsible for the player's movement using the keyboard buttons
    /// </summary>
    private void Moving()
    {
        //check if any axis keys are pressed
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            rigBody.MoveRotation(rigBody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X"), 0)));
            rigBody.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * speed)
                + (transform.right * Input.GetAxis("Horizontal") * speed));
            
        }

        RotateCharacter();
    }    

    /// <summary>
    /// Rotate the camera to look around using mouse
    /// </summary>
    private void RotateCharacter()
    {
        rotX += Input.GetAxis("Mouse X") * rotationSpeed;
        rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.eulerAngles = new Vector3(0, rotX, 0);

        cameraObject.transform.Rotate(-rotY, 0, 0);
        Vector3 rot = cameraObject.transform.localRotation.eulerAngles;
    }

    /// <summary>
    /// Cast a ray from the camera in forward direction for interaction with objects
    /// </summary>
    private void ShootRaycast()
    {
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.75f);
        objPos = cameraObject.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButton(0))
        {
            if (gamemanager.CanCastRay)
            {
                if (Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, out hit, rayDistance))
                {
                    if (hit.collider.tag == "calllift")
                    {
                        if (hit.collider.GetComponent<LiftButtonScript>().buttonPressed == false)
                        {
                            hit.collider.GetComponent<LiftButtonScript>().buttonPressed = true;
                            //LeanTween.moveZ(hit.collider.gameObject, hit.collider.transform.localPosition.z - liftCallButtonAnimOffset, 0.5f).setEaseInOutBounce();
                            L_Manager.lftButtnScript = hit.collider.GetComponent<LiftButtonScript>();
                            L_Manager.FloorNo = hit.collider.GetComponent<LiftButtonScript>().floorNo;
                            L_Manager.Finding_The_Nearest_Lift_To_The_Player();
                        }
                    }
                    else if (hit.collider.tag == "liftbutton")
                    {
                        if (L_Manager.ElevatorStartedToMove == false)
                        {
                            //LeanTween.moveZ(hit.collider.gameObject, hit.collider.transform.localPosition.x + liftFloorOffsetButton, 0.5f);
                            L_Manager.lftButtnScript.buttonPressed = false;
                            L_Manager.ElevatorStartedToMove = true;
                            L_Manager.toFloorNo = int.Parse(hit.collider.name);
                            L_Manager.Take_The_Player_To_The_Floor_Pressed();
                        }
                    }
                }
            }
        }
    }

}

   
