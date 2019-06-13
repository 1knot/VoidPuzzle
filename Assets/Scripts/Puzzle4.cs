using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Puzzle4
{
    //所用的步数
    private int useStep = 0;
    //开局前的各个unit的位置
    Dictionary<string, Vector2> unitPos = new Dictionary<string, Vector2>();
    //当前各个unit的矩形变换
    Dictionary<string, RectTransform> curUnitRT = new Dictionary<string, RectTransform>();
    //当前的unit排列数据
    List<int> data = new List<int>();
    //空格的位置
    private int spacePos = 0;
    //这个局面的种子
    private long seed = 0;
    //easy难度的步数
    private int easyStep = 0;
    //hard难度难度的步数
    private int hardStep = 0;
    //Hard难度的解决路径
    private char[] hardPath;
    
    private GameObject unit16 = GameObject.Find("16");

    //根据种子生成一个Puzzle
    public Puzzle4(long seed){
        this.seed = seed;
        this.getInitUnitPos();
        this.init();
        this.resetPuzzle(seed);
        unit16.SetActive(false);
    }
    public bool resetPuzzle(long seed){
        this.useStep = 0;
        data.Clear();
        List<int> v = new List<int>();
        long weight = 1;
        for(int i = 1; i <= 16; i++){
            v.Add(i);
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
        Debug.Log("重置了拼图");
        return true;
    }

    private void rand(int step){
        this.easyStep = 0;
        if(step<=0){
            return;
        }
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
                        this.easyStep++;
                        this.useStep--;
                    }
                    break;
                case 1://左
                    if(this.canLeft()){
                        this.goLeft();
                        pre = 1;
                        this.easyStep++;
                        this.useStep--;
                    }
                    break;
                case 2://右
                    if(this.canRight()){
                        this.goRight();
                        pre = 2;
                        this.easyStep++;
                        this.useStep--;
                    }
                    break;
                case 3://下
                    if(this.canDown()){
                        this.goDown();
                        pre = 3;
                        this.easyStep++;
                        this.useStep--;
                    }
                    break;
                default:
                    break;
            }
        }
    }
    //随机出一个Puzzle
    public Puzzle4(){
        this.getInitUnitPos();
        this.init();
        for(int i = 1; i <= 16; i++){
            data.Add(i);
        }
        this.spacePos = 16;
        this.rand(75);
        this.seed = this.getSeed();
        unit16.SetActive(false);
    }
    //获取初始的unit位置
    private bool getInitUnitPos(){
        unitPos.Clear();
        for(int i = 1; i <= 16; i++){
            string id = i.ToString();
            unitPos.Add(id, GameObject.Find(id).GetComponent<RectTransform>().anchoredPosition);
        }
        return true;
    }
    //初始化curUnit
    private bool init(){
        curUnitRT.Clear();
        for(int i = 1; i <= 16; i++){
            string id = i.ToString();
            RectTransform idRT =  GameObject.Find(id).GetComponent<RectTransform>();
            curUnitRT.Add(id, idRT);
        }
        return true;
    }
    //获取原始地图种子
    public long getMapSeed(){
        return this.seed;
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
            this.useStep++;
            return true;
        }else{
            return false;
        }
    }
    public bool goDown(){
        if(this.canDown()){
            this.swap(spacePos + 4);
            spacePos += 4;
            this.useStep++;
            return true;
        }else{
            return false;
        }
    }
    public bool goLeft(){
        if(this.canLeft()){
            this.swap(spacePos - 1);
            spacePos -= 1;
            this.useStep++;
            return true;
        }else{
            return false;
        }
    }
    public bool goRight(){
        if(this.canRight()){
            this.swap(spacePos + 1);
            spacePos += 1;
            this.useStep++;
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
    
    public bool slove(){
        VirtualPuzzle4 v = new VirtualPuzzle4(this.data);
        v.slove();
        this.hardStep = v.step;
        this.hardPath = new char[v.step];
        for(int i = 0; i < this.hardStep; i++){
            this.hardPath[i] = v.path[i];
        }
        return true;
    }

    public bool isFinish(){
        for(int i = 0; i < 16; i++){
            if(this.data[i] != i+1)
                return false;
        }
        unit16.SetActive(true);
        return true;
    }
    public int getHardStep(){
        return this.hardStep;
    }
    public char[] getHardPath(){
        return this.hardPath;
    }
    public int getUseStep(){
        return this.useStep;
    }
}