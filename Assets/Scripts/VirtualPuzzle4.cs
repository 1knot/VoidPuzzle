using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VirtualPuzzle4
{
    public List<int> data = new List<int>();
    public int spacePos = 0;
    private int limit;
    
    private bool isFinish = false;
    public int step = 0;
    public char[] path = new char[500];
    public VirtualPuzzle4(List<int> pdata){
        for(int i = 1; i <= 16; i++){
            this.data.Add(pdata[i - 1]);
            if(pdata[i - 1] == 16){
                this.spacePos = i;
            }
        }
        this.limit = this.getDistance();
        if(this.limit == 0){
            this.isFinish = true;
        }
    }
    private int getDistance(){
        int distance = 0;
        for(int i = 1; i <= 16; i++){
            bool flag = true;
            for(int j = 1; j <= this.data.Count && flag; j++){
                if(i == this.data[j - 1]){
                    distance += Math.Abs(i - j) / 4 + Math.Abs(((i - 1) % 4) - ((j - 1) % 4));
                    flag = false;
                }
            }
        }
        return distance;
    }
    private void swap(int pos){
        this.data[this.spacePos - 1] = this.data[pos - 1];
        this.data[pos - 1] = 16;
        this.spacePos = pos;
    }

    private void dfs(int deep, int pre){
        if(this.isFinish){
            return;
        }
        int cost = getDistance();
        if(deep == limit){
            if(cost == 0){
                isFinish = true;
                step = deep;
            }else{
                return;
            }
        }else if(deep < limit){
            if(cost == 0){
                isFinish = true;
                step = deep;
                return;
            }
        }
        
        for(int i = 0; i <= 4; i++){
            if(i + pre == 3 && deep>0)
                continue;
            switch(i){
                case 0://上
                    if(this.canUp()){
                        this.swap(this.spacePos - 4);
                        int p = this.getDistance();
                        if(p + deep < this.limit && !isFinish){
                            path[deep] = 'D';
                            dfs(deep + 1, i);
                        }
                        this.swap(this.spacePos + 4);
                    }
                    break;
                case 1://左
                    if(this.canLeft()){
                        this.swap(this.spacePos - 1);
                        int p = this.getDistance();
                        if(p + deep < this.limit && !isFinish){
                            path[deep] = 'R';
                            dfs(deep + 1, i);
                        }
                        this.swap(this.spacePos + 1);
                    }
                    break;
                case 2://右
                    if(this.canRight()){
                        this.swap(this.spacePos + 1);
                        int p = this.getDistance();
                        if(p + deep < this.limit && !isFinish){
                            path[deep] = 'L';
                            dfs(deep + 1, i);
                        }
                        this.swap(this.spacePos - 1);
                    }
                    break;
                case 3://下
                    if(this.canDown()){
                        this.swap(this.spacePos + 4);
                        int p = this.getDistance();
                        if(p + deep < this.limit && !isFinish){
                            path[deep] = 'U';
                            dfs(deep + 1, i);
                        }
                        this.swap(this.spacePos - 4);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void slove(){
        while(!this.isFinish && this.step<=50){
            dfs(0, 0);
            if(!isFinish)
                limit++;
        }
    }

    private bool canUp(){
        if (spacePos - 4 >= 1){
            return true;   
        }else{
            return false;
        }
    }
    private bool canDown(){
        if(spacePos + 4 <= 16){
            return true;
        }else{
            return false;
        }
    }
    private bool canLeft(){
        if((spacePos - 1) % 4 != 0){
            return true;
        }else{
            return false;
        }
    }
    private bool canRight(){
        if(spacePos % 4 != 0){
            return true;
        }else{
            return false;
        }
    }
    /* 
    public bool isFinish(){
        for(int i = 0; i < 16; i++){
            if(this.data[i] != i+1)
                return false;
        }
        return true;
    }
    */
}
