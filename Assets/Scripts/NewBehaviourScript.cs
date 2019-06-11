using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    Puzzle4 p;
    void Start()
    {
        //这个构造函数里填的是地图种子
        //种子的范围是0~20922789887999
        //以及，还没做测试
        p = new Puzzle4(0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
            p.goDown();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            p.goUp();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            p.goRight();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            p.goLeft();
        
    }
}
