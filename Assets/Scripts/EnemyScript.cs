using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public bool isStatic; //czy stoi czy chodzi
    public float health;
    public int damage;


    private CameraController ctr;


    [Header("Sztuczna Inteligencja")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //Atacking
    public float timeBeetweenAttacks;
    public bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    public float speed;

    [Header("Idle")]
    public float idleTime;
    float idleTimeMax;
    public bool readyToWalk = true;

    //animacje
    [Header("Animacje")]
    public Animator anim;
    private bool changedStances;
    private bool stance2;
    bool charge;
    bool dead;
    bool randomizedIdle;
    public bool imHook;
    public GameObject bloodSplat;
    public int randomAttackRange;

    public BoxCollider box;

    [Header("Muzyka")]
    public AudioClip idleClip;
    public AudioClip hurtClip;
    public AudioClip dedClip;
    public AudioClip spottedClip;
    public AudioSource source;
    bool sawplayer;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
        box = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        ctr = GameObject.Find("GeneralController").GetComponent<CameraController>();
        idleTimeMax = idleTime;

    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0 && !dead)
        {
            dead = true;
            if(Random.Range(0,2) == 0) anim.SetTrigger("Dying1");
            else anim.SetTrigger("Dying2");
            agent.SetDestination(transform.position);
            anim.SetBool("Dying", true);
            transform.gameObject.tag = "dead";
            box.enabled = false;
            agent.enabled = false;

            source.clip = dedClip;
            source.Play();
        }


        ///======= CUTSCENE ============
        if (ctr.Cutscene == false && !dead)
        {
            PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (sawplayer && !PlayerInSightRange) sawplayer = false;


            if (!PlayerInSightRange && !PlayerInAttackRange && alreadyAttacked == false) Patrol();
            if (PlayerInSightRange && !PlayerInAttackRange && alreadyAttacked == false) Chase();
            if (PlayerInSightRange && PlayerInAttackRange) Attack();


        }
        else
        {


        }///-CUTSCENE



    }

    


    private void Patrol()
    {
        if (!walkPointSet && readyToWalk)
        {
            SearchWalkPoint();
        }
            

        if (walkPointSet && readyToWalk)
        {
            agent.SetDestination(walkPoint);
            anim.SetBool("Walking", true);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            readyToWalk = false;
            //Invoke(nameof(resetIdle), idleTime);
            resetIdle2();
            agent.SetDestination(transform.position);
            anim.SetBool("Walking", false);

        }

    }

    private void resetIdle2()
    {
        source.clip = idleClip;
        source.Play();


        if (!randomizedIdle && imHook)
        {
            int zapp = Random.Range(0, 3);
            if (zapp == 1)
            {
                anim.SetTrigger("Idle1");
            } 
            if(zapp == 2) anim.SetTrigger("Idle2");
            randomizedIdle = true;
        }

        if(idleTime <= 0)
        {
            idleTime = Random.Range(idleTimeMax - 3, idleTimeMax + 3);
            agent.angularSpeed = 120;
            readyToWalk = true;
            randomizedIdle = false;
        }
        else
        {
            if(agent.angularSpeed != 0) agent.angularSpeed = 0;
            idleTime -= Time.deltaTime;
        }
    }


    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Chase()
    {
        if (!dead)
        {
            if (!sawplayer)
            {
                source.clip = spottedClip;
                source.Play();
                sawplayer = true;
            }



            agent.SetDestination(player.position);
            if (charge == false) anim.SetBool("Walking", true);
            else anim.SetBool("Running", true);
            if (!changedStances) randomizeStance();
            if (readyToWalk != true) readyToWalk = true;
            if (agent.angularSpeed != 120) agent.angularSpeed = 120;
        }
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        //transform.LookAt(player);


        if (!alreadyAttacked)
        {
            if (charge == true)
            {
                charge = false;
                agent.speed = speed;
                anim.SetBool("Running", false);
                anim.SetTrigger("Attack1");
            }
            else
            {
                if (Random.Range(0, randomAttackRange) == 0)
                {
                    anim.SetTrigger("Attack1");
                }
                else
                {
                    anim.SetTrigger("Attack2");
                }
            }
            alreadyAttacked = true;

        }
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        if (!PlayerInSightRange && !PlayerInAttackRange)
        {
            Chase();
        }
        else
        {
            agent.SetDestination(transform.position);
        }
        health -= damage;

        source.clip = hurtClip;
        source.Play();

        if(!charge) anim.SetTrigger("Hurt");
        anim.SetBool("Walking", false);
        if(imHook)randomRunning();
        GameObject bloodSplatObj = Instantiate(bloodSplat, transform.position, Quaternion.identity);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void randomRunning()
    {
        if (!PlayerInAttackRange)
        {
            int zap = Random.Range(0, 3);
            if (zap == 0)
            {
                agent.speed = 3f;
                charge = true;
            }
        }

    }

    public void randomizeStance()
    {
        if (!changedStances)
        {
            int zap = Random.Range(0, 2);
            if(zap == 0)
            {
                stance2 = false;
            }
            else
            {
                stance2 = true;
                anim.SetBool("State1", false);
                anim.SetBool("State2", true);
            }

            changedStances = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Aoe") && !dead)
        {
            TakeDamage(other.GetComponent<AoeDmg>().Damage);
        }
    }

}
