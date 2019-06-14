using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum THEME_ID
{
    Logo,
    Student
}

public class Scene_MainMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Debug.Log("Start");
        GameObject.Find("PlayBtn").GetComponent<Button>().onClick.AddListener(() => { OnClick(0); });
        GameObject.Find("JdBtn").GetComponent<Button>().onClick.AddListener(() => { OnClick(1); });
        GameObject.Find("KnBtn").GetComponent<Button>().onClick.AddListener(() => { OnClick(2); });
        GameObject.Find("setBtn").GetComponent<Button>().onClick.AddListener(() => { OnClick_2(); });
        GameObject.Find("CloseBtn").GetComponent<Button>().onClick.AddListener(() => { OnCloseApp(); });
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
    }

    //进入所选菜单
    void OnClick(int diff)
    {
        NewBehaviourScript.difficult = diff;
        Debug.Log("进入SampleScene");
        SceneManager.LoadScene("SampleScene");
    }

    //进入所选菜单
    void OnClick_2()
    {
        Debug.Log("进入SelectLevel");
        SceneManager.LoadScene("SelectLevel");
    }

    //退出程序
    void OnCloseApp()
    {
        Debug.Log("进入OnCloserApp");
        /*因为Editor下使用 UnityEditor.EditorApplication.isPlaying = false 结束退出，
        只有当工程打包编译后的程序使用Application.Quit()才起作用*/
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}