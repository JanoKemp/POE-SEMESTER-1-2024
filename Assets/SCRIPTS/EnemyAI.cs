using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] FlagEquip flagEquip;
    public Transform player;

    public NavMeshAgent enemy;

    public GameObject[] wayPoints;//waypoint for enemy patrolling

    public GameObject RedFlag;
    public GameObject BlueFlag;

    public Transform playerHold;
    public Transform enemyHold;
    public Transform blueFlagSpawn;
    public Transform redFlagSpawn;

    public float inPickUpRange = 2f;
    
    public float distanceTo;
    public float withinAttackRange = 1f;
    private float withinChaseRange = 3f;
    [SerializeField]
    private bool entered = false;
    public bool catchBlueFlag = false;  
    public bool isBlueEquipped = false;



    private enum States
    {
        Retrieve, Chase, Attack, Return
    }
    private States presentState;

    public void Start()
    {
        flagEquip = BlueFlag.GetComponent<FlagEquip>();//setting flagEquip to the instance FlagEquip script on blue flag object so that i can access isBlueFlagPickedUp
        presentState = States.Retrieve;
        RedFlag.GetComponent<Rigidbody>().isKinematic = true;
        BlueFlag.GetComponent<Rigidbody>().isKinematic = true;
        //enemy is by defualt set to the patrol state and the first wayPoint in the wayPoints array
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("red flag"))
        {
            entered = true;
        }

        if (other.CompareTag("blue flag"))
        {
            catchBlueFlag = true;
        }
    }

    public void Update()
    {


        isBlueEquipped = flagEquip.isBlueFlagPickedUp;
        Debug.Log(isBlueEquipped);

        distanceTo = Vector3.Distance(transform.position, player.position);//transform.position is the position of the enemy and player.position

        switch (presentState)
        {
            case States.Retrieve:
                Retrieve();

                if (entered)
                {
                    
                    presentState = States.Return;

                }
                else if (isBlueEquipped && entered == false)
                {
                    presentState = States.Chase;
                }
                
                break;

            case States.Return:

                Return(); 

                break;

            case States.Chase:
                Chase();
                if (isBlueEquipped == false)
                {

                    presentState = States.Retrieve;

                }
                
                if (catchBlueFlag)
                {
                    presentState = States.Attack;
                }

             
                break;
            case States.Attack:

                Attack();
                if (isBlueEquipped == false)
                {
                    presentState = States.Retrieve;
                }
                break;


        }
    }
     
    

     public void Return()
     {
        enemy.destination = blueFlagSpawn.position;
     }
    
    public void Retrieve()
    {
        
        if (Vector3.Distance(transform.position, RedFlag.transform.position) > inPickUpRange)
        {
            enemy.destination = RedFlag.transform.position;
        }
        else if (entered)
        {
            RedFlag.GetComponent<Rigidbody>().isKinematic = true;
            RedFlag.transform.position = enemyHold.transform.position; //sets flag posistion to position of empty object attached to player
                                                                       // Flag.transform.rotation = FlagHold.transform.rotation; //sets flag rotation to rotation of empty object attached to player
            //RedFlag.GetComponent<BoxCollider>().enabled = false;//disabled the flags collider to prevent it being triggered
            RedFlag.transform.SetParent(enemyHold); //sets the empty object called FlagHold as the parent to the gun
        }
        
       
        
        

    }
    
    public void Chase()
    {
        enemy.destination = player.position;
    }
    public void Attack()
    {
        
        
            BlueFlag.GetComponent<Rigidbody>().isKinematic = true;
            BlueFlag.transform.position = blueFlagSpawn.transform.position; //sets flag posistion to position of empty object attached to player
                                                                       // Flag.transform.rotation = FlagHold.transform.rotation; //sets flag rotation to rotation of empty object attached to player
           // RedFlag.GetComponent<BoxCollider>().enabled = true;//disabled the flags collider to prevent it being triggered
            BlueFlag.transform.SetParent(blueFlagSpawn); //sets the empty object called FlagHold as the parent to the gun

        //isBlueEquipped == false;

       
    }


}
