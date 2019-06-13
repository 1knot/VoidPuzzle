using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum IMG_ID
{
    btn1,
    btn2,
    btn3,
    btn4,
    btn5
}
public class Scene_Select : MonoBehaviour
{
    public static string picName = "bkc";
    public static int selected = 1;
    // Start is called before the first frame update
    void init()
    {
        for(int i = 1; i <= 4 ; i++)
        {
            if(i == selected)
                GameObject.Find("btn" + i).GetComponentInChildren<Text>().text = "已选择";
            else
                GameObject.Find("btn" + i).GetComponentInChildren<Text>().text = "";
        }
    }
    void Start()
    {
        init();
        GameObject.Find("MenuBtn").GetComponent<Button>().onClick.AddListener(() => { GoToMainMenu(); });
        GameObject.Find("btn1").GetComponent<Button>().onClick.AddListener(() => { OnClickThemeBtn(IMG_ID.btn1); });
        GameObject.Find("btn2").GetComponent<Button>().onClick.AddListener(() => { OnClickThemeBtn(IMG_ID.btn2); });
        GameObject.Find("btn3").GetComponent<Button>().onClick.AddListener(() => { OnClickThemeBtn(IMG_ID.btn3); });
        GameObject.Find("btn4").GetComponent<Button>().onClick.AddListener(() => { OnClickThemeBtn(IMG_ID.btn4); });
        //GameObject.Find("btn5").GetComponent<Button>().onClick.AddListener(() => { OnClickThemeBtn(IMG_ID.btn5); });
    }
    //进入主菜单
    void GoToMainMenu()
    {
        Debug.Log("进入Mainmenu");
        SceneManager.LoadScene("Mainmenu");
    }

    void OnClickThemeBtn(IMG_ID theme)
    {
        //SceneManager.LoadScene("SelectLevel");
        switch (theme)
        {
            case IMG_ID.btn1:
                picName = "bkc";
                selected = 1;
                init();
                break;
            case IMG_ID.btn2:
                picName = "hk";
                selected = 2;
                init();
                break;
            case IMG_ID.btn3:
                picName = "rem";
                selected = 3;
                init();
                break;
            case IMG_ID.btn4:
                picName = "yq";
                selected = 4;
                init();
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
