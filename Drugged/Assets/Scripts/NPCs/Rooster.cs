using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.Events;

public class Rooster : TalkOnTouch
{
    [SerializeField] private NPCConversation conversation1 = null;
    [SerializeField] private NPCConversation conversation2 = null;
    [SerializeField] private NPCConversation conversationAfterFall = null;

    [SerializeField] private UnityEvent fallEvent = null;

    [SerializeField] private float acceptableDistanceFromApple = 0;

    [SerializeField] private Transform[] appleTransforms = null;
    
    private bool isConversation1Done = false;
    private bool hasFallen = false;
    private bool isConversationAfterFallDone = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private new void Start()
    {
        base.Start();
        Cam = CameraScript.Instance.gameObject.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        ConversationManager.OnConversationEnded += OnConversationEnded;
    }

    private void OnConversationEnded()
    {
        if(CurrentConversation == conversation1)
        {
            isConversation1Done = true;
        }
        else if(CurrentConversation == conversation2)
        {
            TouchCollider.enabled = false;
            Fly();
        }
    }

    protected override void LoadAllConversations()
    {
        StartConversation(conversation1);
        EndConversation();
        StartConversation(conversation2);
        EndConversation();
        StartConversation(conversationAfterFall);
        EndConversation();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, acceptableDistanceFromApple);
    }

    protected override NPCConversation GetNextConversation()
    {
        if(hasFallen)
        {
            if (!isConversationAfterFallDone)
            {
                isConversationAfterFallDone = true;
                return conversationAfterFall;
            }
            return PromptConversation;
        }
        else if (isConversation1Done)
        {
            foreach(Transform apple in appleTransforms)
            {
                if (TouchCollider.OverlapPoint(apple.position))
                {
                    Destroy(apple.gameObject);
                    return conversation2;
                }
            }
        }
        else if (!isConversation1Done && CurrentConversation == PromptConversation)
        {
            return conversation1;
        }

        return PromptConversation;
    }

    public void OnFall()
    {
        hasFallen = true;
        fallEvent.Invoke();
        GetComponent<TraumaInducer>().enabled = true;
        TouchCollider.enabled = true;
    }

    public void Fly()
    {
        animator.SetTrigger("startFlying");
        animator.SetLayerWeight(1, 0);
    }

    public void Talk()
    {
        animator.SetBool("isTalking", true);
    }

    public void StopTalking()
    {
        animator.SetBool("isTalking", false);
    }
}
