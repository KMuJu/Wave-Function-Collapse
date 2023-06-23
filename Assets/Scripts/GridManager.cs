using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{

    public int _width, _height;
    public Tile tilePrefab;
    public TileSetManager tileSetManager;
    public Transform _cam;
    public int index1;
    public int index2;
    public double tid;
    
    private TileList tileList;
    private double n;
    private Tile[,] grid;
    
    // Start is called before the first frame update
    void Start()
    {
        startSpill();
        _cam.transform.position = new Vector3((float)_width/2-0.5f, (float)_height/2-0.5f, -10);
        
    }

    public void restart(){
        tileList.restart();
        restartTiles();
    }

    private void startSpill(){
        grid = new Tile[_height, _width];
        tileList = new TileList(_width*_height);
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                makeTile(x, y);
            }
        }
    }

    private void restartTiles(){
        for (int i = 0; i < _width*_height; i++)
        {
            grid[i/_width, i%_width].setStates(tileSetManager.getModules());
            grid[i/_width, i%_width].setSprite(grid[i/_width, i%_width].basicSprite);
            tileList.add(grid[i/_width, i%_width]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        n+=1 * Time.deltaTime;
        if (n>tid){
            n= 0;
            
            run();
        }
        
    }

    private void run() {
        
        Tile t = getTile();
        if (t==null) return;
        endreTile(t);
        
        endreNaboer(t);
    }

    private void endreTile(Tile t){
        t.setSprite();
        tileList.update(t);
    }

    private void endreNaboer(Tile t){
        int x = t.getX();
        int y = t.getY();

        int[][] naboer = new int[][]{new int[]{0,1}, new int[]{1,0}, new int[]{0,-1}, new int[]{-1,0}};
        for (int i = 0; i < naboer.Length; i++)
        {
            int[] pos = naboer[i];
            
            int x2 = (x+pos[0]+_width)%_width;
            int y2 = (y+pos[1]+_height)%_height;
            
            if (outside(x, y)) {
                Debug.Log("utenfor");
                continue;
            }
            bool b = grid[y2, x2].changeStates(t, i);

            tileList.update(grid[y2,x2]);
            if (grid[y2, x2].isCollapsed()) continue;
            
            if (b) endreNaboer(grid[y2,x2]);
        }
    }

    private Tile getTile(){
        List<Tile> liste = tileList.getLowest();
        if (liste.Count==0) return null;
        return liste[Random.Range(0, liste.Count)];
    }

    private bool outside(int x, int y){
        return x<0||x>=_width || y<0||y>=_height;
    }

    private void makeTile(int x, int y){
        // Sprite sprite = sprites[Random.Range(0, sprites.Length)];
        var spawnedTile = Instantiate(tilePrefab, new Vector3(x,y), Quaternion.identity);
        spawnedTile.name = $"x: {x}, y: {y}";
        spawnedTile.setStates(tileSetManager.getModules());
        spawnedTile.setPos(x,y);
        grid[y,x] = spawnedTile;
        // spawnedTile.setSprite();
        tileList.add(spawnedTile);
    }
}
