using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    FlagEquip flagEquip;
    public float moveSpeed = 5f; // Speed of movement
    private float inPickUpRange = 2f;
    public Rigidbody rb; // Reference to the Rigidbody component
    public GameObject RedFlag;
    public GameObject BlueFlag;
    public Transform playerHold;
    public Transform blueBase;
    private bool redCaptured;


    private void Start()
    {
        flagEquip = BlueFlag.GetComponent<FlagEquip>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("red flag"))
        {
            redCaptured = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("red flag"))
        {
            redCaptured = false;
        }
    }
   

    void Update()
    {
        Debug.Log("is red captured: " + redCaptured);

        // Input for movement in horizontal (left/right) and vertical (forward/backward) directions
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // Move the player based on the calculated movement
        rb.MovePosition(rb.position + transform.TransformDirection(movement));

        FlagPickUp();
    }


    void FlagPickUp()
    {
        //Debug.Log(flagEquip.isBlueFlagPickedUp);

        if (flagEquip.isBlueFlagPickedUp)
        {
            BlueFlag.GetComponent<Rigidbody>().isKinematic = true;
            BlueFlag.transform.position = playerHold.transform.position; //sets flag posistion to position of empty object attached to player
                                                                         // Flag.transform.rotation = FlagHold.transform.rotation; //sets flag rotation to rotation of empty object attached to player
                                                                         //BlueFlag.GetComponent<MeshCollider>().enabled = false;//disabled the flags collider to prevent it being triggered
            BlueFlag.transform.SetParent(playerHold); //sets the empty object called FlagHold as the parent to the gun
        }
        else if (redCaptured)
        {
            RedFlag.GetComponent<Rigidbody>().isKinematic = true;
            RedFlag.transform.position = blueBase.transform.position; //sets flag posistion to position of empty object attached to player
                                                                       // Flag.transform.rotation = FlagHold.transform.rotation; //sets flag rotation to rotation of empty object attached to player
                                                                       // RedFlag.GetComponent<BoxCollider>().enabled = true;//disabled the flags collider to prevent it being triggered
            RedFlag.transform.SetParent(blueBase); //sets the empty object called FlagHold as the parent to the gun        }
        }
    }
}
