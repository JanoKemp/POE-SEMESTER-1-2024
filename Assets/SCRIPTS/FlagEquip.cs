using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagEquip : MonoBehaviour
{
    public GameObject Flag;//ref to flag object
    public Transform FlagHold;//ref to empty gameObject that is set as a child to the player and enemy because this is where the flag will be attached when picked up
    public float inPickUpRange = 2f;
    void Start()
    {
        Flag.GetComponent<Rigidbody>().isKinematic = true;//get rigidbody komponent attached to the flag and set isKinematic to true meaning no physics will have an effect of the flag
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Character") && Vector3.Distance(transform.position, other.transform.position) <= inPickUpRange)
        {
            PickUpFlag();
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    void PickUpFlag()
    {
        Flag.GetComponent<Rigidbody>().isKinematic = true;
        Flag.transform.position = FlagHold.transform.position; //sets flag posistion to position of empty object attached to player
       // Flag.transform.rotation = FlagHold.transform.rotation; //sets flag rotation to rotation of empty object attached to player
        Flag.GetComponent<MeshCollider>().enabled = false;//disabled the flags collider to prevent it being triggered
        Flag.transform.SetParent(FlagHold); //sets the empty object called FlagHold as the parent to the gun
    }
}
