using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogueScript : MonoBehaviour
{
	// The note image object
	public Image note;
	public Image controlLayout;
	public AudioSource noteReveal;
	public AudioSource lighterFlicking;

	private void Start() {

		// Disables rendering the note and controller layout
		note.enabled = false;
		controlLayout.enabled = false;

		// Runs the Introduction Dialogue Sequence
		StartCoroutine(introDialogueSequence());
	}

	IEnumerator introDialogueSequence(){
		// Wait 2 seconds for smoother transition
		yield return new WaitForSecondsRealtime(2);

		// Elara
		DialogueController.instance.NewDialogueInstance("Aiden. . . I can barely see!","character_elara");
		
		// Aiden
		DialogueController.instance.NewDialogueInstance("Shh! These sewers are echoey, we gotta make sure no one’s creepin’ on us, Elara.", "character_aiden");
		
		// Elara
		DialogueController.instance.NewDialogueInstance("Ugh. . . it’s not like we’re going to find radioactive rat mutants that understand English.", "character_elara");
		DialogueController.instance.NewDialogueInstance(". . .", "character_elara");
		DialogueController.instance.NewDialogueInstance("Are you even sure this is the right way?", "character_elara");

		// NOTE POPS ONTO SCREEN (AFTER 22 SECONDS IN REAL TIME)
		yield return new WaitForSecondsRealtime(22);
		noteReveal.Play(); 	// Plays note reveal SFX
		note.enabled = true; // Image of note is displayed once enabled

		// Aiden
		DialogueController.instance.NewDialogueInstance("The layout is matchin’ the note perfectly.", "character_aiden");

		//Elara
		DialogueController.instance.NewDialogueInstance("How can you even tell? It’s so dark!", "character_elara");

		// After some time, disable note image
		yield return new WaitForSecondsRealtime(7);
		noteReveal.Play(); // Plays note reveal SFX
		note.enabled = false; // Image of note undisplayed

		// Aiden
		DialogueController.instance.NewDialogueInstance("What’d I say about being quiet!?", "character_aiden");

		// Elara
		DialogueController.instance.NewDialogueInstance("You said that pretty loud yourself.", "character_elara");

		// Aiden
		DialogueController.instance.NewDialogueInstance("Urgh. . .", "character_aiden");
		DialogueController.instance.NewDialogueInstance("I guess it doesn’t matter how loud we are in here.", "character_aiden");
		DialogueController.instance.NewDialogueInstance("To be honest, I’m ready to see what Mom and Dad left for us in these sewers under Lumina.", "character_aiden");

		// Elara
		DialogueController.instance.NewDialogueInstance("Yeah. . .", "character_elara");
		DialogueController.instance.NewDialogueInstance("We’ve had that note for years now, but it was only yesterday we figured out that the sewer layout matched the weird lines scribbled on the note pretty well.", "character_elara");
		DialogueController.instance.NewDialogueInstance("I’m just hoping you know how to read a map.", "character_elara");


		// Aiden
		DialogueController.instance.NewDialogueInstance("Right. . .", "character_aiden");
		DialogueController.instance.NewDialogueInstance("How many years has it been now? A little under a decade or somethin’, right?", "character_aiden");
		DialogueController.instance.NewDialogueInstance("It’s surprising Lumina is looking the way it is now according to those pictures Auntie and Uncle showed us, but after what happened I guess it’s to be expected.", "character_aiden");

		// Elara
		DialogueController.instance.NewDialogueInstance("9 years. It’s been about 9 years and a few months. . .", "character_elara");
		DialogueController.instance.NewDialogueInstance("I only keep track of the anniversary, though. I don’t want to remind myself about what’s happened that often.", "character_elara");

		// Aiden
		DialogueController.instance.NewDialogueInstance("Elara, you say that like our parents died, but we have no way of knowing that.", "character_aiden");
		DialogueController.instance.NewDialogueInstance("As far as we know, they disappeared ‘cause of the pressure put on them by those corporation guys Uncle won’t stop talking about.", "character_aiden");

		// Elara
		DialogueController.instance.NewDialogueInstance("I didn’t say that! I’m just saying I miss them, Aiden. . .", "character_elara");

		// Aiden
		DialogueController.instance.NewDialogueInstance("I miss them, too, Elara. Here’s to hoping we find them.", "character_aiden");
		DialogueController.instance.NewDialogueInstance("Aside from them, though, I can’t say I’m not excited to explore an abandoned city that was used for “testing ambitious engineering revelations” (as Auntie put it).", "character_aiden");

		// Elara
		DialogueController.instance.NewDialogueInstance("Haha, you’re funny, Aiden. I don’t know how you can sound so carefree saying that, but it makes me feel better somehow.", "character_elara");
		DialogueController.instance.NewDialogueInstance("Hey, wait, don’t you have an extra lighter?", "character_elara");

		// Aiden
		DialogueController.instance.NewDialogueInstance("Right, yeah, here ya go. Totally forgot I brought it.", "character_aiden");

		// After dialogue, play lighter flicking sfx and reveal controller layout
		yield return new WaitForSecondsRealtime(140);
		lighterFlicking.Play(); // Play lighter flicking sfx

		// Wait slightly longer before revealing controller layout
		yield return new WaitForSecondsRealtime(5);
		controlLayout.enabled = true; // Animation of controller layout displayed

		// Wait for 6 seconds before undisplaying controller layout
		yield return new WaitForSecondsRealtime(7);
		controlLayout.enabled = false; // Animation of controller layout undisplayed
	}
}
