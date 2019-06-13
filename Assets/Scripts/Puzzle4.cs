//using System;
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
        string a = "";
        for(int i = 0; i<16; i++){
            a += data[i] +" ";
        }
        Debug.Log(a);
    }
    private void rand(int step){
        if(step<=0){
            return;
        }
        Debug.Log(step);

        int pre = 0;

        for(int i=0; i<step; i++){
            int r = Random.Range(0, 4);
            if(r + pre == 3){
                continue;
            }
            switch(r){
                case 0://上
                    if(this.canUp()){
                        this.goUp();
                        pre = 0;
                    }
                    break;
                case 1://左
                    if(this.canLeft()){
                        this.goLeft();
                        pre = 1;
                    }
                    break;
                case 2://右
                    if(this.canRight()){
                        this.goRight();
                        pre = 2;
                    }
                    break;
                case 3://下
                    if(this.canDown()){
                        this.goDown();
                        pre = 3;
                    }
                    break;
                default:
                    break;
            }
        }
            
    }
    public Puzzle4(){
        for(int i = 1; i <= 16; i++){
            string id = i.ToString();
            RectTransform idRT =  GameObject.Find(id).GetComponent<RectTransform>();
            curUnitRT.Add(id, idRT);
            unitPos.Add(id, idRT.anchoredPosition);
            data.Add(i);
        }
        this.spacePos = 16;
        this.rand(100);

    }
    public Puzzle4(Puzzle4 p){
        for(int i = 1; i <= 16; i++){
            string id = i.ToString();
            this.unitPos[id] = p.unitPos[id];
            this.curUnitRT[id] = p.curUnitRT[id];
            this.data[i - 1] = p.data[i - 1];
        }
        this.spacePos = p.spacePos;
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
    public static bool canSlove(long seed){
        List<int> pz = new List<int>();
        List<int> v = new List<int>();
        int zeroPos = 0;
        long weight = 1;
        for(int i = 1; i <= 16; i++){
            v.Add(i);
            weight *= i;
        }
        for(int i = 16; i > 0; i--){
            weight /= i;
            int p = (int)(seed / weight);
            seed = seed % weight;
            pz.Add(v[p]);
            if(v[p] == 16){
                zeroPos = pz.Count;
            }
            v.RemoveAt(p);
        }
        if(zeroPos != 16){
            if(zeroPos % 4 != 0){
                while((zeroPos + 1) % 4 != 1){
                    pz[zeroPos - 1] = pz[zeroPos - 1 + 1];
                    zeroPos++;
                }
            }
            while(zeroPos != 16){
                pz[zeroPos - 1] = pz[zeroPos - 1 + 4];
                zeroPos += 4;
            }
            pz[zeroPos - 1] = 16;
        }
        int ans = 0;
        for(int i = 1; i < 16; i++){
            for(int j = 0; j < i; j++){
                if(pz[i] < pz[j]){
                    ans++;
                }
            }
        }
        if(ans % 2 == 1){
            return false;
        }else{
            return true;
        }
        
    }
    
    public void slove(){
        VirtualPuzzle4 v = new VirtualPuzzle4(this.data);
        v.slove();
        string a = "";
        for(int i = 0; i < v.step; i++){
            a += " " + v.path[i];
        }
        a = "step: " + v.step + "  " + a;
        Debug.Log(a);
    }
}