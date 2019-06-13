using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class NewBehaviourScript : MonoBehaviour
{
    
    Puzzle4 p;
    
    float _timer;

    Text label;
    Text labe2;
    //结束页面
    GameObject resultPanel;

    public GameObject go1;



    void Start()
    {
        //找到结束页面对象
        resultPanel = GameObject.Find("Resultpanel");
        //设置结束页面不可见，下面有假设设置可见
        resultPanel.SetActive(false);

        //绑定text文本
        label = GameObject.Find("Text_number").GetComponent<Text>();
        labe2 = GameObject.Find("Text_time").GetComponent<Text>();
        label.text = "0次";
        labe2.text = "0";
        GameObject.Find("MenuBtn").GetComponent<Button>().onClick.AddListener(() => { GoToMainMenu(); });
        //这个构造函数里填的是地图种子
        //种子的范围是0~20922789887999
        //以及，还没做测试
        //p = new Puzzle4(0);
        //后来换成了随机种子
        p = new Puzzle4();

        GameObject.Find("czBtn").GetComponent<Button>().onClick.AddListener(() => { czBtnClick(); });

        
    }

    //进入所选菜单
    void GoToMainMenu()
    {
        Debug.Log("进入Mainmenu");
        SceneManager.LoadScene("Mainmenu");
    }

    // Update is called once per frame
    void Update()
    {
        //计数
        _timer += Time.deltaTime;
        labe2.text = _timer.ToString("F2")+"秒";

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //向上移动成功
            if (p.goDown())
            {
                afterUDLR();
            }
        }
          
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (p.goUp())
            {
                afterUDLR();
            
            }        
        }
            

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (p.goRight())
            {
                afterUDLR();
            }
        }
            

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (p.goLeft())
            {
                afterUDLR();
            }             
        }
        label.text = p.getUseStep().ToString() + "次";
    }

    void afterUDLR()
    {
        if(p.isFinish())
        {
            Debug.Log("完成拼图提示");
            //拼图完成      
            //结束页面可见
            resultPanel.SetActive(true);
            //还有停止计时

        }
    }
    void czBtnClick()
    {
        p.resetPuzzle(p.getMapSeed());
        Debug.Log("点击了重置按钮");
    }
}
