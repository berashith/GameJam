using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject NPCObj;

    public List<AudioClip> characterSounds = new List<AudioClip>();

    // Use this for initialization
    void Start () {
        // Create Characters
        var newNPC = Instantiate(NPCObj, new Vector3(14.5f, 1.2f, -5.9f), Quaternion.identity);
        newNPC.GetComponent<NPCScript>().characterName = "EarthHealer";
        newNPC.GetComponent<NPCScript>().characterRole = "EarthHealer";
        newNPC.GetComponent<NPCScript>().movementSpeed = 1; //NOT CURRENTLY IMPLEMENTED

        var newAction = new characterAction();
        newAction.isTriggered = false;
        newAction.isReady = true;
        newAction.isClicked = false;
        newAction.triggerType = 1;

        newAction.actionSound = characterSounds[1];

        newNPC.GetComponent<NPCScript>().characterActions.Add(newAction);
        
        // Name
        // Role
        // Location
        // triggered actions

    }

	// Update is called once per frame
	void Update () {
		
	}
}
