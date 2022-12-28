using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrlOpenner : MonoBehaviour
{
    [SerializeField] private Collider playerCol;
    [SerializeField] private string daURL;
    [SerializeField] private GameObject tipCanvas;

    private bool isEntered = false, firstEnter = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCol)
        {
            tipCanvas.GetComponent<Animator>().Play("CanvasAnimation");
            isEntered = true;
            firstEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerCol)
        {
            tipCanvas.GetComponent<Animator>().Play("CanvasAnimationOff");
            isEntered = false;
        }
    }


    private void Update()
    {
        if (isEntered && firstEnter)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                Application.OpenURL(daURL);
                firstEnter = false;
            }
        }
    }
}
