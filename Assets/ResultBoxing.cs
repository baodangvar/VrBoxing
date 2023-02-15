using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using OVRSimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;
using static LogIn;

public class ResultBoxing : MonoBehaviour
{
    [SerializeField]
    Image BtnHome;
    [SerializeField]
    Image BtnShare;

    public TextMeshProUGUI consumedEnergy;
    public TextMeshProUGUI consumableDurability;
    public TextMeshProUGUI rungemEarned;
    public TextMeshProUGUI isWinner;
    private WebSocket ws = null;
    public GameObject win;
    public GameObject low;
    float countTimecallPVE = 4;

    public class ResponseMatchPveRes
    {
        public string cmd { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public ExtendedData extendedData { get; set; }
    }

    public class ExtendedData
    {
        public string winRate { get; set; }
        public Boxingresult boxingResult { get; set; }
    }


    public class Boxingresult
    {
        public string consumedEnergy { get; set; }
        public string consumableDurability { get; set; }
        public string consumableLifetime { get; set; }
        public string rungemEarned { get; set; }
        public string isWinner { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitSocket());
    }
    // Update is called once per frame
    void Update()
    {
        if (countTimecallPVE > 0) countTimecallPVE -= 1f * Time.deltaTime;
        print("AAAAAAAA");
        print("AAAAAAAA" + Maria.stthqplayer1);
        print("AAAAAAAA" +LogIn.idPve.Length);

        if (Maria.stthqplayer1 == "win" && LogIn.idPve.Length > 0)
        {
            print("AAAAAAAA truoc " + countTimecallPVE);
            if (countTimecallPVE < 1 && countTimecallPVE >0)
            {
                ResultPve(LogIn.idPve, true, true);
                countTimecallPVE = -1;
                print("AAAAAAAA sau" + countTimecallPVE);
            }
        }
    }
    public void OnclickBackHome()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ResultPve(string roomID,bool isWinnercall, bool isDraw)
    {
        print("BBBBBBBB");
        var blockData = new Dictionary<string, object>();
        blockData["roomId"] = roomID;
        blockData["isWinner"] = isWinnercall;
        blockData["isDraw"] = isDraw;

        WsParam wsParam = new WsParam("UpdateNPCMatchBoxing");
        wsParam.pushParam("params", blockData);
        ws.Send(wsParam.getData());
        ws.OnMessage += (sender, e) =>
        {
            if (e.IsText)
            {
                print("CCCCCC");

                Debug.Log(e.Data);
                ResponseMatchPveRes responseMatchPveRes = JsonConvert.DeserializeObject<ResponseMatchPveRes>(e.Data);
                var resPerson = responseMatchPveRes.data;

                consumedEnergy.text ="- "+ resPerson.extendedData.boxingResult.consumedEnergy;
                consumableDurability.text ="x " + resPerson.extendedData.boxingResult.consumableDurability;
                rungemEarned.text = "+ "+ resPerson.extendedData.boxingResult.rungemEarned;
                isWinner.text = resPerson.extendedData.winRate+ " %";
                if (Maria.stthqplayer1 == "win")
                {
                    win.SetActive(true);
                }
                else
                {
                    low.SetActive(true);
                }
            }

        };
        ws.OnError += (sender, e) =>
        {
            Debug.Log(e.Message);
        };
    }
    public static bool TryDeserialize<T>(JSONNode data, out T result) where T : class, new()
    {
        return TryDeserialize(data.ToString(), out result);
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
