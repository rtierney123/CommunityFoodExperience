using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    public GameObject expandableContent;
    public GameObject expandButton;
    public GameObject collapseButton;
    public void expand()
    {
        expandableContent.SetActive(true);
        expandButton.SetActive(false);
        collapseButton.SetActive(true);
    }

    public void collapse()
    {
        expandableContent.SetActive(false);
        expandButton.SetActive(true);
        collapseButton.SetActive(false);
    }
}
