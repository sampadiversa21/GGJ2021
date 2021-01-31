using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Leaf : MonoBehaviour
{
    public bool randomizeGravity = false;
    public float minGravityValue = 0.2f;
    public float maxGravityValue = 5f;
    private Rigidbody2D rb;
    private float gravityScale = 1;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        if (randomizeGravity)
        {
            rb.gravityScale = Random.Range(minGravityValue, maxGravityValue);
        }
    }
}
