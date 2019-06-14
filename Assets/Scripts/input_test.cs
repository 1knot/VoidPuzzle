using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class input_test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<InputField>().onValueChanged.AddListener(Changed_Value);

        transform.GetComponent<InputField>().onEndEdit.AddListener(End_Value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void End_Value(string inp)
    {
        print("文本内容" + inp);
    }

    public void Changed_Value(string inp)
    {
        print("正在输入" + inp);
    }
}
