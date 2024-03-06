using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public NavMeshAgent enemy;

    public GameObject[] wayPoints;//waypoint for enemy patrolling

    private int currentWayPoint;// need for this
    public float distanceTo;
    public float withinAttackRange = 1f;
    private float withinChaseRange = 3f;
    [SerializeField]
    private bool entered = false;



    private enum States
    {
        Patrol, Chase, Attack
    }
    private States presentState;

    public void Start()
    {
        currentWayPoint = 0;
        enemy.destination = wayPoints[0].transform.position;
        presentState = States.Patrol;

        //enemy is by defualt set to the patrol state and the first wayPoint in the wayPoints array
    }
    public void Update()
    {
        distanceTo = Vector3.Distance(transform.position, player.position);//transform.position is the position of the enemy and player.position

        switch (presentState)
        {
            case States.Patrol:
                Patrol();
                if (distanceTo <= withinChaseRange)
                {
                    Debug.Log("true");

                    presentState = States.Chase;

                }
                break;

            case States.Chase:
                Chase();
                if (distanceTo <= withinAttackRange)
                {
                    presentState = States.Attack;
                }
                else if (distanceTo > withinChaseRange)
                {
                    presentState = States.Patrol;
                }
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
    public void Patrol()
    {
        int wayPoint;
        if (!enemy.pathPending && enemy.remainingDistance <= enemy.stoppingDistance)
        {
            //since the first wayPoint is set in start, that means the enemy has already calculated its path(!enemy.pathPending)
            //and because it is within the stopping distance (0.5f) of the wayPoint, the boolean entered is set to true, meaning it has reached the wayPoint.
            entered = true;
        }

        if (entered)//checks if entered is true or not
        {
            //since it is true it.....
            wayPoint = Random.Range(0, wayPoints.Length);
            enemy.destination = wayPoints[wayPoint].transform.position;//sets a new wayPoint for the enemy to move to
            entered = false; //entered is set to false because the player has not yeat reached its newly assigned wayPoint
        }






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
