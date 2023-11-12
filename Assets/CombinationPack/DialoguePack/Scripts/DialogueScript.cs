using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDialogueScript : MonoBehaviour
{
    public Dialogue dialogue;
    public string characterName;
    //public string singleLine;       // Elara
    public string[] multipleLines = {"Aiden… I can barely see!",
                                    // Aiden
                                    "Shh! These sewers are echoey, we gotta make sure no one’s creepin’ on us, Elara.", 
                                    // Elara
                                    "Ugh… it’s not like we’re going to find radioactive rat mutants that understand English.",
                                    ". . .",
                                    "Are you even sure this is the right way?",
                                    // Aiden
                                    "The layout is matchin’ the note perfectly.",
                                    // Elara
                                    "How can you even tell? It’s so dark!",
                                    // Aiden
                                    "What’d I say about being quiet!?",
                                    // Elara
                                    "You said that pretty loud yourself.",
                                    // Aiden
                                    "Urgh…",
                                    "I guess it doesn’t matter how loud we are in here.",
                                    "To be honest, I’m ready to see what Mom and Dad left for us in these sewers under Lumina.",
                                    // Elara
                                    "Yeah…",
                                    "We’ve had that note for years now, but it was only yesterday we figured out that the sewer layout matched the weird lines scribbled on the note pretty well.",
                                    "I’m just hoping you know how to read a map.",
                                    // Aiden
                                    "Right…",
                                    "How many years has it been now? A little under a decade or somethin’, right?",
                                    "It’s surprising Lumina is looking the way it is now according to those pictures Auntie and Uncle showed us, but after what happened I guess it’s to be expected.",
                                    // Elara
                                    "9 years. It’s been about 9 years and a few months…",
                                    "I only keep track of the anniversary, though. I don’t want to remind myself about what’s happened that often.",
                                    // Aiden
                                    "Elara, you say that like our parents died, but we have no way of knowing that.",
                                    "As far as we know, they disappeared ‘cause of the pressure put on them by those corporation guys Uncle won’t stop talking about.",
                                    // Elara
                                    "I didn’t say that! I’m just saying I miss them, Aiden…",
                                    // Aiden
                                    "I miss them, too, Elara. Here’s to hoping we find them.",
                                    "Aside from them, though, I can’t say I’m not excited to explore an abandoned city that was used for “testing ambitious engineering revelations” (as Auntie put it).",
                                    // Elara
                                    "Haha, you’re funny, Aiden. I don’t know how you can sound so carefree saying that, but it makes me feel better somehow.",
                                    "Hey, wait, don’t you have an extra lighter?",
                                    // Aiden
                                    "Right, yeah, here ya go. Totally forgot I brought it."};
    public float delay = 4;
    public float typeSpeed = 0.04f;

    private void Start()
    {
        Invoke("Speech", .5f);
    }

    public void Speech()
    {
        dialogue.typingSpeed = typeSpeed;

        dialogue.Say(multipleLines, characterName, delay); 
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dialogue.Skip();
        }
        if (Input.GetMouseButtonDown(1))
        {
            dialogue.Clear();
        }
    }
}
