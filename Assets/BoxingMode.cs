using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoxingMode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Image btnStartMode;
    [SerializeField]
    Image btnChoosePve;
    [SerializeField]
    Image btnChoosePvp;
    [SerializeField]
    GameObject modePve;
    [SerializeField]
    GameObject modePvp;


    public void OnButtonStartMode()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnButtonChoosePve()
    {
        modePve.SetActive(true);
        modePvp.SetActive(false);
    }
    public void OnButtonChoosePvp()
    {
        modePve.SetActive(false);
        modePvp.SetActive(true);
    }

}
