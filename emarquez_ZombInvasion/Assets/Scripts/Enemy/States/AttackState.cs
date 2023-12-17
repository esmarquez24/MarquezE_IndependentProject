using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer()) // enemy can be seen
        {
            // lock the lose player timer and increment the move and shot timers
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            // if shot timer > fireRate
            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            // move the enemy to a random position after a random time
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else // lost sight of player
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                // change to search state
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
    public void Shoot()
    {
        // store reference to poisonSpawn
        Transform poisonspawn = enemy.poisonSpawn;
        // instantiate new poison orb
        GameObject poison = GameObject.Instantiate(Resources.Load("Prefabs/Poison") as GameObject, poisonspawn.position, enemy.transform.rotation);
        // calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - poisonspawn.transform.position).normalized;
        // add force to the rigidbody of poison
        poison.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f,3f),Vector3.up) * shootDirection * 40;
        Debug.Log("Shoot");
        shotTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
