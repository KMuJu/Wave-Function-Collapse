using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] sprites;
    private int spriteSize = 56;
    private string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private Dictionary<Color, char> farger = new Dictionary<Color, char>();
    private List<Module> modules = new List<Module>();
    void Start()
    {
        // farger[new Color()]
        // modules = new Module[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            Module m = new Module(sprites[i], lagKanter(sprites[i]));
            modules.Add(m);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Module> getModules(){
        return new List<Module>(modules);
    }

    private char getRandomChar(){
        int index = Random.Range(0, letters.Length);
        char c = letters[index];
        letters.Remove(index);
        return c;
    }

    public void print(int start, int n){
        string s = "";
        int antall = Mathf.Min(n, modules.Count);
        for (int i = start; i < antall; i++)
        {
            s += modules[i].ToString() + ", ";
        }
        Debug.Log(s);
    }

    private List<string> lagKanter(Sprite sprite){
        List<string> kanter = new List<string>();
        Color toppvenstre = sprite.texture.GetPixel(0, 55);
        Color toppmidt = sprite.texture.GetPixel(23, 55);
        Color topphoyre = sprite.texture.GetPixel(55, 55);

        Color bunnvenstre = sprite.texture.GetPixel(0, 0);
        Color bunnmidt = sprite.texture.GetPixel(23, 0);
        Color bunnhoyre = sprite.texture.GetPixel(55, 0);

        Color venstretopp = sprite.texture.GetPixel(0,55);
        Color venstremidt = sprite.texture.GetPixel(0,23);
        Color venstrebunn = sprite.texture.GetPixel(0,0);

        Color hoyretopp = sprite.texture.GetPixel(55,55);
        Color hoyremidt = sprite.texture.GetPixel(55,23);
        Color hoyrebunn = sprite.texture.GetPixel(55,0);

        Color[] fargearr = {toppvenstre, toppmidt, topphoyre, 
                            hoyretopp, hoyremidt, hoyrebunn,
                            bunnhoyre, bunnmidt, bunnvenstre,
                            venstrebunn, venstremidt, venstretopp};

        for (int i = 0; i < fargearr.Length; i+=3)
        {
            string kant = "";
            Color[] arr = {fargearr[i], fargearr[i+1], fargearr[i+2]};
            foreach (Color c in arr)
            {
                if (farger.ContainsKey(c)){
                    kant += farger[c];
                }
                else {
                    char bokstav = getRandomChar();
                    kant += bokstav;
                    farger[c] = bokstav;
                }
            }
            kanter.Add(kant);
        }

        return kanter;
    }
}
