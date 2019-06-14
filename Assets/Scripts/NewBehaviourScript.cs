using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;


public class NewBehaviourScript : MonoBehaviour
{
    
    Puzzle4 p;
    
    public static int difficult = 0;
    float _timer;

    Text label;
    Text labe2;
    //结束页面
    GameObject resultPanel;

    public GameObject go1;
    
    //电脑需要的步数
    string needStepsStr = "电脑思考中。。";
    Text needSteps;
    //还原按钮
    GameObject hyBtn;
    //还原按钮是否可视
    bool canHyBtNVisual = false;
    //用户是否可操作
    bool canUserOp = true;
    //电脑的操作 0不操作 1向上 2向左 3 向右 4 向下
    int ComputerOp = 0;

    public GameObject winTxtObj;
    public GameObject failedTxtObj;

    void Start()
    {

        winTxtObj = GameObject.Find("winTxtObj");
        failedTxtObj = GameObject.Find("failedTxtObj");

        string picName = Scene_Select.picName;
        GameObject.Find("original_img").GetComponent<Image>().sprite = GameObject.Find(picName).GetComponent<SpriteRenderer>().sprite;
        for(int i = 1; i <=16; i++){
            string id = i.ToString();
            GameObject.Find(id).GetComponent<Image>().sprite = GameObject.Find(picName+"_"+id).GetComponent<SpriteRenderer>().sprite;
        }

        //找到结束页面对象
        resultPanel = GameObject.Find("Resultpanel");
        //设置结束页面不可见，下面有假设设置可见
        resultPanel.SetActive(false);

        //绑定text文本
        label = GameObject.Find("Text_number").GetComponent<Text>();
        labe2 = GameObject.Find("Text_time").GetComponent<Text>();
        needSteps = GameObject.Find("Need_steps").GetComponent<Text>();
        label.text = "0次";
        labe2.text = "0";
    
        hyBtn = GameObject.Find("hyBtn");


        GameObject.Find("MenuBtn").GetComponent<Button>().onClick.AddListener(() => { GoToMainMenu(); });

        //这个构造函数里填的是地图种子
        //种子的范围是0~20922789887999
        //以及，还没做测试
        //p = new Puzzle4(0);
        //后来换成了随机种子
        p = new Puzzle4();

        // resultPanel.p = p;
        // resultPanel.rp = resultPanel
        GameObject.Find("czBtn").GetComponent<Button>().onClick.AddListener(() => { czBtnClick(); });
        hyBtn.GetComponent<Button>().onClick.AddListener(() => { hyBtnClick(); });
        new Thread(sloveThread).Start();
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
        if (p.isFinish())
        {
            _timer = _timer;
            labe2.text = _timer.ToString("F2") + "秒";
        }
        else
        {
            //计数
            _timer += Time.deltaTime;
            labe2.text = _timer.ToString("F2") + "秒";
        }
       
        
        if(canUserOp)
        {
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
        }else if(ComputerOp != 0){
            switch(ComputerOp)   {
                case 1:
                    p.goUp();
                    ComputerOp = 0;
                    break;
                case 2:
                    p.goLeft();
                    ComputerOp = 0;
                    break;
                case 3:
                    p.goRight();
                    ComputerOp = 0;
                    break;
                case 4:
                    p.goDown();
                    ComputerOp = 0;
                    break;
            }
        }
        
        label.text = p.getUseStep().ToString() + "次";
        needSteps.text = needStepsStr;
        hyBtn.SetActive(canHyBtNVisual);
    }

    void afterUDLR()
    {
        if(p.isFinish())
        {
            Debug.Log("完成拼图提示");
            //拼图完成      
            //结束页面可见
            resultPanel.SetActive(true);
            winTxtObj.SetActive(false);
            failedTxtObj.SetActive(false);

            if(difficult > 0)
            {
                if(difficult == 1)
                {
                    if(p.getUseStep() <= p.getEasyStep())
                    {
                        winTxtObj.SetActive(true);
                    }
                    else
                    {
                        failedTxtObj.SetActive(true);
                    }
                }
                else if(difficult == 2)
                {
                    if(p.getUseStep() <= p.getHardStep())
                    {
                        winTxtObj.SetActive(true);
                    }
                    else
                    {
                        failedTxtObj.SetActive(true);
                    }
                }
            }
            else
            {
                winTxtObj.SetActive(true);
            }
            //还有停止计时

        }
    }
    void czBtnClick()
    {
        p.resetPuzzle(p.getMapSeed());
        Debug.Log("点击了重置按钮");
    }
    void hyBtnClick(){
        canUserOp = false;
        new Thread(() => {
            int step = p.getHardStep();
            char[] path = p.getHardPath();
            for (int i = 0; i < step; i++)
            {
                switch (path[i])
                {
                    case  'D':
                        ComputerOp = 1;
                        break;
                    case  'U':
                        ComputerOp = 4;
                        break;
                    case  'R':
                        ComputerOp = 2;
                        break;
                    case  'L':
                        ComputerOp = 3;
                        break;
                }
                Thread.Sleep(200);
            }
            canUserOp = true;
        }).Start();
        
    }
    
    //用于运行解决进程
    void sloveThread(){
        Debug.Log("开始解决进程");
        if(p.slove()){
            needStepsStr  = "电脑思考完了并以" + p.getHardStep() + "步完成了比赛";
            string str = "";
            for(int i=0; i<p.getHardStep(); i++){
                str += p.getHardPath()[i];
            }
            Debug.Log(str);
            canHyBtNVisual = true;
        }
    }
}
