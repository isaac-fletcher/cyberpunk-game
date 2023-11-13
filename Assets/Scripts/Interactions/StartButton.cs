using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public float transitionSpeed;
    public string nextScene;
    public AudioSource clickSound;
    public AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchScene() 
    {
        clickSound.Play();

        StartCoroutine(fadeToNextScene());
    }

    // Fades to next scene
	IEnumerator fadeToNextScene(){
        while(backgroundMusic.volume > 0){
            float fadeOutSpeed = 0.1f;
            float delta = backgroundMusic.volume - (fadeOutSpeed * Time.deltaTime);

            if (delta < 0)
                backgroundMusic.volume = 0;
            else   
                backgroundMusic.volume = delta;
            
            yield return new WaitForEndOfFrame();
        }

		Initiate.Fade(nextScene, Color.black, transitionSpeed);
	}
}
