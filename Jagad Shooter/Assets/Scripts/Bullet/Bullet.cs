using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 5f;
    public float deactive = 3;
    public float damageSent = 100f;
    public bool isPlayer;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        if (isPlayer)
        {
            temp.y += speed * Time.deltaTime;
        }
        else
        {
            temp.y -= speed * 1.5f * Time.deltaTime;
        }

        transform.position = temp;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damageSent);

            Destroy(gameObject);
        }
    }
}
