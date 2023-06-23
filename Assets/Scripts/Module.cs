using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module{
    private Sprite sprite;
    private List<string> kanter;
    public Module(Sprite sprite, List<string> kanter){
        this.sprite = sprite;
        this.kanter = kanter;
    }

    public Sprite getSprite(){
        return sprite;
    }

    public string getKant(int i){
        return new string(kanter[i]);
    }

    public override string  ToString(){
        return "[" + kanter[0] + ", " + kanter[1] + ", " + kanter[2] + ", " + kanter[3] + ", " + sprite.name + "]";
    }

    // public Module rotate(int n){
    //     List<string> kantCopy = new List<string>();


    //     return new Module(sprite, kantCopy);
    // }


}