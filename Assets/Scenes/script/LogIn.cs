using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using WebSocketSharp;
using System.Collections.Concurrent;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Unity.VisualScripting.Antlr3.Runtime;

public class MyInfo
{
    public static string SESSION = "";
    public static string TOKEN = "";
}

public class WsParam
{
    private Dictionary<string, object> data;

    public WsParam(string command)
    {
        data = new Dictionary<string, object>();
        data["cmd"] = command;
        pushParam("session", MyInfo.SESSION);
        //pushParam("token", MyInfo.TOKEN == string.Empty ? PlayerData.Instance.CoreUserData.accessToken : MyInfo.TOKEN);
    }

    public void pushParam(string key, object data)
    {
        this.data[key] = data;
    }

    public string getData()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(data);
    }
}
public class LogIn : MonoBehaviour
{
    public static string checkname;
    public static string checkid;
    public static string idPve = "";
    public static string name;
    public static string id;
    public static string dt=null;
    public static int x = 0;
    public static string dt1;
    public static int y = 0;


    public static string token;

    public string energy;
    [SerializeField]
    TMP_InputField  userNameInputField, passwordInputField;
    [SerializeField]
    Button signinPass;
    private WebSocket ws = null;
    private readonly ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();
    IEnumerator Example()
    {
        yield return new WaitForSecondsRealtime(5);
    }
    public partial class LoginResult
    {
        public string cmd;
        public int errorCode;
        public string message;
        public string accessToken;
        public string session;
        public string username;
        public string id;
    }
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
    public partial class LoginResult1
    {
        public string cmd;
        public int errorCode;
        public player player1;
        public player player2;
        public string id;
        public fightingRoom fightingRoom;
    }
    public partial class fightingRoom
    {
        public player player1;
        public player player2;
    }
    public partial class player
    {
        public string userName;
        public string id;
    }
    void Start()
    {
        StartCoroutine(Example());
        StartCoroutine(InitSocket());
    }

    void Update()
    {
        while (_actions.Count > 0)
        {
            if (_actions.TryDequeue(out var action))
            {
                action?.Invoke();
            }
        }

        if (ws == null)
        {
            return;
        }
        else
        {
            if (x == 0)
            {
                //signinPass.onClick.AddListener(() => HandleLogin());
               //Login("dieu.dev23@gmail.com", "123456");//xoa
                x++;
            }
        }
        if (dt != null)
        {
            if (x == 1)
            {
                ws.Close();
                StartCoroutine(InitSocket());
                x++;
            }

        }
        
    }

    public void handleLoginError()
    {
        Application.LoadLevel("MainMenu");
    }
    public void HandleLogin()
    {
        string username = userNameInputField.text;
        string password = passwordInputField.text;
        Login(username, password);

    }

    public void Login(string email, string password)
    {
        WsParam wsParam = new WsParam("SignIn"); 

        wsParam.pushParam("userName", email);
        wsParam.pushParam("password", password);
        ws.Send(wsParam.getData());
        ws.OnMessage += (sender, e) =>
        {
            if (e.IsText)
            {
                Debug.Log(e.Data);
                LoginResult rs = JsonUtility.FromJson<LoginResult>(e.Data);
                Debug.Log(rs.message);
                checkname = rs.username;
                checkid = rs.id;
                if (rs.errorCode == -1)
                {
                    Debug.Log("Login that bai");
                    _actions.Enqueue(() => handleLoginError());
                }
                if (rs.accessToken != null)
                {
                    dt = rs.accessToken;
                    token = dt;
                    Debug.Log("dang nhap thanh cong");
                }
    }

        };  
        ws.OnError += (sender, e) =>
        {
            Debug.Log(e.Message);
        };
    }
    public IEnumerator InitSocket()
    {
        ws = new WebSocket("wss://stg-game-api.runnow.io:21141?token="+dt);
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
        if (x == 2)
        {
            Debug.Log("thong tin nguoi choi");
            WsParam wsParam = new WsParam("MatchPvPBoxing");
            ws.Send(wsParam.getData());
            ws.OnMessage += (sender, e) =>
            {
                if (e.IsText)
                {
                    LoginResult1 rs = JsonUtility.FromJson<LoginResult1>(e.Data);
                    Debug.Log(e.Data);
                    energy = rs.ToString();
                    dt1 = rs.fightingRoom.ToString();
                    if (dt1.Length >= 0)
                    {
                        y = 1;
                    }
                    if(energy== "Not enough energy. Requires at least 3 energy!!!")
                    {
                        y = 2;
                    }
                }

            };
            ws.OnError += (sender, e) =>
            {
                Debug.Log(e.Message);
            };
        }
    }
    /*public void matchPVP()
    {
        Debug.Log("thong tin nguoi choi");
        WsParam wsParam = new WsParam("MatchPvPBoxing");
        ws.Send(wsParam.getData());
        ws.OnMessage += (sender, e) =>
        {
            if (e.IsText)
            {
                LoginResult1 rs = JsonUtility.FromJson<LoginResult1>(e.Data);
                Debug.Log(e.Data);
            }

        };
        ws.OnError += (sender, e) =>
        {
            Debug.Log(e.Message);
        };
    }*/
}
