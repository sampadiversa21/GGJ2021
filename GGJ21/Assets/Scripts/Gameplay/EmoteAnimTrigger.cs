using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteAnimTrigger : MonoBehaviour
{
    public PlayerEmote targetEmote;

    public void TriggerEmote(string emote)
    {
        if (targetEmote)
            targetEmote.Play(emote);
        else
            PlayerEmote.PlayPlayerEmote(emote);
    }
}
