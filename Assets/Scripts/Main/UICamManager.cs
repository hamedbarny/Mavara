using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamManager : MonoBehaviour
{
    [SerializeField] private Camera UiCam;
    [SerializeField] private float offsetX, offsetZ;
    Vector3 dragOrigin;

    private void OnEnable()
    {
        UiCam.orthographicSize = 100;
        MapDragManager();
    }
    void Update()
    {
        //Zoom in n Out//
        if(Input.GetAxis("Mouse ScrollWheel")!= 0)
        {
            UiCam.orthographicSize = Mathf.Clamp(UiCam.orthographicSize - Input.mouseScrollDelta.y * 10, 30, 100);
            if (UiCam.orthographicSize > 30 && UiCam.orthographicSize < 100)
                MapDragManager();
        }

        //Drag n Move camera view//
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = UiCam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            MapDragManager();
        }
    }

    void MapDragManager()
    {
        Vector3 camPos;
        float sizeCam = UiCam.orthographicSize, rangeDrag;
        rangeDrag = (100 - sizeCam) * .8f;
        Vector3 difference = dragOrigin - UiCam.ScreenToWorldPoint(Input.mousePosition);
        //UiCam.transform.position += difference;
        camPos.y = 60;
        camPos.x = Mathf.Clamp(difference.x + UiCam.transform.position.x,
                                offsetX - rangeDrag, offsetX + rangeDrag);
        camPos.z = Mathf.Clamp(difference.z + UiCam.transform.position.z,
                                offsetZ - rangeDrag * 2, offsetZ + rangeDrag * 2);
        UiCam.transform.position = camPos;
    }


}
