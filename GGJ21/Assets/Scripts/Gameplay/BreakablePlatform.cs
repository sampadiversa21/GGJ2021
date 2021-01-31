using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{

    public float breakTime = 1;
    public float resetTime = 3;
    public float fallSpeed = 10;
    public float fallAngSpeed = 200;
    public bool resetOnLeave = true;
    public bool timeAsSpring = true;
    public float shakeTimeStep = 0.08f;
    public Color originalSpriteColor = Color.white;
    public Color shakeSpriteColor = new Color(0.8f, 0.5f, 0.5f);
    public string[] objectTagFilter = new string[] { "Player" };

    private bool playerStepping = false;
    private bool broken = false;
    private float currentTime = 0;
    private Vector3 originalPosition;
    private Vector3 originalRotartion;

    private Collider2D myCollider;
    private SpriteRenderer myRenderer;
    private List<Collider2D> currentColliders = new List<Collider2D>();


    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        originalRotartion = transform.eulerAngles;
        currentTime = breakTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        string otherTag = collision.collider.tag;
        for (int x = 0; x < objectTagFilter.Length; x++)
        {
            if (objectTagFilter[x] == otherTag)
            {
                currentColliders.Add(collision.collider);
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        string otherTag = collision.collider.tag;
        for (int x = 0; x < objectTagFilter.Length; x++)
        {
            if (objectTagFilter[x] == otherTag)
            {
                currentColliders.Remove(collision.collider);
                break;
            }
        }
    }


    void Update()
    {
        if (broken)
            CalcFall();
        else
            CalcBreakTime();

        AnimateShake();
    }


    private void AnimateShake()
    {
        float alpha = (breakTime - currentTime) * (broken ? 0 : 1);
        alpha = ((Mathf.FloorToInt(currentTime / shakeTimeStep) % 2) == 0) ? 0 : alpha;

        myRenderer.color = Color.Lerp(originalSpriteColor, shakeSpriteColor, alpha);
    }

    private Vector3 fallVelocity = Vector3.zero;
    private Vector3 fallAngVelocity = Vector3.zero;
    private void CalcFall()
    {
        fallVelocity += Vector3.down * Time.deltaTime * fallSpeed;
        fallAngVelocity += Vector3.forward * Time.deltaTime * fallAngSpeed;
        transform.position += fallVelocity * Time.deltaTime;
        transform.eulerAngles += fallAngVelocity * Time.deltaTime;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            ResetPlatform();
        }
    }

    private void CalcBreakTime()
    {
        if (resetOnLeave)
            playerStepping = currentColliders.Count > 0;
        else
            playerStepping = playerStepping || currentColliders.Count > 0;

        if (!playerStepping)
        {
            if (!timeAsSpring)
                currentTime = breakTime;
            else
            {
                currentTime += Time.deltaTime;
                currentTime = Mathf.Min(currentTime, breakTime);
            }
        }
        else
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                BreakPlatform();
            }
        }
    }

    public void BreakPlatform()
    {
        if (broken)
            return;

        broken = true;
        currentTime = resetTime;
        fallVelocity = Vector3.zero;
        fallAngVelocity = Vector3.zero;
        myCollider.enabled = false;
        playerStepping = false;
    }

    public void ResetPlatform()
    {
        if (!broken)
            return;
        broken = false;

        fallVelocity = Vector3.zero;
        fallAngVelocity = Vector3.zero;
        currentTime = breakTime;
        transform.position = originalPosition;
        transform.eulerAngles = originalRotartion;
        myCollider.enabled = true;
        playerStepping = false;
    }
}
