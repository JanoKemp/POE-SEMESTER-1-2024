using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class EnemyAI : MonoBehaviour
{
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
    private int currentWayPoint;// need for this
    public float distanceTo;
    public float withinAttackRange = 1f;
    private float withinChaseRange = 3f;
    [SerializeField]
    private bool entered = false;



    private enum States
    {
        Retrieve, Chase, Attack, Return
    }
    private States presentState;

    public void Start()
    {
       // currentWayPoint = 0;
        //enemy.destination = wayPoints[0].transform.position;
        presentState = States.Retrieve;
        RedFlag.GetComponent<Rigidbody>().isKinematic = true;
        BlueFlag.GetComponent<Rigidbody>().isKinematic = true;
        //enemy is by defualt set to the patrol state and the first wayPoint in the wayPoints array
    }
    public void Update()
    {
        distanceTo = Vector3.Distance(transform.position, player.position);//transform.position is the position of the enemy and player.position

        switch (presentState)
        {
            case States.Retrieve:
               
                if (RedFlag.transform.position == playerHold.transform.position || BlueFlag.transform.position == playerHold.transform.position)
                {
                    
                    presentState = States.Chase;

                }
                else Retrieve();
                break;

            case States.Chase:
                Chase();
                if (RedFlag.transform.position != playerHold.transform.position && BlueFlag.transform.position != playerHold.transform.position)
                {

                    Retrieve();

                }
                

                if (distanceTo <= withinAttackRange)
                {
                    presentState = States.Attack;
                }

              

                /* else if (distanceTo > withinChaseRange)
                 {
                     presentState = States.Patrol;
                 }
                */
                break;
            case States.Attack:
                Attack();
                if (distanceTo > withinAttackRange)
                {
                    presentState = States.Chase;
                }
                break;


        }
    }
     /* private void OnTriggerStay(Collider other)
      {
          if (other.CompareTag("red flag") || other.CompareTag("blue flag"))
          {
            PickUp();                   //THIS CAUSED PROBLEMS BECAUSE IT JUST CHECKS IF THE PLAYER HAS ENTERED ANY WAYPOINT AND NOT ITS CURRENT WAYPOINT. NO NEED FOR THIS METHOD ANYMORE
          }
      }
    */

    /* public void Patrol()
     {
         if (entered)   problem with this was that it checked if the player was within any wayPoint because of the ontriggerEnter method and because of this, it continuously assigned a new wayPoint
         {
             int wayPoint = Random.Range(0, 4);//excludes last number
             currentWayPoint = wayPoint;
             agent.destination = wayPoints[currentWayPoint].transform.position;
             entered = false;
         }
         else agent.destination = wayPoints[currentWayPoint].transform.position; //fetching random way point from array based on random number generated and seetting destination of agent 



     }
    */
    public void Retrieve()
    {
        //since it is true it.....
        // enemy.destination = RedFlag.transform.position;//sets a new wayPoint for the enemy to move to
        if (Vector3.Distance(transform.position, RedFlag.transform.position) > inPickUpRange)
        {
            enemy.destination = RedFlag.transform.position;
        }
        else if (Vector3.Distance(transform.position, RedFlag.transform.position) <= inPickUpRange)
        {
            RedFlag.GetComponent<Rigidbody>().isKinematic = true;
            RedFlag.transform.position = enemyHold.transform.position; //sets flag posistion to position of empty object attached to player
                                                                       // Flag.transform.rotation = FlagHold.transform.rotation; //sets flag rotation to rotation of empty object attached to player
            RedFlag.GetComponent<MeshCollider>().enabled = false;//disabled the flags collider to prevent it being triggered
            RedFlag.transform.SetParent(enemyHold); //sets the empty object called FlagHold as the parent to the gun
        }
        else if (Vector3.Distance(transform.position, BlueFlag.transform.position) <= inPickUpRange)
        {
            BlueFlag.transform.position = blueFlagSpawn.transform.position;
        }
       
        
        

    }
    /*void PickUp()
    {
        if(Vector3.Distance(transform.position, RedFlag.transform.position) <= inPickUpRange) 
        {
            RedFlag.GetComponent<Rigidbody>().isKinematic = true;
            RedFlag.transform.position = enemyHold.transform.position; //sets flag posistion to position of empty object attached to player
                                                                   // Flag.transform.rotation = FlagHold.transform.rotation; //sets flag rotation to rotation of empty object attached to player
            RedFlag.GetComponent<MeshCollider>().enabled = false;//disabled the flags collider to prevent it being triggered
            RedFlag.transform.SetParent(enemyHold); //sets the empty object called FlagHold as the parent to the gun
        }
    }
    */
    public void Chase()
    {
        enemy.destination = player.position;
    }
    public void Attack()
    {
        if (Vector3.Distance(transform.position, RedFlag.transform.position) <= inPickUpRange)
        {
            RedFlag.GetComponent<Rigidbody>().isKinematic = true;
            RedFlag.transform.position = redFlagSpawn.transform.position; //sets flag posistion to position of empty object attached to player
                                                                       // Flag.transform.rotation = FlagHold.transform.rotation; //sets flag rotation to rotation of empty object attached to player
            RedFlag.GetComponent<MeshCollider>().enabled = true;//disabled the flags collider to prevent it being triggered
            RedFlag.transform.SetParent(redFlagSpawn); //sets the empty object called FlagHold as the parent to the gun
        }
    }


}
