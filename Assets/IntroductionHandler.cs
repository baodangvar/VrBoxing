using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class IntroductionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LoginScreen = null;
    public GameObject FirstScreen = null;
    public GameObject SecondScreen = null;
    public GameObject StartScreen = null;
    public GameObject ModeScreen = null;


    void Start()
    {
       
    }

    public void MoveToFirstScreen()
    {
        LoginScreen.SetActive(false);
        FirstScreen.SetActive(true);
    }

    public void MoveToSecondScreen()
    {
        FirstScreen.SetActive(false);
        SecondScreen.SetActive(true);
    }

    public void MoveToStartScreen()
    {
        StartScreen.SetActive(true);
        SecondScreen.SetActive(false);
    }

    public void Movesence()
    {
        StartScreen.SetActive(false);
        ModeScreen.SetActive(true);
    }
}
