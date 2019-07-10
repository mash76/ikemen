using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dbg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void dumpIntArray(int[] ary){

    	Debug.Log("dumpIntArray" );
    	 foreach(int a in ary){
    	 	Debug.Log(a);
    	 }

    } 


    public static void isSpriteNull(Sprite obj){

    	if (obj == null) {
    		Debug.Log("isSpriteNull Sprite null");
    		UnityEngine.Application.Quit();
    	}else{
    		Debug.Log("isSpriteNull Sprite name " + obj.name);    		
    	}

    } 


    public static void isGONull(GameObject obj){

    	if (obj == null) {
    		Debug.Log("isGONull GO null");
    		UnityEngine.Application.Quit();
    	}else{
    		Debug.Log("isGONull GO name " + obj.name);    		
    	}

    } 
}
