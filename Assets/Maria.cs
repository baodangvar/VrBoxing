using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class Maria : MonoBehaviour
{
    private Animator anim;
    public GameObject HandL;
    public GameObject HandR;
    public Slider hqplayer1;
    public Slider hqplayer2;
    float check = 0;
    float checkr = 0;

    //new
    float winLow = 0;
    float timeTurn = 6;
    float turnl = 0;
    public GameObject mode3d;
    float a = 0.002f;
    float c = 0.01f;
    public GameObject backNham;
    public GameObject backBungNo;
    public GameObject backTrung;
    float checkAnimationAttackOrDefend = 0;
    public GameObject cameraOVR;
    public GameObject popupResult;
    public static string stthqplayer1 ="";

    void Start()
    {
        winLow = 0;
        timeTurn = 6;

        anim = GetComponent<Animator>();
        anim.SetInteger("AttackIndex", 6);
        anim.SetTrigger("Attack");
    }
    void Update()
    {
        var positionL = HandL.transform.position;
        var positionR = HandR.transform.position;
        print("x" + positionL.x);
        print("y" + positionL.y);
        print("z" + positionL.z);
        if (positionL.z > 0.4)
        {
            if (check == 0)
            {
                if (backNham.activeSelf == true)
                {
                    if (positionL.x > -0.09 && positionL.x < 0.06)
                    {
                        anim.SetInteger("AttackIndex", 2);
                        anim.SetTrigger("Attack");
                        hqplayer1.value -= 10;
                        checkAnimationAttackOrDefend = 1;
                    }
                }
            }
            check = 1;

        }
        if (positionL.z < 0.3)
        {
            check = 0;
        }

        if (positionR.z > 0.4)
        {
            if (checkr == 0)
            {
                if (backNham.activeSelf == true)
                {
                    if (positionR.x > -0.09 && positionR.x < 0.06)
                    {
                        anim.SetInteger("AttackIndex", 2);
                        anim.SetTrigger("Attack");
                        hqplayer1.value -= 10;
                        checkAnimationAttackOrDefend = 1;
                    }
                }
            }
            checkr = 1;

        }
        if (positionR.z < 0.3)
        {
            checkr = 0;
        }
        if (hqplayer1.value == 0)
        {
            winLow = 1;
            stthqplayer1 = "win";
            popupResult.SetActive(true);
            mode3d.SetActive(false);
        }//mở lại
        
        //NPC

        if (winLow == 0)
        {

            if (timeTurn <= 0)
            {
                turnl = 0;
                timeTurn = 6;
                checkAnimationAttackOrDefend = 0;
            }
            timeTurn -= 1f * Time.deltaTime;
            if (timeTurn > 4)
            {
                var positionmode3D = mode3d.transform.localPosition;
                mode3d.transform.localPosition = new Vector3(positionmode3D.x - a, positionmode3D.y, positionmode3D.z);
                if (positionmode3D.x < -0.2) a = -0.005f;
                if (positionmode3D.x > 0.2) a = 0.005f;
            }
            if (timeTurn < 4 && timeTurn > 3.6)
            {
                var positionmode3D = mode3d.transform.localPosition;
                print("NPC MODE Z" + positionmode3D.z);
                if (positionmode3D.z > 0.6) mode3d.transform.localPosition = new Vector3(positionmode3D.x, positionmode3D.y, positionmode3D.z - c);
                if (positionmode3D.z < 1.1)
                {
                    backNham.SetActive(true);
                }
            }
            if (timeTurn < 3.1 && timeTurn > 2)
            {
               
                backNham.SetActive(false);
                if (checkAnimationAttackOrDefend == 0)
                {
                    anim.SetInteger("AttackIndex", 3);
                    anim.SetTrigger("Attack");
                    if (turnl == 0)
                    {
                        turnl = 1;
                        hqplayer2.value -= 10;
                        if (hqplayer2.value == 0)
                        {
                            winLow = 1;
                            stthqplayer1 = "low";
                            popupResult.SetActive(true);
                            mode3d.SetActive(false);
                        }
                    }
                }
                if (checkAnimationAttackOrDefend == 1)
                {
                    anim.SetInteger("AttackIndex", 0);
                    anim.SetTrigger("Attack");
                }
            }
            if (timeTurn < 2 && timeTurn > 1.5)
            {
                var positionmode3D = mode3d.transform.localPosition;

                if (turnl == 1) backTrung.SetActive(true);
                if (checkAnimationAttackOrDefend == 1)
                {
                    backBungNo.SetActive(true);
                    if (positionmode3D.z > 1.1) mode3d.transform.localPosition = new Vector3(positionmode3D.x, positionmode3D.y, positionmode3D.z + 3 * c);
                }
                if (positionmode3D.z > 1 && positionmode3D.z < 1.2)
                {
                    mode3d.transform.localPosition = new Vector3(positionmode3D.x - a, positionmode3D.y, positionmode3D.z);
                }
            }
            if (timeTurn < 1.5 && timeTurn > 0)
            {
                anim.SetInteger("AttackIndex", 6);
                anim.SetTrigger("Attack");
                backBungNo.SetActive(false);
                backTrung.SetActive(false);

                var positionmode3D = mode3d.transform.localPosition;
                if (positionmode3D.z < 1.1) mode3d.transform.localPosition = new Vector3(positionmode3D.x, positionmode3D.y, positionmode3D.z + c);
                if (positionmode3D.z > 1 && positionmode3D.z < 1.2)
                {
                    mode3d.transform.localPosition = new Vector3(positionmode3D.x - a, positionmode3D.y, positionmode3D.z);
                }
                if (positionmode3D.x < -0.2) a = -0.005f;
                if (positionmode3D.x > 0.2) a = 0.005f;
            }
        }
        else
        {
            anim.SetInteger("AttackIndex", 6);
            anim.SetTrigger("Attack");
            backNham.SetActive(false);
            backBungNo.SetActive(false);
            backTrung.SetActive(false);
            turnl = 0;
        }
    }


}
