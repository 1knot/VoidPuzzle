using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Debug.Log("点击了按钮");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
