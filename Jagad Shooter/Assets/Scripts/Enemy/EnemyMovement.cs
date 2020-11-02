using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{

    public float pointGet = 100;

    public float speed = 5f;
    public float rotateSpeed = 50f;

    public bool canShoot;
    public bool canRotate;
    public bool canMove = true;

    public bool haveInsideCamera = false;

    public Transform bulletSpawn;
    public GameObject bulletPrefabs;

    private Animator anim;
    public UnityEvent destroyedEvent;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if (canRotate)
        {
            if (Random.Range(0, 2) > 0)
            {
                rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);
                rotateSpeed *= -1f;
            }
        }

        if (canShoot)
        {
            Invoke("StartShooting", Random.Range(1f, 3f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateEnemy();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.y += -speed * Time.deltaTime;
        transform.position = temp;
    }

    void RotateEnemy()
    {
        if (canRotate)
        {
            transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime), Space.World);
        }
    }

    void StartShooting()
    {
        GameObject bullet = Instantiate(bulletPrefabs, bulletSpawn.position, bulletPrefabs.transform.rotation);
        bullet.gameObject.GetComponent<Bullet>().isPlayer = false;
        bullet.transform.tag = gameObject.tag;
        if (canShoot)
        {
            Invoke("StartShooting", Random.Range(0.2f, 0.8f));
        }
    }

    private void WillBeDestroyed()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("booomm");
            canMove = false;
            if (canShoot)
            {
                CancelInvoke("StartShooting");
                canShoot = false;
            }
            canRotate = false;
            anim.Play("Destroy");
            PlayerData.Score += pointGet;
            Invoke("WillBeDestroyed", .3f);
        }
    }

    private void OnBecameVisible()
    {
        haveInsideCamera = true;
    }

    private void OnBecameInvisible()
    {
        if (haveInsideCamera)
        {
            Destroy(gameObject);
        }
    }
}
