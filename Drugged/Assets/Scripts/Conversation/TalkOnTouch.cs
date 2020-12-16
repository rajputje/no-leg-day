using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public abstract class TalkOnTouch : InteractOnTouch
{
    [Tooltip("Dialog that signifies the player can start a conversation with the object. For eg. \"...\"")]
    [SerializeField] protected NPCConversation PromptConversation = null;
    protected NPCConversation CurrentConversation = null;

    [Tooltip("Offset from speaker's transform position")]
    [SerializeField] protected Vector3 DialogBoxOffset = new Vector3(0, 0);

    protected abstract NPCConversation GetNextConversation();

    protected void Start()
    {
        LoadAllConversations();
    }

    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (!ConversationManager.Instance.IsConversationActive && 1 << collision.gameObject.layer == InteractionTriggeringLayer)
        {
            StartConversation(PromptConversation);
        }
    }

    /// <summary>
    /// Inheriting members should use this method to start a conversation
    /// </summary>
    /// <param name="conversation"></param>
    protected void StartConversation(NPCConversation conversation)
    {
        CurrentConversation = conversation;
        ConversationManager.Instance.transform.position = transform.position + DialogBoxOffset;
        ConversationManager.Instance.StartConversation(conversation);
    }

    /// <summary>
    /// Inheriting members should use this method to end conversations
    /// </summary>
    /// <param name="conversation"></param>
    protected void EndConversation()
    {
        CurrentConversation = null;
        ConversationManager.Instance.EndConversation();
    }

    protected new void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (CurrentConversation == PromptConversation && !IsTakingInput)
        {
            EndConversation();
        }
    }

    protected override void Interact()
    {
        if (ConversationManager.Instance.IsConversationActive && CurrentConversation != PromptConversation)
            ConversationManager.Instance.ContinueDialog();
        else
            StartConversation(GetNextConversation());
    }

    protected abstract void LoadAllConversations();
}
