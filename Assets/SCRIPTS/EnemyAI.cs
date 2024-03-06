using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public NavMeshAgent enemy;

    public GameObject[] wayPoints;//waypoint for enemy patrolling

    public GameObject Flag;

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

        //enemy is by defualt set to the patrol state and the first wayPoint in the wayPoints array
    }
    public void Update()
    {
        distanceTo = Vector3.Distance(transform.position, player.position);//transform.position is the position of the enemy and player.position

        switch (presentState)
        {
            case States.Retrieve:
                Retrieve();
                if (distanceTo <= withinChaseRange)
                {
                    

                    presentState = States.Chase;

                }
                break;

            case States.Chase:
                Chase();
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
    /*  private void OnTriggerEnter(Collider other)
      {
          if (other.CompareTag("WayPoint"))
          {
              entered = true;                    //THIS CAUSED PROBLEMS BECAUSE IT JUST CHECKS IF THE PLAYER HAS ENTERED ANY WAYPOINT AND NOT ITS CURRENT WAYPOINT. NO NEED FOR THIS METHOD ANYMORE
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
            enemy.destination = Flag.transform.position;//sets a new wayPoint for the enemy to move to
        

    }
    public void Chase()
    {
        enemy.destination = player.position;
    }
    public void Attack()
    {
        Debug.Log("Attacks");
    }


}
