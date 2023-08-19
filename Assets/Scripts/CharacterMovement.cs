using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("===Walking===")]
    public float speed;
    private float SetSpeed;
    public float speedBackwards;
    public float rotationSpeed;
    public float tiltAngle;
    float VerticalInput;
    Vector3 moveDirection;
    Rigidbody rb;


    [Header("===Interaction===")]
    public bool canInteract;
    public bool aiming;
    private bool Cutscene;
    private Collider tempInteraction;

    private CameraController ctr;
    public Animator charAnim;

    public GameObject enemyDetector;
    PlayerDetector detector;
    BoxCollider detectorCollider;

    float translation;

    [Header("===Strzelanie===")]
    public float maxCzascd;
    float Czascd;
    public bool rdy;
    float ColsizeZ;
    float colCenter;

    public bool blockedWalking;

    [Header("===Zdrowie===")]
    public int health;
    public int maxHealth;
    public GameObject splashEffect;
    public bool dead;

    [Header("=Ammunition=")]
    public int pistolAmmo;
    public int shotgunAmmo;
    public bool shotgun;
    public bool axe;
    public GameObject pistolObj1;
    public GameObject pistolObj2;
    public GameObject shotgunObj;
    public GameObject axeObj;

    [Header("=CoPosiadam=")]
    public bool posiadamAxe;
    public bool posiadamShotgun;
    public bool posiadamKlucz1;
    public bool posiadamKlucz2;


    //[Header("Aoe")]
    //public GameObject shotgunAoe;
    //public GameObject axeAoe;
    //public Transform axeShotgunTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetSpeed = speed;
        ctr = GameObject.Find("GeneralController").GetComponent<CameraController>();
        detector = enemyDetector.GetComponent<PlayerDetector>();
        detectorCollider = enemyDetector.GetComponent<BoxCollider>();
        Czascd = maxCzascd;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !dead)
        {
            dead = true;
            ctr.Cutscene = true;
        }


        ///Cheaty
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetSpeed++;
            speedBackwards++;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SetSpeed--;
            speedBackwards--;
        }


        ///sprawdzanie cutscenki


        if (ctr.Cutscene == false)
        {
            if(!rdy && aiming)ResetCooldown();

            if(blockedWalking == false)
            {
                ///PODMIANA BRONI///
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {

                    pistolObj1.SetActive(true);
                    pistolObj2.SetActive(true);
                    if (shotgun)
                    {
                        shotgun = false;
                        shotgunObj.SetActive(false);
                        charAnim.SetBool("Shotgun", false);
                    } 
                    if (axe)
                    {
                        axe = false;
                        axeObj.SetActive(false);
                        charAnim.SetBool("Axe", false);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha2) && posiadamShotgun)
                {
                    pistolObj1.SetActive(false);
                    pistolObj2.SetActive(false);
                    if (!shotgun)
                    {
                        shotgun = true;
                        shotgunObj.SetActive(true);
                        charAnim.SetBool("Shotgun", true);

                    }
                    if (axe)
                    {
                        axe = false;
                        axeObj.SetActive(false);
                        charAnim.SetBool("Axe", false);

                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha3) && posiadamAxe)
                {
                    pistolObj1.SetActive(false);
                    pistolObj2.SetActive(false);

                    if (shotgun)
                    {
                        shotgun = false;
                        shotgunObj.SetActive(false);
                        charAnim.SetBool("Shotgun", false);

                    }
                    if (!axe)
                    {
                        axe = true;
                        axeObj.SetActive(true);
                        charAnim.SetBool("Axe", true);

                    }
                }
                ///-PODMIANA BRONI-///

                /// INPUTS
                if (Input.GetKey(KeyCode.S))
                {
                    charAnim.SetBool("WalkB", true);
                    speed = speedBackwards;
                }
                else
                {


                    if (speed > 0 && !Input.GetKey(KeyCode.W))
                    {
                        speed -= Time.deltaTime;
                    }
                }

                if (Input.GetKey(KeyCode.W))
                {
                    charAnim.SetBool("WalkF", true);
                    speed = SetSpeed;
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    charAnim.SetBool("WalkF", false);
                }

                if (Input.GetKeyUp(KeyCode.S))
                {
                    charAnim.SetBool("WalkB", false);
                }

                //interakt
                if (Input.GetKeyDown(KeyCode.E) && canInteract == true)
                {
                    interact();
                }


                /// INPUTS END
                /// 

                float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

                translation *= Time.deltaTime;
                rotation *= Time.deltaTime;



                moveDirection = transform.forward * VerticalInput;
                //rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);



                // Move translation along the object's z-axis
                transform.Translate(0, 0, translation);
                transform.Rotate(0, rotation, 0);
            }//-blocked walking
            


            //aim
            if (Input.GetKey(KeyCode.C))
            {
                aiming = true;
                blockedWalking = true;
                charAnim.SetBool("Aim", true);
                if (Input.GetKeyDown(KeyCode.LeftControl) && rdy == true)
                {
                    if(shotgunAmmo > 0 && shotgun) shootGun();
                    if (pistolAmmo > 0 && !shotgun && !axe) shootGun();
                    if (axe) shootGun();
                }
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                aiming = false;
                charAnim.SetBool("Aim", false);
                detectorCollider.center = new Vector3(0f, 0.25f, 0f);
                detectorCollider.size = new Vector3(2f, 2.12f, 0f);
                rdy = false;
                if (detector.spottedEnemy) detector.spottedEnemy = null;
            }


        }// end cutscene



    }

    private void FixedUpdate()
    {
        translation = Input.GetAxis("Vertical") * speed;
        VerticalInput = Input.GetAxis("Vertical");
    }


    public void interact()
    {
        if(tempInteraction)tempInteraction.gameObject.GetComponent<Doors>().Interact();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            canInteract = true;
            tempInteraction = other;
        }

        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            canInteract = false;
            tempInteraction = null;
        }
    }

    void ResetCooldown()
    {
        if(Czascd <= 0)
        {
            ColsizeZ = 0f;
            colCenter = 0f;
            rdy = true;
            Czascd = maxCzascd;
        }
        else
        {
            Czascd -= Time.deltaTime;
            if(ColsizeZ < 10)ColsizeZ += Time.deltaTime * 5f;
            if(colCenter < 6)colCenter += Time.deltaTime * 3f;
            detectorCollider.size = new Vector3(2f, 2.12f, ColsizeZ);
            detectorCollider.center = new Vector3(0f, 0.25f, colCenter);
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        //anim damage
        GameObject splashEffectObj = Instantiate(splashEffect, transform.position, Quaternion.identity);

    }

    public void shootGun()
    {
        detectorCollider.center = new Vector3(0f, 0.25f, 0f);
        detectorCollider.size = new Vector3(2f, 2.12f, 0f);
        rdy = false;
        if (shotgun)
        {
            shotgunAmmo--;
        }
        if (!shotgun && !axe)
        {
            detector.Shoot();
            pistolAmmo--;
        }


        charAnim.SetTrigger("Shoot");
    }


    public void addItem(int ktore)
    {
        if(ktore == 5)
        {
            if (health + 30 > 100)
            {
                health = 100;
            }
            else
            {
                health = health + 30;
            }
        }
        if(ktore == 3)
        {
            pistolAmmo += 10;
        }
        if(ktore == 4)
        {
            shotgunAmmo += 3;
        }
    }


}
