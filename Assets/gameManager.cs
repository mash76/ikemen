﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject s1; // 効果音

    //点数
    public GameObject[] faces; // 樹になるイケメンUIImageの配列
    private int faceCount = 8; // 樹になるイケメン果実数

    //点数  実質の所持金
    public static int score ;
    public static GameObject scoreObj;

    public static int zukanGets ;
    public static GameObject zukanGetsObj;

    public static GameObject maskObj;

    public static GameObject notificationObj;
    public static GameObject notificationImageObj;

    //図鑑
    public static int[] aryZukan;
    public static int ikemenTotalCount = 6;


    // 統計用の数字
    // 総獲得数 プレイ時間、プレイ日数、起動回数


    void Start()
    {
        // sound
        s1 = GameObject.Find("nyu1");

        // 樹になるface
        faces = new GameObject[ faceCount ];
        for (int i=0; i<faceCount; i++){
            faces[i] = GameObject.Find("Canvas/faceLayer/face" + i);
            Debug.Log(faces[i]);
        }

        // 図鑑初期化
        aryZukan = new int[ikemenTotalCount];
        for (int i=0; i< ikemenTotalCount; i++){
            aryZukan[i] = 0;
        }
        //score
        score = 0;
        scoreObj = GameObject.Find("Canvas/UILayer/score");

        zukanGets = 0;
        zukanGetsObj = GameObject.Find("Canvas/UILayer/zukanCount");

        maskObj = GameObject.Find("Canvas/maskLayer/blackMask");
        maskObj.SetActive(false);

        notificationObj         = GameObject.Find("Canvas/notificationLayer");
        notificationImageObj    = GameObject.Find("Canvas/notificationLayer/Image");
        notificationObj.SetActive(false);

    }



    void Update()
    {
        // 非表示中の顔のタイマーカウント
         for (int i=0;i<faceCount;i++){
            if (faces[i].activeSelf == false){
                ikemen1 script = faces[i].GetComponent<ikemen1>();
                script.count++;
                if (script.count > script.interval1){
                    script.resetSprite();
                }
            }
         }
    }

    public static void newIkemenGet(Sprite sp1){ 

        Debug.Log("newIkemenGet");
        Debug.Log(sp1);

        maskObj.SetActive(true);
        notificationObj.SetActive(true);
        notificationObj.transform.localScale = Vector3.zero; 
        notificationImageObj.GetComponent<Image>().overrideSprite = sp1;

        Sequence seq = DOTween.Sequence();
        seq.Append(notificationObj.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutExpo))
            .Append(notificationObj.transform.DOScale(Vector3.one, 0.5f))
            .AppendCallback( () => {
                    Debug.Log(" notification comp");
                    notificationObj.SetActive(false);
                    maskObj.SetActive(false);
                }
            ).Play();

    }

    public static void addZukan(int val){ 
         zukanGets += val;
         Debug.Log(zukanGets);
         Text t1 = zukanGetsObj.GetComponent<Text>(); 
         t1.text = zukanGets.ToString();


    }   

    public static void setScore(int val){
         score += val;
         Debug.Log(score);
         Text t1 = scoreObj.GetComponent<Text>(); 
         t1.text = score.ToString();

    }

    public static void PlaySound(){
    	s1.GetComponent<AudioSource>().Play();
    }
}
