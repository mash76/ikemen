using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class Zukan : MonoBehaviour
{

	public float zukanX = 0f;
	public float mouseX = 0f; 
    public bool showStatus = false;
    private float scrollSpeed = 2.5f;

    private Vector2 topLeft = new Vector2(-600.0f,250.0f);
    private float Xmargin = 220.0f;
    private float Ymargin = 220.0f;

    public static GameObject[] zukanImages;

    // Start is called before the first frame update
    void Start()
    {
        
        zukanX = this.transform.position.x;

        // 図鑑を並べる
        GameObject prefab = (GameObject)Resources.Load ("zukanQuestion");
        // プレハブからインスタンスを生成
        GameObject cv = GameObject.Find("Canvas");
        GameObject zukanBase = GameObject.Find("Canvas/zukanLayer/zukan");

        zukanImages = new GameObject[ gameManager.ikemenTotalCount +1 ];
        for (int i =0; i<= 4; i++){
            for (int j =0; j<= 8; j++){
                int ikemenCode = i*8 + j +1;
                GameObject p1 = Instantiate (prefab, new Vector3( topLeft.x + j * Xmargin, topLeft.y - i * Ymargin, 0.1f), Quaternion.identity); 
                p1.transform.SetParent(zukanBase.transform, false);
                p1.name = "question" + i.ToString() + j.ToString();
                if (ikemenCode <= gameManager.ikemenTotalCount){
                    Sprite sp1 = Resources.Load<Sprite>("ikemen" + ikemenCode.ToString() + "_1");
                    p1.GetComponent<Image>().overrideSprite = sp1;
                    Debug.Log(ikemenCode);
                    zukanImages[ikemenCode] = p1;
                }
            }
        }
        this.gameObject.transform.parent.gameObject.SetActive(false);

    }



    // Update is called once per frame
    void Update()
    {
        for(int i=1; i<= gameManager.ikemenTotalCount; i++){
            if (Random.Range( 0,10000) < 15){
                GameObject face = zukanImages[i];

                Sprite sp1 = Resources.Load<Sprite>("ikemen" + i.ToString() + "_1");
                Sprite sp2 = Resources.Load<Sprite>("ikemen" + i.ToString() + "_2");
                face.GetComponent<Image>().overrideSprite = sp2;


                Sequence seq = DOTween.Sequence();
                seq.Append(face.transform.DOScale(Vector3.one * 1.15f, 0.1f).SetEase(Ease.OutExpo))
                    .Join(face.transform.DORotate(new Vector3(40f,0f,0f), 0.1f))
                    .Append(face.transform.DOScale(Vector3.one, 0.4f))
                    .Join(face.transform.DORotate(new Vector3(0f,0f,0f), 0.2f))
                    .AppendCallback( () => {
                        Debug.Log(" notification comp");
                        face.GetComponent<Image>().overrideSprite = sp1;
                    })
                    .Play();

            }
        }
    }

    public void startMove(){
    	mouseX = Input.mousePosition.x;
        zukanX = this.transform.position.x;
    } 

    public void moving(){
    	float newX = (Input.mousePosition.x - mouseX) * scrollSpeed;
        float newXX = zukanX + newX;
        float leftLimit = 0.0f;         
        float rightlimit = -1123.0f + (735.0f / 2.0f); //1500.0f - 735.0f; //0.0f;//-375.0f;
        if (newXX < rightlimit) newXX = rightlimit;
        if (newXX > leftLimit) newXX = leftLimit;
        this.transform.position = new Vector2(newXX, this.transform.position.y);
    } 

    public void toggleZukan(){
        Debug.Log("toggle zukan");
        showStatus = !showStatus;
        this.gameObject.transform.parent.gameObject.SetActive(showStatus);
    }


}
