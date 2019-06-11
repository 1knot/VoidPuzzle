using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    List<int> l = new List<int>();
    Puzzle4 p;
    void Start()
    {
        p = new Puzzle4(0);
        //Debug.Log(p.getSeed());
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
