using UnityEngine;

public class MenuCameraScroiler : MonoBehaviour
{
    public float speed = 2f;

    public Vector2 direction = new Vector2(1f, 1f);
    void Update()
    {
        Vector3 moveDelta = (Vector3)direction.normalized * speed * Time.deltaTime;
        transform.position += moveDelta;
    }
}
