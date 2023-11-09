using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Interactions
{
    public class Button : MonoBehaviour
    {
        public GameObject door1;
        public GameObject door2;

        private bool door1StayOpen;

        // Start is called before the first frame update
        void Start()
        {
            door1StayOpen = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (door1StayOpen)
                door1.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D c)
        {
            if (c.gameObject.CompareTag("Player"))
            {
                door2.SetActive(false);
                door1StayOpen = true;
            }
            else
            {
                door2.SetActive(true);
            }
        }
    }
}
