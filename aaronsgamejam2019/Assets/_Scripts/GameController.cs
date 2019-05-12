﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject NPCObj;

    public List<AudioClip> characterSounds  = new List<AudioClip>();
    public List<GameObject> gazeObjects     = new List<GameObject>();
    public List<GameObject> locations       = new List<GameObject>();

    private GameObject EarthHealer;
    private GameObject PeopleHealer;

    // Use this for initialization
    void Start () {
        // Create Characters

        EarthHealer = addNPC("EarthHealer", "EarthHealer", new Vector3(14.5f, 1.2f, -5.9f), 1);
        addAction("Speech 1", EarthHealer, false, true,  false, 1, null,            true, characterSounds[0]);
        addAction("Speech 2", EarthHealer, false, false, false, 3, gazeObjects[0],  true, characterSounds[1]);
        addAction("Speech 3", EarthHealer, false, false, false, 1, null,            true, characterSounds[2]);
        addAction("Speech 4", EarthHealer, false, false, false, 1, null,            true, characterSounds[3]);

        


    }

    public void gazeReceived(GameObject target)
    {
        if (target.name == "EarthHealer Bucket") {
            EarthHealer.GetComponent<NPCScript>().gazeReceived(target);
        }
    }


    GameObject addNPC(string characterName, string characterRole, Vector3 characterLocation, float movementSpeed)
    {
        var newNPC = Instantiate(NPCObj, characterLocation, Quaternion.identity);
        // These aren't used currently, but help us know who's who in the Editor.
        newNPC.GetComponent<NPCScript>().characterName = characterName;
        newNPC.GetComponent<NPCScript>().characterRole = characterRole;

        newNPC.GetComponent<NPCScript>().movementSpeed = movementSpeed; //NOT CURRENTLY IMPLEMENTED

        return newNPC;

    }

    void addAction(string actionName, GameObject targetNPC, bool isTriggered, bool isReady, bool isClicked, int triggerType, GameObject gazeObject, bool actionSpeak, AudioClip actionSound)
    {
        var newAction = new characterAction();
        newAction.actionName = actionName;
        newAction.isTriggered = isTriggered;
        newAction.isReady = isReady;
        newAction.isClicked = isClicked;

        newAction.gazeObject = gazeObject;

        /*
         *  0 = Instant (as soon as "isReady" is true, trigger next Update()
         *  1 = Click       // Trigger when clicked
         *  2 = Proximity   // Trigger by proximity
         *  3 = Gaze        // Trigger by gaze
         *  4 = Follow      // Trigger by following
         */
        newAction.triggerType = triggerType;

        // If true, NPC will activate sound during this action.
        newAction.actionSpeak = actionSpeak;

        // Check GameController in the Hierarchy to see what sounds are loaded in and find their index number.
        newAction.actionSound = actionSound;

        targetNPC.GetComponent<NPCScript>().characterActions.Add(newAction);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
