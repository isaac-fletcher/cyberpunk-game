using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityDialogueScript : MonoBehaviour
{
	// Next scene to play
	public string nextScene;

	// Transition speed of scene change
	public float transitionSpeed;

	private void Start() {
		// Runs the Introduction Dialogue Sequence
		StartCoroutine(cityDialogueSequence());
	}

	IEnumerator cityDialogueSequence(){

		// Wait 2 seconds for smoother transition
		yield return new WaitForSecondsRealtime(7);

		// Elara
		DialogueController.instance.NewDialogueInstance("Woah. . .","character_elara");
		
		yield return new WaitForSecondsRealtime(7);

		// Aiden
		DialogueController.instance.NewDialogueInstance("You can say that again. . .", "character_aiden");

		yield return new WaitForSecondsRealtime(5);
		
		// Elara
		DialogueController.instance.NewDialogueInstance("It's so. . .", "character_elara");

		// Aiden
		DialogueController.instance.NewDialogueInstance("Fascinating?", "character_aiden");
		DialogueController.instance.NewDialogueInstance("I was thinking the same thing. . . and yet it looks so cold and worn out.", "character_aiden");

		yield return new WaitForSecondsRealtime(5);
		
		// Elara
		DialogueController.instance.NewDialogueInstance("I just know it isn't what they would've wanted. . . Mom and Dad. . .", "character_elara");
		DialogueController.instance.NewDialogueInstance("They worked so hard. . . why did it fall apart?", "character_elara");

		// Aiden
		DialogueController.instance.NewDialogueInstance("Uncle always talked about what the purpose of this place was. . . what people used to call Lumina.", "character_aiden");
		DialogueController.instance.NewDialogueInstance("A city meant to test risky, innovative engineering. It was bustling with life at some point.", "character_aiden");
		DialogueController.instance.NewDialogueInstance("Apparently, a bustling city equals a bustling economy and it becomes a point of interest for those Corpo jerks. . .", "character_aiden");
		DialogueController.instance.NewDialogueInstance("They took advantage of our parents. They had to have.", "character_aiden");
		DialogueController.instance.NewDialogueInstance("When our Aunt and Uncle explained it all to us, they said the agreement between the corporations and our parents was made in good faith, but I can't help but feel otherwise.", "character_aiden");

		// Elara
		DialogueController.instance.NewDialogueInstance("I just. . . . .", "character_elara");
		DialogueController.instance.NewDialogueInstance("I just. . . . . . . . . .", "character_elara");
		DialogueController.instance.NewDialogueInstance("This isn't what our parents wanted. . .", "character_elara");
		DialogueController.instance.NewDialogueInstance("They wanted the best for everyone. That's why they took on such a job in Lumina. So they could be innovative and helpful and outreaching and. . .", "character_elara");

		yield return new WaitForSecondsRealtime(178);

		// Fade to next 
		Initiate.Fade(nextScene, Color.black, transitionSpeed);
	}
}
