using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class clik : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //获取按钮游戏对象
        GameObject btnObj = GameObject.Find("Canvas/Button");
        //获取按钮脚本组件
        Button btn = (Button)btnObj.GetComponent<Button>();
        //添加点击侦听
        btn.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick()
    {
        Debug.Log("click!");
    }

}

