using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4
{
    Dictionary<string, Vector2> unitPos = new Dictionary<string, Vector2>();
    Dictionary<string, RectTransform> curUnitRT = new Dictionary<string, RectTransform>();
    List<int> data = new List<int>();
    private int spacePos = 0;
    public Puzzle4(long seed){
        for(int i = 1; i <= 16; i++){
            string id = i.ToString();
            RectTransform idRT =  GameObject.Find(id).GetComponent<RectTransform>();
            curUnitRT.Add(id, idRT);
            unitPos.Add(id, idRT.anchoredPosition);
        }
        List<int> v = new List<int>();
        long weight = 1;
        for(int i = 1; i <= 16; i++){
            v.Add(i);
        }
        for(int i = 16; i > 0; i--){
            weight *= i;
        }
        for(int i = 16; i > 0; i--){
            weight /= i;
            int p = (int)(seed / weight);//额。。关于这一块的强转。。不太好。。但是又想不出比较好的方法
            seed = seed % weight;
            data.Add(v[p]);
            if(v[p] == 16){
                this.spacePos = data.Count;
            }
            v.RemoveAt(p);
        }
        for(int i = 1; i <= 16; i++){
            string posId = i.ToString();
            string unitId = data[i - 1].ToString();
            curUnitRT[unitId].anchoredPosition = unitPos[posId];
        }
    }
    //获取当前地图种子
    public long getSeed(){
        long weight = 1;
        List<int> v = new List<int>();
        for(int i = 1; i <= data.Count; i++){
            weight *= i;
            v.Add(i);
        }
        long seed = 0;
        for(int i = data.Count, pos = 0; i > 0; i--, pos++){
            weight /= i;
            for(int j = 0; j < v.Count; j++){
                if(v[j] == data[pos]){
                    seed += j * weight;
                    v.RemoveAt(j);
                    break;
                }
            }
        }
        return seed;
    }
    private void swap(int aim){
        int o = aim;
        string oid = data[o - 1].ToString();
        string opos = o.ToString();
        string sid = "16";
        string spos = spacePos.ToString();
        curUnitRT[oid].anchoredPosition = unitPos[spos];
        curUnitRT[sid].anchoredPosition = unitPos[opos];
        int temp = data[spacePos - 1];
        data[spacePos - 1] = data[o - 1];
        data[o - 1] = temp;
    }
    public bool goUp(){
        if(this.canUp()){
            this.swap(spacePos - 4);
            spacePos -= 4;
            return true;
        }else{
            return false;
        }
    }
    public bool goDown(){
        if(this.canDown()){
            this.swap(spacePos + 4);
            spacePos += 4;
            return true;
        }else{
            return false;
        }
    }
    public bool goLeft(){
        if(this.canLeft()){
            this.swap(spacePos - 1);
            spacePos -= 1;
            return true;
        }else{
            return false;
        }
    }
    public bool goRight(){
        if(this.canRight()){
            this.swap(spacePos + 1);
            spacePos += 1;
            return true;
        }else{
            return false;
        }
    }



    public bool canUp(){
        if (spacePos - 4 >= 1){
            return true;   
        }else{
            return false;
        }
    }
    public bool canDown(){
        if(spacePos + 4 <= 16){
            return true;
        }else{
            return false;
        }
    }
    public bool canLeft(){
        if((spacePos - 1) % 4 != 0){
            return true;
        }else{
            return false;
        }
    }
    public bool canRight(){
        if(spacePos % 4 != 0){
            return true;
        }else{
            return false;
        }
    }
}
