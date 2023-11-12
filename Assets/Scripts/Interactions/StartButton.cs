using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public float transitionSpeed;
    public string nextScene;
    public AudioSource clickSound;

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
        Initiate.Fade(nextScene, Color.black, transitionSpeed);
    }
}