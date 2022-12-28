///summary///
///this is to handle map and map cells
///open n close
///cell info


using UnityEngine;
using TMPro;

public class CellHandler : MonoBehaviour
{
    //Cell logo Material for selected cell purposes;
    public static Material matLogo,prevMatLogo;

    [SerializeField] private GameObject cell, panelMap;
    [SerializeField] private Material matHovered, matDefault, matSelect;
    [SerializeField] private TextMeshProUGUI txtOwner, txtArea, txtCoOrd;
    private Vector2 coOrd;
    Color selectedColor = new Color(.93f,.54f,.1f);



    private void OnEnable()
    {
        cell.GetComponent<MeshRenderer>().material = matDefault;
        matLogo = new Material(matDefault);
        
    }
    private void OnDisable()
    {
        matLogo.SetColor("_EmisColor", Color.gray);
    }

    private void OnMouseDown()
    {
        prevMatLogo = matLogo;
        matLogo = this.transform.Find("CellImage").GetComponent<MeshRenderer>().material;
        matLogo.SetColor("_EmisColor", selectedColor);
        prevMatLogo.SetColor("_EmisColor", Color.gray);
        MapManager.teleportPos = this.transform.Find("TpPoint").position;
        cell.GetComponent<MeshRenderer>().material = matSelect;
        LandCoOrd();
        PanelTextHandler();
    }
    private void OnMouseEnter()
    {
        cell.GetComponent<MeshRenderer>().material = matHovered;
    }
    private void OnMouseExit()
    {
        cell.GetComponent<MeshRenderer>().material = matDefault;
    }


    void LandCoOrd()
    {
        float x, z;
        x = this.transform.position.x - 9;
        z = this.transform.position.z - 275;
        x = x / 6;
        z = z / 6;
        int xx = (int)x;
        int yy = (int)z;
        coOrd = new Vector2(xx, yy);
    }

    void PanelTextHandler()
    {
        if (!panelMap.activeInHierarchy) panelMap.SetActive(true);
        txtOwner.text = this.name;
        txtArea.text = (this.transform.localScale.x * this.transform.localScale.z).ToString() +" Block(s)";
        txtCoOrd.text = "CoOrd: (" + coOrd.x + "," + coOrd.y+")";
    }

}
