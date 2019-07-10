using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

//using System; // array

public class ikemen : MonoBehaviour
{

	public Sprite sp1,sp2; // 差し替え用画像
	private GameObject gManager;

	public int interval1 = 60; // 再出現インターバル
	public int count = 0; // 内部 再出現カウント
	private int objStatus = 0; // 0 ノーマル 1=クリック中〜表示完了

    string ikemenCodeNow = "0";
    int ikemenInt = 0;





    // Start is called before the first frame update
    void Start()
    {
    	changeIkemen();
    }

    private void changeIkemen(){

        // 現在レベルに合わせて出現イケメン選択
        // float ikemenVal = Mathf.Floor(gameManager.score / 1000);
        // if (ikemenVal > gameManager.ikemenTotalCount) ikemenVal = gameManager.ikemenTotalCount;

        int iCate = Random.Range( 0, 4) +1; 
    	int iNum = Random.Range( 0, 4) +1; 

        ikemenCodeNow = iCate.ToString() + iNum.ToString();
        ikemenInt = (iCate -1 ) * 4 + iNum;

        sp1 = Resources.Load<Sprite>("ikemen" + ikemenCodeNow + "_1");
        sp2 = Resources.Load<Sprite>("ikemen" + ikemenCodeNow + "_2");
        dbg.isSpriteNull(sp1);
        dbg.isSpriteNull(sp2);
        this.GetComponent<Image>().overrideSprite = sp1;

        Debug.Log("changeIkemen " + ikemenInt.ToString() + "  " + ikemenCodeNow );

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnPointClick(){

        Debug.Log("click");

    	if (objStatus == 1 ) return;
  		objStatus = 1;

        Debug.Log(sp2);

    	//this.GetComponent<Image>().overrideSprite = sp2;
    	gameManager.PlaySound();
        Vector3 s1 = Vector3.one;
    	this.transform.localScale = new Vector3(s1.x * 1.2f,s1.y * 1.2f ,s1.z);

    	Transform tran = this.gameObject.transform;

    	// 複数連続
  		Sequence seq = DOTween.Sequence();
		seq.Append(tran.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack))
			.AppendCallback( () => {

					this.gameObject.SetActive(false);
					gameManager.setScore(100);

					// 新イケメンなら演出
                    Debug.Log("ikemen int " + ikemenInt.ToString());

                    // 二回目でここがおかしくなる
					if (gameManager.aryZukan[ikemenInt.ToString()] == "0"){
						gameManager.aryZukan[ikemenInt.ToString()] = "1";
						Debug.Log("new ikemen " + ikemenCodeNow);
						newIkemen();
					}else{
                        Debug.Log("kizon ikemen");
                    }
				}
			).Play();
    }


    public void newIkemen(){
    	//図関数を更新
    	gameManager.addZukan(1);

    	//図鑑を更新
    	Debug.Log("newIkemen");
    	Debug.Log(sp1);
    	gameManager.newIkemenGet(sp1);
    }

    public void resetSprite(){
    	Debug.Log("reset" + this.name);

    	changeIkemen();

		Transform tran = this.gameObject.transform;
		this.gameObject.SetActive(true);

  		Sequence seq = DOTween.Sequence();
		seq.Append(tran.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBack))
			.AppendCallback( () => {
					Debug.Log(" emerge completed");
			  		objStatus = 0;
				}
			).Play();

    	count = 0;
    }
}
