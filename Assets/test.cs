using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    TMP_InputField userNameInputField, passwordInputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void read(string s)
    {
        userNameInputField.text = s;
        Debug.Log(userNameInputField);
    }

}
