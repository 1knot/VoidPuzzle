using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelResult : MonoBehaviour
{

    public GameObject winTxtObj;
    //public Button againBtn;
    //public Button mainmenuBtn;
    


    private void Awake()
    {

       

    }

    // Use this for initialization
    void Start()
    {

       

        //调用方法
        //againBtn.onClick.AddListener(() => { againBtn(); });
        //mainmenuBtn.onClick.AddListener(() => { OnMainMenuBtn(); });
        //winTxtObj = GameObject.Find("winTxtObj");

        GameObject.Find("againBtn").GetComponent<Button>().onClick.AddListener(() => { againBtn(); });
        GameObject.Find("mainmenuBtn").GetComponent<Button>().onClick.AddListener(() => { OnMainMenuBtn(); });
        // GameObject.Find("cwBtn").GetComponent<Button>().onClick.AddListener(() => { 
        //     p.resetPuzzle(p.getMapSeed());
        //     rp.SetActive(false);
        // });

        //假设一开始就胜利
       // MatchResult(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //再来一次
    void againBtn()
    {
        Debug.Log("进入SampleScene");
        SceneManager.LoadScene("SampleScene");
    }

    //返回菜单
    void OnMainMenuBtn()
    {
        Debug.Log("进入Mainmenu");
        SceneManager.LoadScene("Mainmenu");
    }

    //设置拼图完成
    //public PanelResult resultPanel;
    //resultPanel.MatchResult(true);
    //另外一种实现 先不管了
/*public void MatchResult(bool win)
    {
        winTxtObj.SetActive(false);
        //againBtn.gameObject.SetActive(win);

        if (win)
        {
            

        }
    }*/
}
