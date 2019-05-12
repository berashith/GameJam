using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject NPCObj;

    public List<AudioClip> characterSounds = new List<AudioClip>();

    // Use this for initialization
    void Start () {
        // Create Characters

        var EarthHealer = addNPC("EarthHealer", "EarthHealer", new Vector3(14.5f, 1.2f, -5.9f), 1);
        addAction(EarthHealer, false, true,  false, 1, true, characterSounds[0]);
        addAction(EarthHealer, false, false, false, 1, true, characterSounds[1]);
        addAction(EarthHealer, false, false, false, 1, true, characterSounds[2]);
        addAction(EarthHealer, false, false, false, 1, true, characterSounds[3]);

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

    void addAction(GameObject targetNPC, bool isTriggered, bool isReady, bool isClicked, int triggerType, bool actionSpeak, AudioClip actionSound)
    {
        var newAction = new characterAction();
        newAction.isTriggered = isTriggered;
        newAction.isReady = isReady;
        newAction.isClicked = isClicked;

        /*
         *  0 = Instant (as soon as "isReady" is true, trigger next Update()
         *  1 = Click       // Trigger when clicked
         *  2 = Proximity   // Trigger by proximity
         *  3 = Gaze        // Trigger by gaze
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
