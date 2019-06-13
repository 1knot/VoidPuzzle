using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{
    
    Puzzle4 p;
    int steps = 0;
    float _timer;

    Text label;
    Text labe2;

    void Start()
    {
        //绑定text文本
        label = GameObject.Find("Text_number").GetComponent<Text>();
        labe2 = GameObject.Find("Text_time").GetComponent<Text>();
        label.text = "0次";
        labe2.text = "0";
        GameObject.Find("MenuBtn").GetComponent<Button>().onClick.AddListener(() => { GoToMainMenu(); });
        //这个构造函数里填的是地图种子
        //种子的范围是0~20922789887999
        //以及，还没做测试
        p = new Puzzle4(0);
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
                //步数累加1
                steps++;
                label.text = steps.ToString() + "次";
                Debug.Log("向上移动了");
                Debug.Log(steps);
            }
           
        }
          
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (p.goUp())
            {
                steps++;
                label.text = steps.ToString() + "次";
                Debug.Log("向下移动了");
                Debug.Log(steps);
            }        
        }
            

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (p.goRight())
            {
                steps++;
                label.text = steps.ToString()+"次";
                Debug.Log("向左移动了");
                Debug.Log(steps);
            }
        }
            

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (p.goLeft())
            {
                steps++;
                label.text = steps.ToString() + "次";
                Debug.Log("向右移动了");
                Debug.Log(steps);
            }             
        }
            
     
    }
}
