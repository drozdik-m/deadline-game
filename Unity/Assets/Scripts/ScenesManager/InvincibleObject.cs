using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object that is not destroyed by changing scenes and deletes any new duplicates
/// </summary>
public class InvincibleObject : MonoBehaviour
{
    private static InvincibleObject kingInstance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //Am I the duplicate?? HIRAKIRY
        if (kingInstance == null)
            kingInstance = this;
        else if (kingInstance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        AllConditions.Instance.Reset();
    }

    void Update()
    {
        
    }
}

//Wild cat appears
//(_＼ヽ
//　 ＼＼ .Λ＿Λ.
//　　 ＼( ͡° ͜ʖ ͡°) 　
//　　　 >　⌒ヽ
//　　　/ 　 へ＼
//　　 /　　/　＼＼
//　　 ﾚ ノ ヽ_つ
//　　/　/
//　 /　/|
//　((ヽ
//　|　|、＼
//　| 丿 ＼ ⌒)
//　| |　　) /
//`ノ ) 　 Lﾉ
//(_／