using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

//using System; // array

public class ikemen1 : MonoBehaviour
{
	public Sprite sp1,sp2; // 差し替え用画像
	private GameObject gManager;


	public int interval1 = 60; // 再出現インターバル
	public int count = 0; // 内部 再出現カウント

	private Vector3 scale1;
	private int objStatus = 0; // 0 ノーマル 1=クリック中〜表示完了

	private int ikemenCodeNow = 0 ; 

    // Start is called before the first frame update
    void Start()
    {
    	changeIkemen();

    }

    private void changeIkemen(){

        float ikemenVal = Mathf.Floor(gameManager.score / 1000);
        if (ikemenVal > gameManager.ikemenTotalCount) ikemenVal = gameManager.ikemenTotalCount;

    	ikemenCodeNow = Random.Range( 0, gameManager.ikemenTotalCount) +1; 

        sp1 = Resources.Load<Sprite>("ikemen" + ikemenCodeNow.ToString() + "_1");
        sp2 = Resources.Load<Sprite>("ikemen" + ikemenCodeNow.ToString() + "_2");
        scale1 = this.transform.localScale;
        this.GetComponent<Image>().overrideSprite = sp1;

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnPointClick(){

    	if (objStatus == 1 ) return;
  		objStatus = 1;

    	this.GetComponent<Image>().overrideSprite = sp2;
    	gameManager.PlaySound();
    	this.transform.localScale = new Vector3(scale1.x * 1.2f,scale1.y * 1.2f ,scale1.z);

    	Transform tran = this.gameObject.transform;

    	// 複数連続
  		Sequence seq = DOTween.Sequence();
		seq.Append(tran.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack))
			.AppendCallback( () => {
					Debug.Log("disapeear " + ikemenCodeNow.ToString());
					this.gameObject.SetActive(false);
					gameManager.setScore(100);

					// 新イケメンなら演出
					Debug.Log("gameManager.aryZukan ");
					Debug.Log(gameManager.aryZukan);
					if (gameManager.aryZukan[ikemenCodeNow] == 0){
						gameManager.aryZukan[ikemenCodeNow] = 1;
						Debug.Log("new ikemen get " + ikemenCodeNow.ToString());
						newIkemen(ikemenCodeNow);
					}
				}
			).Play();
    }


    public void newIkemen(int ikemenCodeNow){
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
