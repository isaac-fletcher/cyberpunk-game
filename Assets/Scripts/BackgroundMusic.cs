using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    // What scene to stop background music
    public string whatSceneToStop;

    // The background music
    private AudioSource bgMusic;

    // Start is called before the first frame update
    void Start()
    {
        bgMusic = gameObject.GetComponent<AudioSource>();
        // Fades-in music
        StartCoroutine(fadeInMusic());
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals(whatSceneToStop)){
            StartCoroutine(fadeOutMusic());
        }
    }

    // Ensures object is not destroyed on load
    private void Awake(){
        DontDestroyOnLoad(gameObject);
    }

    // Fades in music
    IEnumerator fadeInMusic(){                  
    	// While background music is not optimal volume...                  
    	float maxVol = 0.075f;                  
    	while(bgMusic.volume < maxVol){                 
    		// Increase volume based on current volume, the "enloudening" speed (Set to 1.0), and deltaTime                 
    		float speed = 0.01f;                 
    		bgMusic.volume = bgMusic.volume + (speed * Time.deltaTime);                 
    		if (bgMusic.volume > maxVol)                    
    			bgMusic.volume = maxVol;                    
    		yield return new WaitForEndOfFrame();                
    	}                   
    	yield return null;                  
    }                   
                    
    // Fades out music                  
    IEnumerator fadeOutMusic(){     
        yield return new WaitForSecondsRealtime(2);            
    	// While background music is not silent...                  
    	while(bgMusic.volume > 0){                  
    		// Decrease volume based on current volume, the "quietting" speed, and deltaTime                    
    		float speed = 0.0001f;
			float delta = bgMusic.volume - (speed * Time.deltaTime);

			if (delta < 0)
				bgMusic.volume = 0;
			else 
				bgMusic.volume = delta;
			yield return new WaitForEndOfFrame();
		}
		yield return null;
	}
}
