// using System.Array;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{

    public SpriteRenderer _renderer;
    public GameObject hightlight;

    public Sprite basicSprite;

    private int listIndex;
    // private Sprite[] states;
    private List<Module> states;
    private bool collapsed = false;
    private int x, y;

    public override string ToString()
    {
        return name;
    }

    public void setStates(List<Module> states) {
        this.states = new List<Module>(states);
    }

    public void setPos(int x, int y){
        this.x = x;
        this.y = y;
    }

    public int getX() {
        return x;
    }

    public int getY(){
        return y;
    }

    public List<Module> getModules(){
        return states;
    }


    public int compare(Tile t){
        return t.getNumStates()-getNumStates();
    }

    public bool changeStates(Tile t, int kant){
        
        List<Module> modules = new List<Module>(t.getModules());
        HashSet<Module> ikkeslett = new HashSet<Module>();
        foreach (Module m1 in modules)
        {
            foreach (Module m2 in states){
                int index = (kant+2)%4;
                
                string k = Reverse(m2.getKant(index));
                
                if (k.Equals(m1.getKant(kant))){
                    ikkeslett.Add(m2);
                }

            }
        }
        if (ikkeslett.Count==states.Count) return false;
        List<Module> statesCopy = new List<Module>(states);
        foreach (Module m in statesCopy)
        {
            if (ikkeslett.Contains(m)) continue;
            states.Remove(m);
        }
        
        return true;

    }

    private string Reverse( string s )
    {
        char[] charArray = s.ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }

    public int getNumStates(){
        return states.Count;
    }

    public int getIndex(){
        return listIndex;
    }

    public void setIndex(int i){
        listIndex = i;
    }

    public bool isCollapsed(){
        return collapsed;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("string test: " + "ABB".Equals(Reverse("BBA")) + ", " + Reverse("BBA"));
    }

    public void check2Modules(Module m1, Module m2, int kant){
        int index = (kant+2)%4;
        
        string k = Reverse(m2.getKant(index));
        Debug.Log(k + ", " + m1.getKant(kant));
        Debug.Log(k.Equals(m1.getKant(kant)) + ", " + index + ", " + kant);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSprite(){
        Module m = states[Random.Range(0, states.Count)];
        _renderer.sprite = m.getSprite();
        collapsed = true;
        states = new List<Module>();
        states.Add(m);
    }
    
    public void setSprite(Sprite s){
        _renderer.sprite = s;
        collapsed = false;
    }

    public void setSprite(Module m){
        _renderer.sprite = m.getSprite();
    }
    

    public void OnMouseEnter(){
        hightlight.SetActive(true);
    }

    public void OnMouseExit(){
        hightlight.SetActive(false);
    }
}
