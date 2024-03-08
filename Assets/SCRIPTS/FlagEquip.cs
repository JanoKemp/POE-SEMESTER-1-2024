using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagEquip : MonoBehaviour
{
    public GameObject Flag;//ref to flag object
    public Transform FlagHold;//ref to empty gameObject that is set as a child to the player and enemy because this is where the flag will be attached when picked up
    public float inPickUpRange = 2f;
    public bool isBlueFlagPickedUp = false;
    void Start()
    {
        //Flag.GetComponent<Rigidbody>().isKinematic = true;//get rigidbody komponent attached to the flag and set isKinematic to true meaning no physics will have an effect of the flag
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isBlueFlagPickedUp=true;
        }
    }
   
   






    // Update is called once per frame
    void Update()
    {
        
    }

   
}
