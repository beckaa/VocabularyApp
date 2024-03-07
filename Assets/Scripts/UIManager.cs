using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void wantToQuit(GameObject objectA)
    {
        objectA.SetActive(true);
    }
    public void exit()
    {
        Application.Quit();
    }
}
