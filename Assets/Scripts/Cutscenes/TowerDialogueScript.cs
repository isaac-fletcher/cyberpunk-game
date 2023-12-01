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
		DialogueController.instance.NewDialogueInstance("Woah. . .","character_elara");
		
		yield return new WaitForSecondsRealtime(7);

		// Aiden
		DialogueController.instance.NewDialogueInstance("You can say that again. . .", "character_aiden");


        yield return new WaitForSecondsRealtime(178);

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
