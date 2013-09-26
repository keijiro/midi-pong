using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public float velocity = 10.0f;
    public float radius = 0.5f;
    public float accel = 1.1f;
    public float xLimit = 16.0f;
    Vector3 direction;

    public static Vector3 initialDirection = Vector3.right;

    void Start ()
    {
        direction = initialDirection;
    }

    void Update ()
    {
        transform.position += direction * (velocity * Time.deltaTime);

        if (Mathf.Abs (transform.position.x) > xLimit) {
            GameObject.FindWithTag("Manager").SendMessage ("Increment", transform.position.x < 0.0f ? 1 : 0);
            Destroy (gameObject);
        }

        var colliders = Physics.OverlapSphere (transform.position, radius);
        foreach (var collider in colliders) {
            if (collider.tag == "Wall") {
                if (direction.y * collider.transform.position.y > 0.0f) {
                    direction.Set(direction.x, -direction.y, 0);
                }
            } else {
                if (direction.x * collider.transform.position.x > 0.0f) {
                    var vy = collider.transform.position.y - transform.position.y;
                    direction = new Vector3(-direction.x, vy, 0).normalized;
                    velocity *= accel;
                }
            }
        }
    }

    void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position, radius);
    }
}