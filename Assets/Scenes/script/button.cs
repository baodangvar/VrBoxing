using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject cancel;
    public void bt1()
    {
        cancel.SetActive(true);
    }
    public void bt2()
    {
        Application.LoadLevel("Main Menu");
    }
    public void bt3()
    {
        cancel.SetActive(false);
    }
    public void bt4()
    {
        Application.LoadLevel("SampleScene");
    }
}
