using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject NPCObj;

    public List<AudioClip> characterSounds = new List<AudioClip>();

    // Use this for initialization
    void Start () {
        // Create Characters

        // Set Vector3 to the coordinates you'd like them to be at.
        var newNPC = Instantiate(NPCObj, new Vector3(14.5f, 1.2f, -5.9f), Quaternion.identity);
        // These aren't used currently, but help us know who's who in the Editor.
        newNPC.GetComponent<NPCScript>().characterName = "EarthHealer";
        newNPC.GetComponent<NPCScript>().characterRole = "EarthHealer";


        newNPC.GetComponent<NPCScript>().movementSpeed = 1; //NOT CURRENTLY IMPLEMENTED

        // You can make as many actions as you like for the person. These consist of triggers, which when met cause some actions.
        var newAction = new characterAction();
        newAction.isTriggered = false;
        newAction.isReady = true;
        newAction.isClicked = false;
        newAction.triggerType = 1; // Defined in NPCScript

        newAction.actionSpeak = true;
        // Check GameController in the Hierarchy to see what sounds are loaded in and find their index number.
        newAction.actionSound = characterSounds[1];


        newNPC.GetComponent<NPCScript>().characterActions.Add(newAction);

    }

	// Update is called once per frame
	void Update () {
		
	}
}
