using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koala : TalkOnTouch
{
    [SerializeField] private NPCConversation conversation = null;
    [SerializeField] private ParticleSystem[] bloodParticleSystems = null;

    private new void Start()
    {
        base.Start();
        Cam = CameraScript.Instance.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        ConversationManager.OnConversationEnded += OnConversationEnded;
    }

    private void OnDisable()
    {
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }

    private void OnConversationEnded()
    {
        if(CurrentConversation == conversation)
        {
            TriggerDisableSwitch();
            TouchCollider.enabled = false;
        }
    }

    private void TriggerDisableSwitch()
    {
        GetComponent<Animator>().SetTrigger("disableSwitch");
    }

    public void OnKoalaDeath()
    {
        GetComponent<Animator>().enabled = false;
        AudioManager.Instance.PlaySoundIfNotPlaying(AudioClipNames.LoudSquish);
        foreach(ParticleSystem ps in bloodParticleSystems)
        {
            ps.Play();
        }
    }

    protected override NPCConversation GetNextConversation()
    {
        if(CurrentConversation == PromptConversation)
            return conversation;
        return PromptConversation;
    }

    protected override void LoadAllConversations()
    {
        StartConversation(conversation);
        EndConversation();
    }
}
