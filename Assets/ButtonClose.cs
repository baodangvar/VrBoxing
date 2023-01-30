using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClose : MonoBehaviour
{
    public GameObject model;
    public GameObject UI;
    [SerializeField]
    Image btnPet;

    public void OnButtonPet()
    {
        model.SetActive(true);
        UI.SetActive(false);
    }
}
