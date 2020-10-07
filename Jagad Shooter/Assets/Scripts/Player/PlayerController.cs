using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float XMin = -2.5f, XMax = 2.5f;

    [Space(10)]
    [Header("Shoot Setting")]
    public bool canShoot;
    public GameObject bullet;
    public Transform attackPoint;
    public float shootInterval = .35f;
    public float currentInterval;

    private void Start()
    {
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

#if (UNITY_EDITOR)
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

    void Shoot()
    {
        GameObject bullets = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        bullets.GetComponent<Bullet>().isPlayer = true;
        bullets.transform.tag = gameObject.tag;
    }
}
