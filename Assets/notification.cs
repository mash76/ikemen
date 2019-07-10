using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notification : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 1-3のレアリティをセット
    public void setStar(int rarity){
        // 1,2 normal     3 rare   4 sr
        this.transform.Find("star1").gameObject.SetActive(true);
        
        if (rarity >= 2){
            this.transform.Find("star2").gameObject.SetActive(true);
        }else{
            this.transform.Find("star2").gameObject.SetActive(false);            
        }
        if (rarity >= 3){
            this.transform.Find("star3").gameObject.SetActive(true);
        }else{
            this.transform.Find("star3").gameObject.SetActive(false);            
        }
    }
}
