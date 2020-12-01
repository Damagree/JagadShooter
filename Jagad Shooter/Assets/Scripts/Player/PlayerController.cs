
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public bool isMoveRight = false;
    public bool isMoveLeft = false;
    public float XMin = -2.5f, XMax = 2.5f;
    public GameObject explotions;

    [Space(10)]
    [Header("Shoot Setting")]
    public bool canShoot;
    public GameObject bullet;
    public Transform attackPoint;
    public float shootInterval = .35f;
    public float currentInterval;

    public UnityEvent loseEvent;

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        currentInterval = shootInterval;
    }

    // Update is called once per frame
    void Update()
    {
        shootInterval += Time.deltaTime;
        if (shootInterval > currentInterval)
        {
            canShoot = true;
        }

        if (isMoveRight)
        {
            Vector3 temp = transform.position;
            temp.x += moveSpeed * Time.deltaTime;
            if (temp.x > XMax)
            {
                temp.x = XMax;
            }
            transform.position = temp;
        }
        else if (isMoveLeft)
        {
            Vector3 temp = transform.position;
            temp.x -= moveSpeed * Time.deltaTime;
            if (temp.x < XMin)
            {
                temp.x = XMin;
            }
            transform.position = temp;
        }

        #if UNITY_EDITOR
        MovePlayerByKeyboard();

        if (canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canShoot = false;
                shootInterval = 0f;
                Shoot();
            }
        }
        #endif
    }

    void MovePlayerByKeyboard()
    {
        Vector3 temp = transform.position;
        if (Input.GetAxis("Horizontal") > 0f)
        {
            temp.x += moveSpeed * Time.deltaTime;
            if (temp.x > XMax)
            {
                temp.x = XMax;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0f)
        {
            temp.x -= moveSpeed * Time.deltaTime;
            if (temp.x < XMin)
            {
                temp.x = XMin;
            }
        }

        transform.position = temp;
    }

    public void MoveLeft()
    {
        isMoveLeft = true;
    }

    public void NotMove()
    {
        isMoveRight = false;
        isMoveLeft = false;
    }

    public void MoveRight()
    {
        isMoveRight = true;
    }

    public void Shoot()
    {
        GameObject bullets = Instantiate(bullet, attackPoint.position, bullet.transform.rotation);
        bullets.GetComponent<Bullet>().isPlayer = true;
        bullets.transform.tag = gameObject.tag;
    }

    private void WillBeDestroyed()
    {
        loseEvent.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            float curentHealth = gameObject.GetComponent<Health>().currentHealth;

            if (curentHealth <= 0)
            {
                canShoot = false;
                anim.Play("Destroy");
                Invoke("WillBeDestroyed", .3f);
            }
            else
            {
                explotions.GetComponent<Animator>().Play("Explode_Main");
            }
            
        }
    }
}
