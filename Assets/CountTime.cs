using System.Collections;
using System.Collections.Generic;
using OVRSimpleJSON;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using WebSocketSharp;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using static LogIn;
using static ResultBoxing;


public class CountTime : MonoBehaviour
{
    public float levelToLoad = 1f;
    float currentTime = 0f;
    float startingTime = 15f;
    public GameObject model;
    public GameObject UI;
    public string namebot;
    public string idnameplayer;
    public TextMeshProUGUI username;
    public TextMeshProUGUI id;
    public GameObject popupFitingRoom;
    public GameObject popupRungem;
    public GameObject popupFinMatch;
    public GameObject popupCancelMatch;
    public GameObject popupResult;
    private WebSocket ws = null;
    public static string idResPvp = "";
    int checkShowPopUpReslust = 0;



    [SerializeField] Text countdownText;
    //pvp
    public class EmitMatchPvp
    {
        public string cmd { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public Player1 player1 { get; set; }
        public Player2 player2 { get; set; }
    }
    public class Player1
    {
        public string userName { get; set; }
        public string id { get; set; }
    }
    public class Player2
    {
        public string userName { get; set; }
        public string id { get; set; }
    }
    //
    public class resMatchPvpFighting
    {
        public string cmd { get; set; }
        public DataRes data { get; set; }
    }
    public class DataRes
    {
        public ExtenedData extenedData { get; set; }
    }
    public class ExtenedData
    {
        public FightingRoom fightingRoom { get; set; }
    }
    public class FightingRoom
    {
        public string id { get; set; }
    }
    //
    public class resMatchPvpLastest
    {
        public string cmd { get; set; }
        public DataResLastest data { get; set; }
    }
    public class DataResLastest
    {
        public ExtenedDatalastest extenedData { get; set; }
    }
    public class ExtenedDatalastest
    {
        public LastestMatch lastestMatch { get; set; }
    }
    public class LastestMatch
    {
        public string id { get; set; }
    }


    void Start()
    {
        StartCoroutine(InitSocket());

        if (BoxingMode.mode == "PVP")
        {
            createPvp();
            connectEmitApi();//xoa
        }
        currentTime = startingTime;
        namebot = LogIn.checkname;
        idnameplayer = LogIn.checkid;
        username.text = namebot;
        id.text = idnameplayer;
        
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (BoxingMode.mode == "PVE")
        {
            UI.SetActive(false);
            model.SetActive(true);
        }
        if (BoxingMode.mode == "PVP")
        {
            if (idResPvp.Length > 0 && checkShowPopUpReslust == 0)
            {
                popupResult.SetActive(true);//xóa
                checkShowPopUpReslust = 1;
                currentTime = -2;
            }
            else
            {
                if (currentTime > 0f) currentTime -= 1f * Time.deltaTime;
                countdownText.text = currentTime.ToString("f0");
                if (currentTime <= 0f && currentTime > -1f)
                {
                    popupFinMatch.SetActive(true);
                }
            }
            
        }
    }
    public void createPvp()
    {
        WsParam wsParam = new WsParam("MatchPvPBoxing");
        ws.Send(wsParam.getData());
        ws.OnMessage += (sender, e) =>
        {
            if (e.IsText)
            {
                Debug.Log(e.Data);
                EmitMatchPvp emitPvp = JsonConvert.DeserializeObject<EmitMatchPvp>(e.Data);
                if (emitPvp != null && emitPvp.cmd == "MatchPvPBoxing")
                {
                    if (emitPvp.data.player1 != null && emitPvp.data.player2 != null)
                    {
                        if (emitPvp.data.player1.id.Length > 0 && emitPvp.data.player2.id.Length > 0)
                        {
                            UI.SetActive(false);
                            model.SetActive(true);
                            currentTime = -2;
                        }
                    }
                }
                resMatchPvpFighting resPvpFighting = JsonConvert.DeserializeObject<resMatchPvpFighting>(e.Data);
                if (resPvpFighting != null && resPvpFighting.cmd == "MatchPvPBoxing")
                {
                    if (resPvpFighting.data.extenedData.fightingRoom != null && resPvpFighting.data.extenedData.fightingRoom.id.Length > 0)
                    {
                        popupFitingRoom.SetActive(true);
                        currentTime = -2;
                    }
                }
                resMatchPvpLastest resPvpLastest = JsonConvert.DeserializeObject<resMatchPvpLastest>(e.Data);
                if (resPvpLastest != null && resPvpLastest.cmd == "MatchPvPBoxing")
                {
                    if (resPvpLastest.data.extenedData.lastestMatch != null &&  resPvpLastest.data.extenedData.lastestMatch.id.Length > 0)
                    {
                        idResPvp = resPvpLastest.data.extenedData.lastestMatch.id;
                    }
                }
            }

        };
        ws.OnError += (sender, e) =>
        {
            Debug.Log(e.Message);
        };
    }
    public void connectEmitApi()
    {
        WsParam wsParam = new WsParam("GetUserInformation");
        ws.Send(wsParam.getData());
        ws.OnMessage += (sender, e) =>
        {
            if (e.IsText)
            {
                Debug.Log(e.Data);
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
