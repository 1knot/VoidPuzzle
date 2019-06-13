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
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MenuBtn").GetComponent<Button>().onClick.AddListener(() => { GoToMainMenu(); });
        GameObject.Find("btn1").GetComponent<Button>().onClick.AddListener(() => { OnClickThemeBtn(IMG_ID.btn1); });
        GameObject.Find("btn2").GetComponent<Button>().onClick.AddListener(() => { OnClickThemeBtn(IMG_ID.btn2); });
        GameObject.Find("btn3").GetComponent<Button>().onClick.AddListener(() => { OnClickThemeBtn(IMG_ID.btn3); });
        GameObject.Find("btn4").GetComponent<Button>().onClick.AddListener(() => { OnClickThemeBtn(IMG_ID.btn4); });
        GameObject.Find("btn5").GetComponent<Button>().onClick.AddListener(() => { OnClickThemeBtn(IMG_ID.btn5); });
    }
    //进入主菜单
    void GoToMainMenu()
    {
        Debug.Log("进入Mainmenu");
        SceneManager.LoadScene("Mainmenu");
    }

    void OnClickThemeBtn(IMG_ID theme)
    {
        SceneManager.LoadScene("SelectLevel");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
