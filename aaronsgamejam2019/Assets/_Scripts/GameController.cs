using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject NPCObj;
    public GameObject Player;

    public List<AudioClip> characterSounds  = new List<AudioClip>();
    public List<GameObject> gazeObjects     = new List<GameObject>();
    public List<GameObject> locations       = new List<GameObject>();

    private GameObject EarthHealer;
    private GameObject PeopleHealer;
    private GameObject ChildGuide;

    // Use this for initialization
    void Start () {
        // Create Characters

        EarthHealer = addNPC("EarthHealer", "EarthHealer", new Vector3(4.1f, 1.2f, -97f), 1);
        addAction("Good Choice",         EarthHealer,  false, true,  false, 1, null,            true,  characterSounds[0], false, null);
        addAction("Bottle Bucket",       EarthHealer,  false, false, false, 3, gazeObjects[0],  true,  characterSounds[1], false, null);
        addAction("Go To Soil Bucket",   EarthHealer,  false, false, false, 0, null,            false, null,               true,  locations[0]);
        addAction("Look in Soil Bucket", EarthHealer,  false, false, false, 4, null,            true,  characterSounds[2], false, null);
        addAction("Soil Bucket Speech",  EarthHealer,  false, false, false, 1, null,            true,  characterSounds[3], false, null);

        PeopleHealer = addNPC("PeopleHealer", "PeopleHealer", new Vector3(6.5f, 1.2f, -5.9f), 1);
        addAction("Surprised",           PeopleHealer, false, true,  false,  1, null, true, characterSounds[4], false, null);
        addAction("Screen Bucket",       PeopleHealer, false, false, false,  3, gazeObjects[0], true, characterSounds[5], false, null);

        PeopleHealer = addNPC("ChildGuide", "ChildGuide", new Vector3(2.7f, 1.2f, -108f), 1);
        PeopleHealer.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        addAction("Follow", ChildGuide,         false, true,  false, 1, null, false, null, false, null);
        addAction("Go To Healer", ChildGuide,   false, false, false, 0, null, false, null, true,  locations[1]);


    }

    public void gazeReceived(GameObject target)
    {
        if (target.name == "EarthHealer WaterBottle Bucket") {
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

    void addAction(string actionName, GameObject targetNPC, bool isTriggered, bool isReady, bool isClicked, int triggerType, GameObject gazeObject, bool actionSpeak, AudioClip actionSound, bool actionLead, GameObject actionLocation)
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

        newAction.actionLead = actionLead;
        newAction.actionLocation = actionLocation;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
