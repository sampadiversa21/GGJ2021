using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Leaf : MonoBehaviour
{
    private Rigidbody2D rb;
    private float gravityScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    //IEnumerator ChangeSpeed()
    //{
        
    //}
}
