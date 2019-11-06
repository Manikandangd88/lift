using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    public int FloorNo;               //var to store the current floor the player is standing
    public int toFloorNo;             //var to store the player wants to go

    public bool ElevatorStartedToMove = false;       //flag to tell that the elevator has started to move

    public GameObject Lift_1;         //lift 1 reference
    public GameObject Lift_2;         //lift 2 refererence
    public GameObject Player;         //player reference

    private Transform nearestLift;    //reference to the nearest lift to the player

    public LiftButtonScript lftButtnScript;        //reference to the liftbuttonscript

    public List<Transform> floorPoints;            //list of references of the floors

    /// <summary>
    /// This method is responsible for bringing the nearest lift to the floor, where he is standing
    /// </summary>
    public void Bring_The_Lift_To_The_Player()
    {        
        LeanTween.moveY(nearestLift.gameObject, floorPoints[FloorNo - 1].position.y, 3f).setOnComplete(() => Open_Or_Close_LiftDoor(1));
    }

    /// <summary>
    /// This method is responsible for playing the open and close animation of the lift door
    /// </summary>
    /// <param name="status"></param>
    public void Open_Or_Close_LiftDoor(int status)
    {
        ElevatorStartedToMove = false;
        if (status == 1)
        {
            nearestLift.GetComponent<Animator>().SetTrigger("open");
        }
        else if (status == 0)
        {
            nearestLift.GetComponent<Animator>().SetTrigger("close");
        }
    }

    /// <summary>
    /// This method is reponsible for translating the lift to the floor which the player pressed inside the lift
    /// </summary>
    public void Take_The_Player_To_The_Floor_Pressed()
    {
        Player.transform.SetParent(nearestLift);
        LeanTween.moveY(nearestLift.gameObject, floorPoints[toFloorNo - 1].position.y, 3f).setOnComplete(() => Open_Or_Close_LiftDoor(1));
    }

    /// <summary>
    /// This method is resposbible for finding which lift is near to the player.
    /// If both the lifts are near to the player either one of them will translate to the floor where the player is standing
    /// </summary>
    public void Finding_The_Nearest_Lift_To_The_Player()
    {
        float lift_1_Dist = Vector3.Distance(Lift_1.transform.position, Player.transform.position);
        float lift_2_Dist = Vector3.Distance(Lift_2.transform.position, Player.transform.position);

        if(lift_1_Dist > lift_2_Dist)
        {            
            nearestLift = Lift_2.transform;
        }
        else if (lift_1_Dist < lift_2_Dist)
        {            
            nearestLift = Lift_1.transform;
        }
        else if (lift_1_Dist == lift_2_Dist)
        {
            int randNo = Random.Range(0, 2);

            if(randNo == 0)
            {
                nearestLift = Lift_1.transform;
            }
            else
            {
                nearestLift = Lift_2.transform;
            }
        }

        Bring_The_Lift_To_The_Player();
    }
}
