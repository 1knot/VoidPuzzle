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
        _timer += Time.deltaTime;

        if (_timer % 0.5f > 0.25f)
        {
            anyKeyObj.SetActive(true);
        }
        else
        {
            anyKeyObj.SetActive(false);
        }

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