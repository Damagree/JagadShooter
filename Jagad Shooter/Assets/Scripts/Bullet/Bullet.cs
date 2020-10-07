using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 5f;
    public float deactive = 3;

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
            temp.y -= speed * Time.deltaTime;
        }

        transform.position = temp;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
