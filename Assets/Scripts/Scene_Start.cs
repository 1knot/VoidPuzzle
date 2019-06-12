using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Start : MonoBehaviour
{

    float _timer;
    GameObject anyKeyObj;

    // Use this for initialization
    void Start()
    {
        _timer = 0;
        anyKeyObj = GameObject.Find("Text");
    }

    // Update is called once per frame
    void Update()
    {
        //计数
        _timer += Time.deltaTime;

        //文字闪烁
        if (_timer % 0.5f > 0.25f)
        {
            //文字可见
            anyKeyObj.SetActive(true);
        }
        else
        {
            //文字不可见
            anyKeyObj.SetActive(false);
        }

        //累计时间大于100或按下任意键
        if (_timer > 100 || Input.anyKeyDown)
        {
            //任意键按下，则直接跳转主菜单场景
            GoToMainMenu();
        }
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}