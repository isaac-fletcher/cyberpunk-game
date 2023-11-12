using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogueScript : MonoBehaviour
{
	// Controls the fade-out & fade-in speed
	// of the controller layout
	public float fadeSpeed;

	// Controls the "quietting" and "enloudening"
	// speed for walking sfx volume
	public float quiettingSpeed;

	// Controls max walking sfx volume
	public float maxWalkingVol;

	// The note image object
	public Image note;

	// The controller layout
	public Image controlLayout;

	// SFX for note reveal
	public AudioSource noteReveal;

	// SFX for lighter flicking on
	public AudioSource lighterFlicking;

	// SFX for walking
	public AudioSource walking;

	// Background music
	public AudioSource bgMusic;

	private void Start() {

		// Disables rendering the note and controller layout
		note.enabled = false;
		controlLayout.enabled = false;

		// Fades music in
		StartCoroutine(fadeInMusic());

		// Runs the Introduction Dialogue Sequence
		StartCoroutine(introDialogueSequence());
	}

	IEnumerator introDialogueSequence(){
		// Need to fade in first since CoRoutines happen simultaneously
		// (i.e., running fade-in CoRoutine the line before the fade-out
		// CoRoutine essentially just keeps the Image transparent)
		StartCoroutine(fadeInControllerLayout());

		// "Enloudens" walking sfx
		StartCoroutine(enloudeningWalking());

		// Wait 2 seconds for smoother transition
		yield return new WaitForSecondsRealtime(3);

		// Elara
		DialogueController.instance.NewDialogueInstance("Aiden. . . I can barely see!","character_elara");
		
		// Aiden
		DialogueController.instance.NewDialogueInstance("Shh! These sewers are echoey, we gotta make sure no one’s creepin’ on us, Elara.", "character_aiden");
		
		// Elara
		DialogueController.instance.NewDialogueInstance("Ugh. . . it’s not like we’re going to find radioactive rat mutants that understand English.", "character_elara");
		DialogueController.instance.NewDialogueInstance(". . .", "character_elara");
		DialogueController.instance.NewDialogueInstance("Are you even sure this is the right way?", "character_elara");

		// NOTE POPS ONTO SCREEN (AFTER 23 SECONDS IN REAL TIME)
		yield return new WaitForSecondsRealtime(20);
		// "Quiets" walking sfx
		StartCoroutine(quiettingWalking());

		yield return new WaitForSecondsRealtime(3);

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

		// After some time...
		yield return new WaitForSecondsRealtime(9);
		// "Enloudens" walking sfx
		StartCoroutine(enloudeningWalking());

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

		yield return new WaitForSecondsRealtime(126);
		// "Quiets" walking sfx
		StartCoroutine(quiettingWalking());

		// Aiden
		DialogueController.instance.NewDialogueInstance("Right, yeah, here ya go. Totally forgot I brought it.", "character_aiden");

		// After dialogue, play lighter flicking sfx and reveal controller layout
		yield return new WaitForSecondsRealtime(5);
		lighterFlicking.Play(); // Play lighter flicking sfx

		// Wait slightly longer before revealing controller layout
		yield return new WaitForSecondsRealtime(5);

		// Set controller layout transparency
		controlLayout.material.color = new Color(controlLayout.material.color.r, controlLayout.material.color.g, controlLayout.material.color.b, 0);
		controlLayout.enabled = true;

		// Runs fade-in for controller layout
		StartCoroutine(fadeInControllerLayout());

		// Wait for fade-out
		yield return new WaitForSecondsRealtime(7);

		// Runs fade-out for controller layout
		StartCoroutine(fadeOutControllerLayout());

		// Runs fade-out for background music
		StartCoroutine(fadeOutMusic());
	}

	// Runs controller layout fade-in
	IEnumerator fadeInControllerLayout(){
		// While the controller layout image's A-value (controls transparency)
		// is less than 1 (AKA, is not opaque)...
		while(controlLayout.material.color.a < 1){
			// Grab GameObject's color (easy for reading)
			Color objectColor = controlLayout.material.color;
			// Calculate fade amount based on fade speed, GameObject's current A-value, and deltaTime
			float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

			// Update temp GameObject color
			objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
			// Update GameObject
			controlLayout.material.color = objectColor;
			yield return null;
		}
	}

	// Runs controller layout fade-out
	IEnumerator fadeOutControllerLayout(){
		// While the controller layout image's A-value is greater than 0 (AKA, is not transparent)...
		while(controlLayout.material.color.a > 0){
			// Grab GameObject's color (easy for reading)
			Color objectColor = controlLayout.material.color;
			// Calculate fade amount based on fade speed, GameObject's current A-value, and deltaTime
			float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

			// Update temp GameObject color
			objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
			// Update GameObject
			controlLayout.material.color = objectColor;
			yield return null;
		}
	}

	// Slowly silences walking sfx
	IEnumerator quiettingWalking(){
		// While walking volume is not silent...
		while(walking.volume > 0){
			// Decrease volume based on current volume, the "quietting" speed, and deltaTime
			float delta = walking.volume - (quiettingSpeed * Time.deltaTime);

			if (delta < 0)
				walking.volume = 0;
			else 
				walking.volume = delta;

			yield return new WaitForSecondsRealtime(1);
		}
		yield return null;
	}

	// Slowly raises walking sfx
	IEnumerator enloudeningWalking(){
		// While walking volume is not max walking volume...
		while(walking.volume < maxWalkingVol){
			// Increase volume based on current volume, the "enloudening" speed, and deltaTime
			walking.volume = walking.volume + (quiettingSpeed * Time.deltaTime);
			if (walking.volume > maxWalkingVol)
				walking.volume = maxWalkingVol;
			yield return new WaitForSecondsRealtime(1);
		}
		yield return null;
	}

	// Fades in music
	IEnumerator fadeInMusic(){
		// While background music is not optimal volume...
		float maxVol = 0.075f;
		while(bgMusic.volume < maxVol){
			// Increase volume based on current volume, the "enloudening" speed (Set to 1.5), and deltaTime
			float speed = 1.0f;
			bgMusic.volume = bgMusic.volume + (speed * Time.deltaTime);
			if (bgMusic.volume > maxVol)
				bgMusic.volume = maxVol;
			yield return new WaitForSecondsRealtime(1);
		}
		yield return null;
	}

	// Fades out music
	IEnumerator fadeOutMusic(){
		// While background music is not silent...
		while(bgMusic.volume > 0){
			// Decrease volume based on current volume, the "quietting" speed, and deltaTime
			float speed = 3.5f;
			float delta = bgMusic.volume - (speed * Time.deltaTime);

			if (delta < 0)
				bgMusic.volume = 0;
			else 
				bgMusic.volume = delta;
			yield return new WaitForSecondsRealtime(1);
		}
		yield return null;
	}
}
