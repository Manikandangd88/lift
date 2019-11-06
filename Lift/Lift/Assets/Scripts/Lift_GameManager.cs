using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift_GameManager : MonoBehaviour
{
    public bool CanCastRay = true;           //flag to allow ray to be casted

    public GameObject FloorUI;               //UI object for floor navigation
    [SerializeField]
    private GameObject Player;               //Player object

    [SerializeField]
    private LiftManager lftmanager;          //lift manager script    

    // Update is called once per frame
    void Update()
    {
        //Check if the escape button is pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Enable_Or_Disable_The_FloorSelectionUI();
        }
    }

    /// <summary>
    /// Enable or disable the manual floor selection UI object 
    /// If the UI object is swithed on - it gets switched off. If the UI object is switched off - it gets switched on.
    /// The mouse pointer is locked and the visiblity is switched off, when the UI object is disabled
    /// the mouse pointer is unlocked and the visiblity is switched on, when the UI object is enabled
    /// </summary>
    private void Enable_Or_Disable_The_FloorSelectionUI()
    {
        if (FloorUI.activeSelf == true)
        {
            FloorUI.SetActive(false);
            CanCastRay = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
        else
        {
            FloorUI.SetActive(true);
            CanCastRay = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
    }

    /// <summary>
    /// Manually teleporting the player to the floor, which he pressed in the UI object
    /// </summary>
    /// <param floor no ="no"></param>
    public void NavigatePlayerToTheFloorSelected(int no)
    {
        Player.transform.position = new Vector3(lftmanager.floorPoints[no - 1].position.x,
                                                lftmanager.floorPoints[no - 1].position.y + 2f,
                                                lftmanager.floorPoints[no - 1].position.z);

        Enable_Or_Disable_The_FloorSelectionUI();        
    }
}
