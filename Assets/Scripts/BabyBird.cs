using UnityEngine;


public class BabyBird : MonoBehaviour
{
    public float speed = 2f;
    public float lifeTime = 10f;

    private bool facingRight = false;
    private Vector3 _flyDirection;

    private void Start()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized;
        _flyDirection = new Vector3(randomCircle.x, randomCircle.y, 0);

        if (_flyDirection.x < 0 && facingRight)
        {
            Flip();
        } else if (_flyDirection.x > 0 && !facingRight)
        {
            Flip();
        }
        
        Destroy(gameObject, lifeTime);
        
    }

    void Update()
    {
        transform.position += _flyDirection * speed * Time.deltaTime;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}