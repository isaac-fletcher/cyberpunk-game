using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitDoor : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private bool p1out;
    private bool p2out;

    public float transitionSpeed;
    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        p1out = false;
        p2out = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name == player1.name)
        {
            c.gameObject.SetActive(false);
            p1out = true;
        }
        if (c.gameObject.name == player2.name)
        {
            c.gameObject.SetActive(false);
            p2out = true;        
        }

        if (p1out && p2out)
            Initiate.Fade(nextScene, Color.black, transitionSpeed);
            //Application.Quit(); // will use SceneManager to change area in the future
    }
}
