using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerCutsceneScript : MonoBehaviour
{
    // Next scene to play
	public string nextScene;

	// Transition speed of scene change
	public float transitionSpeed;

	// The black screen sprite
	public GameObject blackScreen;

	// The fade-in speed
	public float fadeSpeed;

	private void Start() {
		// Fades in
		StartCoroutine(fadeIn());

		// Runs the Introduction Dialogue Sequence
		StartCoroutine(towerDialogueSequence());
	}

    IEnumerator towerDialogueSequence(){
        // Wait 2 seconds for smoother transition
		yield return new WaitForSecondsRealtime(7);

        // Elara
		DialogueController.instance.NewDialogueInstance("Oh. . . we've been here for so long now the sun's coming out.","character_elara");

        // Aiden
		DialogueController.instance.NewDialogueInstance("It sure is.", "character_aiden");
		DialogueController.instance.NewDialogueInstance("And wouldn't you know it. . . there's the tower Mom and Dad worked everyday at.", "character_aiden");
        DialogueController.instance.NewDialogueInstance("\"The tower that powers Lumina!\" As the world once said.", "character_aiden");
        DialogueController.instance.NewDialogueInstance("I'm positive THAT is where we'll find information about our parents. After all, the whole city has been feeling like an invitation for us.", "character_aiden");

        // Elara
		DialogueController.instance.NewDialogueInstance("You think so? I know it's felt like a maze since we got here, but I'm not so sure we're finding what we want to find, Aiden. . .","character_elara");

        // Aiden
		DialogueController.instance.NewDialogueInstance("What d'ya mean? It's THE tower our parents worked at!", "character_aiden");

        // Elara
		DialogueController.instance.NewDialogueInstance("Yes, but. . .","character_elara");
        DialogueController.instance.NewDialogueInstance("We've been through the sewers and the streets and we've found no clues about our parents. Only that note has been our evidence of something here.","character_elara");
        DialogueController.instance.NewDialogueInstance("And yet,. . . . . . . there seems to be nothing here.","character_elara");

        // Aiden
		DialogueController.instance.NewDialogueInstance("Elara, relaaax. I think it's just your tiredness gettin' to ya.", "character_aiden");

        // Elara
		DialogueController.instance.NewDialogueInstance(". . . . . I guess so.","character_elara");
        DialogueController.instance.NewDialogueInstance("Well, no time to lose. I'm ready for bed at this point.","character_elara");

        yield return new WaitForSecondsRealtime(91);

		// Fade to next 
		Initiate.Fade(nextScene, Color.black, transitionSpeed);
    }
    
    // Runs fade-in for the cutscene
	IEnumerator fadeIn(){
		// While the black screen's sprite is not fully transparent...
		while(blackScreen.GetComponent<SpriteRenderer>().color.a > 0){
			// Grab GameObject's color (easy for reading)
			Color objectColor = blackScreen.GetComponent<SpriteRenderer>().color;
			// Calculate fade amount based on fade speed, GameObject's current A-value, and deltaTime
			float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

			// Update temp GameObject color
			objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
			// Update GameObject
			blackScreen.GetComponent<SpriteRenderer>().color = objectColor;
			yield return null;
		}
	}
}
