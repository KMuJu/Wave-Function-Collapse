using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TileList {
    public Tile[] liste;
    int listIndex = 0;

    public TileList(int size){
        liste = new Tile[size];
    }

    public void print(){
        string retur = "";
        foreach (Tile t in getLowest()){
            if (t==null)continue;

            retur += "[" + t.ToString() + ", " +t.getNumStates() + "], ";
            // retur += t.getNumStates() + ", ";
        }
        Debug.Log(retur);
    }

    public List<Tile> getLowest(){
        List<Tile> retur = new List<Tile>();
        int verdi = liste[0].getNumStates();
        for (int i = 0; i < liste.Length; i++)
        {
            if (liste[i] == null) {
                break;
            }
            if (liste[i].isCollapsed()) {
                break;
            }
            if (liste[i].getNumStates()>verdi){
                break;
            }
            retur.Add(liste[i]);
        }
        return retur;
        
    }

    public void restart() {
        listIndex = 0;
    }

    public void add(Tile t){
        if (listIndex>=liste.Length){
            Debug.Log("For mange tiles");
            return;
        }
        liste[listIndex] = t;
        t.setIndex(listIndex);
        moveUp(t);
        listIndex++;
    }

    public void update(Tile t){
        if (t.isCollapsed()){
            moveDown(t);
            return;
        }
        moveUp(t);

    }

    public void moveUp(Tile t){
        int i = 0;
        while (true) {
            i++;
            if (i+2>liste.Length) return;
            if (t.getIndex()-1>=0){
                Tile c1 = liste[t.getIndex()-1];
                
                if (t.compare(c1)>0){
                    swap(t, c1);
                    continue;
                }
            }
            return;
        }
    }

    private void moveDown(Tile t){
        while(t.getIndex()+1<listIndex){
            swap(liste[t.getIndex()], liste[t.getIndex()+1]);
        }
    }

    private void swap(Tile a, Tile b){
        liste[a.getIndex()] = b;
        liste[b.getIndex()] = a;
        int aIndex = a.getIndex();
        a.setIndex(b.getIndex());
        b.setIndex(aIndex);
    }
}

