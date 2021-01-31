using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmote : MonoBehaviour
{

    public SpriteRenderer emoteRenderer;
    [SerializeField]
    public EmoteConfig[] emotes;

    private bool playing = false;
    private int currentEmote;
    private int currentFrame;
    private bool pingPongReturning;
    private float frameTime;
    private float totalTime;
    private int loopCount;

    private void Awake()
    {
        UpdateSprite(null);
    }

    public static void PlayPlayerEmote(string emote)
    {
        FindObjectOfType<Platformer.Mechanics.PlayerController>().gameObject.GetComponentInChildren<PlayerEmote>().Play(emote);
    }

    public void Play(string emote)
    {
        int index = -1;
        for (int x = 0; x < emotes.Length; x++)
        {
            if (emotes[x].emoteName == emote)
            {
                index = x;
                break;
            }
        }

        if (index < 0)
        {
            Debug.LogError("Unable to find emote named \"" + emote + "\".");
            return;
        }

        currentEmote = index;
        currentFrame = 0;
        pingPongReturning = false;
        frameTime = 0;
        totalTime = 0;
        loopCount = 0;
        playing = true;
    }

    public void Finish()
    {
        currentEmote = 0;
        currentFrame = 0;
        pingPongReturning = false;
        frameTime = 0;
        totalTime = 0;
        loopCount = 0;
        playing = false;
        UpdateSprite(null);
    }

    void Update()
    {
        if (playing)
        {
            UpdateEmoteTime(Time.deltaTime, emotes[currentEmote]);
        }
    }

    private void UpdateEmoteTime(float deltaTime, EmoteConfig emote)
    {
        frameTime += deltaTime;
        totalTime += deltaTime;

        if (totalTime >= emote.emoteTotalLife)
        {
            Finish();
            return;
        }

        if (frameTime >= emote.emoteFrameLife)
        {
            frameTime -= emote.emoteFrameLife;
            currentFrame += pingPongReturning ? -1 : 1;

            if (currentFrame >= emote.emoteSprites.Length)
            {
                if (emote.emotePingPong)
                {
                    pingPongReturning = true;
                    currentFrame -= 2;
                }
                else
                {
                    currentFrame = 0;
                    loopCount++;
                }
            }
            if (currentFrame < 0)
            {
                pingPongReturning = false;
                currentFrame = 0;
                loopCount++;
            }
        }

        if (loopCount >= emote.emoteMaxLoops)
        {
            Finish();
            return;
        }

        UpdateSprite(emote.emoteSprites[currentFrame]);
    }

    private void UpdateSprite(Sprite desiredSprite)
    {
        if (emoteRenderer.sprite != desiredSprite)
            emoteRenderer.sprite = desiredSprite;
    }

    [System.Serializable]
    public class EmoteConfig
    {
        /// <summary>
        /// Name used to call the emote.
        /// </summary>
        public string emoteName = "emote";
        /// <summary>
        /// Time in seconds each frame is displayed.
        /// </summary>
        public float emoteFrameLife = 0.2f;
        public float emoteTotalLife = 1.5f;
        public int emoteMaxLoops = 3;
        public bool emotePingPong = false;
        public Sprite[] emoteSprites = new Sprite[1];
    }
}
