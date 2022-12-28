using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static Vector3 teleportPos , landPos;
    [SerializeField] private GameObject mapUI, mapCanvas, camUI, camPlayer, tipCanvas;
    [SerializeField] private EventSystem eventS;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!mapUI.activeInHierarchy)
                MapOpener();
            else if (mapUI.activeInHierarchy)
                MapCloser();
        }
    }
    public void MapBackButton()
    {
        MapCloser();
    }

    public void MapTeleportButton()
    {
        teleportPos.y = 17;
        MapCloser();
        camPlayer.transform.position = teleportPos;
        eventS.SetSelectedGameObject(null);
    }

    void MapCloser()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camPlayer.SetActive(true);
        camUI.SetActive(false);
        mapUI.SetActive(false);
    }
    void MapOpener()
    {
        tipCanvas.GetComponent<Animator>().Play("CanvasAnimationOff");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        camPlayer.SetActive(false);
        camUI.SetActive(true);
        mapUI.SetActive(true);
    }
}
