using UnityEngine;
using Platformer.Mechanics;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerFall : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameController.Instance.cinematic1 && collision.tag == "FallLimit")
        {
            Debug.Log("Reached the fall limit");
            GetComponent<Transform>().position = spawnPoint.transform.position;
        }
    }
}
