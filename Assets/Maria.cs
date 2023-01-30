using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maria : MonoBehaviour
{
    private Animator anim;
    public GameObject HandL;
    public GameObject HandR;
    public Slider hqplayer1;
    //public GameObject image3;
    //public GameObject image4;
    //public GameObject image5;
    float check = 0;
    float checkr = 0;

    IEnumerator Start()
    {
        var countWin = 0;
        var countLow = 0;

        anim = GetComponent<Animator>();
        while (true)
        {
            yield return new WaitForSeconds(3);
            var x = Random.Range(0, 5);
            anim.SetInteger("AttackIndex", x);
            anim.SetTrigger("Attack");
            if (x == 2 || x == 3)
            {
                countWin++;
                print(countWin);
            }
            else
            {
                countLow++;
            }
            if (countWin == 3)
            {
                //image1.SetActive(true);
                //image2.SetActive(true);
                //image3.SetActive(true);
                //image4.SetActive(true);
                //anim.StopPlayback();
            }

        }

    }
    void Update()
    {
        var positionL = HandL.transform.position;
        var positionR = HandR.transform.position;
        print("x" + positionL.x);
        print("y" + positionL.y);
        print("z" + positionL.z);
        if(positionL.z> 0.4)
        {
            if (check == 0)
            {
                if (positionL.x > -0.09 && positionL.x < 0.06)
                {
                    hqplayer1.value -= 3;
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
                if (positionR.x > -0.09 && positionR.x < 0.06)
                {
                    hqplayer1.value -= 3;
                }
            }
            checkr = 1;

        }
        if (positionR.z < 0.3)
        {
            checkr = 0;
        }
    }


}
