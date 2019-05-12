using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAction
{
    public bool isReady = false;        // Can't trigger until ready
    public bool isTriggered = false;
    public bool isFinished = false; 

    public bool isClicked = false;
    public AudioClip actionSound;
    public Vector3 navPoint;

    /*
     *  0 = Instant (as soon as "isReady" is true, trigger next Update()
     *  1 = Click       // Trigger when clicked
     *  2 = Proximity   // Trigger by proximity
     *  3 = Gaze        // Trigger by gaze
     */

    public int triggerType;

    public bool actionSpeak = false;
    public bool actionFollow = false;
    public bool actionLead = false;
}

public class NPCScript : MonoBehaviour {

    public string characterName;
    public string characterRole;
    public float movementSpeed;
    public AudioSource NPCVoice;


    public bool clicked;

    public List<characterAction> characterActions = new List<characterAction>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        checkTriggers();
	}

    public void clickNPC()
    {
        if (clicked == false)
        {
            clicked = true;
            checkTriggers();
            clicked = false;

        }
    }

    // Run through all triggers in list. If one is ready but not triggered, and the trigger condition is met, trigger it.
    void checkTriggers()
    {
        // NOTE current system is linear: when one action is triggered, it readies the next action. Ideally, have a list of actions that get 'readied' upon activation / completion.

        int actionIndex = 0; 
        foreach (var actionItem in characterActions)
        {
            if (actionItem.actionSpeak && NPCVoice.isPlaying) {
                return;
            }
            if (actionItem.isReady && !actionItem.isTriggered && !actionItem.isFinished)
            {
                if (actionItem.triggerType == 1 && clicked == true)
                {
                    actionItem.isReady = false;
                    clicked = false;
                    actionItem.isTriggered = true;

                    // Is it an audio trigger? If so, trigger it
                    if (actionItem.actionSpeak)
                    {
                        // This should really be a coroutine. Ideally we should detect when it finished, so we can activate actions at start or end of audio.
                        NPCVoice.clip = actionItem.actionSound;
                        NPCVoice.Play(0);
                    }
                }

                if (characterActions.Count > actionIndex + 1)
                {
                    characterActions[actionIndex + 1].isReady = true;
                }
            }
            if (characterActions.Count > actionIndex + 1)
            {
                actionIndex++;
            }
        }
    }
}
