using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    private float rotX = 0;
    private float rotY = 0;
    public float rotationSpeed = 1;
    private float verticalAxis = 0;
    private float horizontalAxis = 0;

    private Vector3 moveDirection = Vector3.zero;

    private Rigidbody rigBody;

    [SerializeField]
    private Camera cameraObject;

    private void Awake()
    {
        Initialization();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGameIsPaused();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void Initialization()
    {
        rigBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void MoveCharacter()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        rigBody.velocity = new Vector3(-horizontalAxis, 0, verticalAxis) * speed * Time.fixedDeltaTime;
        //rigBody.velocity = Vector3.forward * speed * Time.deltaTime;

        RotateCharacter();
    }

    private void RotateCharacter()
    {
        rotX += Input.GetAxis("Mouse X") * rotationSpeed;
        rotY = Input.GetAxis("Mouse Y") * rotationSpeed;               

        transform.eulerAngles = new Vector3(0, rotX, 0);       

        cameraObject.transform.Rotate(-rotY, 0, 0); 
        Vector3 rot = cameraObject.transform.localRotation.eulerAngles;        
    }

    private void CheckIfGameIsPaused()
    {
        if(Input.GetMouseButton(1))
        {
            Debug.Log("The mouse button pressed is : " + Input.GetMouseButton(1));
        }
    }

}
