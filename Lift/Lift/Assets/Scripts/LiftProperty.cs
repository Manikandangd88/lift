using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftProperty : MonoBehaviour
{
    private Animator liftanimator;       //reference to the animator component

    [SerializeField]
    private GameObject player;           //reference to the player object

    // Start is called before the first frame update
    void Start()
    {
        liftanimator = GetComponent<Animator>();
    }    

    /// <summary>
    /// When the player enters the lift the door closes
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            liftanimator.SetTrigger("close");
        }
    }

    /// <summary>
    /// When player exits the lift the lift door closes
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            liftanimator.SetTrigger("close");
            player.transform.parent = null;
        }
    }
}
