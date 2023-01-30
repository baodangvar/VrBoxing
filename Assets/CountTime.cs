using System.Collections;
using System.Collections.Generic;
using OVRSimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class CountTime : MonoBehaviour
{
    public float levelToLoad = 1f;
    float currentTime = 0f;
    float startingTime = 5f;
    public GameObject model;
    public GameObject UI;

    [SerializeField] Text countdownText;

    //private void OnEnable()
    //{
    //    RunNow.EventManager.WS_OnMessage += OnResponse;
    //}
    //private void OnDisable()
    //{
    //    RunNow.EventManager.WS_OnMessage -= OnResponse;
    //}
    void Start()
    {
        currentTime = startingTime;
        //WsParam wsParam = new WsParam("MatchPvPBoxing");
        //WsClient.instance.RequestToServer(wsParam.getData());
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        currentTime -= 1f * Time.deltaTime;
        countdownText.text = currentTime.ToString("f0");
        if (currentTime <= 0f)
        {

            var sence = "";
            if (levelToLoad == 1f)
            {
                //sence = "canh2";
                //Application.LoadLevel(sence);
                model.SetActive(true);
                UI.SetActive(false);
                //WsParam wsParam = new WsParam("MatchPvPBoxing");
                //WsClient.instance.RequestToServer(wsParam.getData());
            }

        }
    }
    //public void OnResponse(JSONNode data)
    //{
    //    string cmd = data[ParamKey.COMMAND].Value;

    //    switch (cmd)
    //    {
    //        case "MatchPvPBoxing":
    //            //if (needlePet != null || treePet != null || waterPet != null || firePet != null || earthPet != null)
    //            //{
    //            //    needlePet.Clear();
    //            //    treePet.Clear();
    //            //    waterPet.Clear();
    //            //    firePet.Clear();
    //            //    earthPet.Clear();
    //            //}
    //            if (!Utils.TryDeserialize<ResultData>(data, out var info))
    //            {
    //                var test1 = info;
    //                //UIManager.ShowInfoPopup("Unable to parse data from server");
    //                //return;
    //            }

    //            //RenderElementToScreen(info);
    //            //Debug.Log(info);
    //            var test = data;
    //            var x = 0;
    //            break;
    //    }
    //}
}
