using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAction
{
    public bool isTriggered = false;
    public bool isReady = false;
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
            Debug.Log("CLICKED");
            checkTriggers();
        }
    }

    // Run through all triggers in list. If one is ready but not triggered, and the trigger condition is met, trigger it.
    void checkTriggers()
    {
        foreach (var actionItem in characterActions)
        {
            if (actionItem.isReady && !actionItem.isTriggered)
            {
                if (actionItem.triggerType == 1 && clicked == true)
                {
                    actionItem.isTriggered = true;
                    Debug.Log("TRIGGERED");

                    // Is it an audio trigger? If so, trigger it
                    NPCVoice.clip = actionItem.actionSound;
                    NPCVoice.Play(0);

                }
            }
        }
    }
}
