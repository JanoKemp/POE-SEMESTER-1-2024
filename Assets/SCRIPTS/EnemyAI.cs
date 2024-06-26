using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] FlagEquip flagEquip;
    
    /// </summary>
    public Transform player;

    public NavMeshAgent enemy;

    public GameObject[] wayPoints;//waypoint for enemy patrolling

    public GameObject RedFlag;
    public GameObject BlueFlag;

    public Transform RedBase;

    public Transform playerHold;
    public Transform enemyHold;
    public Transform blueFlagSpawn;
    public Transform redFlagSpawn;

    public float inPickUpRange = 2f;
    
    public float distanceTo;
    public float withinAttackRange = 1f;
    [SerializeField]
    private bool entered = false;
    public bool catchBlueFlag = false;  
    public bool isBlueEquipped = false;
    public bool inContactWithBase = false;



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
        
        if (other.CompareTag("RedBase"))
        {
            inContactWithBase = true;
            Score.instance.EnemyScore();//get addEnemyScore to add point to enemyScore when enemy comes in contact with RedBase
        }


    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("red flag"))
        {
            entered = false;
        }

        if (other.CompareTag("blue flag"))
        {
            catchBlueFlag = false;
        }

       
    }

    public void Update()
    {
        
        Debug.Log("In contact with base: " + inContactWithBase);



        isBlueEquipped = flagEquip.isBlueFlagPickedUp;
        //Debug.Log("blue equipped: " + isBlueEquipped);
        //Debug.Log("In contact with enemy: " + catchBlueFlag);

        distanceTo = Vector3.Distance(transform.position, player.position);//transform.position is the position of the enemy and player.position

        switch (presentState)
        {
            case States.Retrieve:
                Retrieve();

                if (entered)//if enemy has picked up red
                {
                    
                    presentState = States.Return;

                }
                else if (isBlueEquipped && entered == false)//player picked up blue and enemy has not picked up red
                {
                    presentState = States.Chase;
                }
                
                break;

            case States.Return:
                Return();

                if (entered == false)//if enemy has not picked up red
                {
                    presentState = States.Retrieve;
                }

                break;

            case States.Chase:
                Chase();
                if (isBlueEquipped == false) // has player picked up blue
                {

                    presentState = States.Retrieve;

                }
                
                if (catchBlueFlag)//if enemy has captured blue flag
                {
                    presentState = States.Attack;
                }

             
                break;
            case States.Attack:

                Attack();
                if (isBlueEquipped == false)//if player does not have blue flag
                {
                    presentState = States.Retrieve;
                }
                break;


        }
    }
     
    

     public void Return()
     {
        enemy.destination = RedBase.position;
     }
    
    public void Retrieve()
    {
        
         enemy.destination = RedFlag.transform.position;
        
        if (entered)
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
