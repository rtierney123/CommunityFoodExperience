using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{

    public Tab[] tabs;

    public void expandTab(Tab selectedTab)
    {
        foreach(Tab tab in tabs)
        {
            tab.collapse();
        }
        selectedTab.expand();
    }

    public void collapseTab(Tab selectedTab)
    {
        selectedTab.collapse();
    }
}
