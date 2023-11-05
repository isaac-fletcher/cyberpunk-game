using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitDoor : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private bool p1out;
    private bool p2out;

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
        if (c.gameObject.name == "Player1")
        {
            player1.SetActive(false);
            p1out = true;
        }
        else if (c.gameObject.name == "Player2")
        {
            player2.SetActive(false);
            p2out = true;        
        }

        if (p1out && p2out)
            Application.Quit(); // will use SceneManager to change area in the future
    }
}
