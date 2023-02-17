using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Newtonsoft.Json;

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
    private WebSocket ws = null;

    public static string mode ="";
    //pve
    [Serializable]
    public partial class ResponseMatchPve
    {
        public data4 data;
        public string cmd;
    }
    [Serializable]
    public partial class data4
    {
        public string id;
    }
    
    void Start()
    {
        StartCoroutine(InitSocket());
        //createPve();//xóa test
        //mode = "PVE";//xóa
         //SceneManager.LoadScene("SampleScene");//xoa
        //mode = "PVP";//xoa

    }
    public void OnButtonStartMode()
    {
        if(mode =="PVE") createPve();
        if (mode == "PVP")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void OnButtonChoosePve()
    {
        modePve.SetActive(true);
        modePvp.SetActive(false);
        mode = "PVE";
    }
    public void OnButtonChoosePvp()
    {
        modePve.SetActive(false);
        modePvp.SetActive(true);
        mode = "PVP";
    }
    public void createPve()
    {
        WsParam wsParam = new WsParam("CreateNPCMatchBoxing");
        ws.Send(wsParam.getData());
        ws.OnMessage += (sender, e) =>
        {
            if (e.IsText)
            {
                Debug.Log(e.Data);
                ResponseMatchPve rs = JsonUtility.FromJson<ResponseMatchPve>(e.Data);
                LogIn.idPve = rs.data.id;
                print("idPve " + LogIn.idPve);
                if (LogIn.idPve.Length>0) SceneManager.LoadScene("SampleScene");
            }

        };
        ws.OnError += (sender, e) =>
        {
            Debug.Log(e.Message);
        };
    }
    
    public IEnumerator InitSocket()
    {
        ws = new WebSocket("wss://stg-game-api.runnow.io:21141?token=" + LogIn.token);
        ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
        ws.Connect();
        Debug.Log("Initial State : " + ws.ReadyState);
        ws.OnMessage += (sender, e) =>
        {

        };
        ws.OnError += (sender, e) =>
        {
            Debug.Log(e.Message);
        };
        yield return null;

    }

}
