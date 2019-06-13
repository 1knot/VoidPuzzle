using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Select : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MenuBtn").GetComponent<Button>().onClick.AddListener(() => { GoToMainMenu(); });
    }
    //进入主菜单
    void GoToMainMenu()
    {
        Debug.Log("进入Mainmenu");
        SceneManager.LoadScene("Mainmenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
