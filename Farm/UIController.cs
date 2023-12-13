using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allPanels = new List<GameObject>();
    private GameObject activePanel;

    private void Start()
    {
        if (allPanels.Count != 0)
            activePanel = allPanels[0];
    }

    private void ChangeActivePanel(GameObject newObject)
    {
        if (activePanel != null)
        {
            activePanel.SetActive(false);
        }
        activePanel = newObject;
        activePanel.SetActive(true);
        Debug.Log(activePanel.name);
    }    

    public void OpenPanel(int panelIndex)
    {
        ChangeActivePanel(allPanels[panelIndex]);
    }

}
