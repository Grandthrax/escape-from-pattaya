using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private int width;
    private int height;

    public int widthWutliple;
    public int heightMultiple;

    public float enemyExponent = 0.1f;
    public GameObject[] enemies;

     QuestPointer questPointer;

    public GameObject endFlag;
    public List<GameObject> roads;

    public List<GameObject> houses;

    public List<GameObject> roadStuff;
    public GameObject carttop;
    public GameObject[] cartbottoms;

    public GameObject[] food;

    public GameObject finishPoint;



    public GameObject borderTL;
    public GameObject borderTR;
    public GameObject borderBL;
    public GameObject Confiner;
    public GameObject borderBR;
    public GameObject borderL;
    public GameObject borderR;
    public GameObject borderT;
    public GameObject borderB;
    public int numRoads = 5;

    public float cartChance = 0.1f;
    public float lampChance = 0.4f;

    public GameObject tileParent;
    string seed;

    int[,] map;

    GameObject enemiesTop;
    void Start()
    {
        enemiesTop = new GameObject("Enemies");
        
        

        //each level is 25% bigger than the level before. a bit messy because of int conversion but doesn't matter
        width = (int)(widthWutliple * (1.0+LevelManager.level*0.25));
        height = (int)(heightMultiple * (1.0+LevelManager.level*0.25));


        questPointer = FindObjectOfType<QuestPointer>();
      

        GenerateMap();
    }

    void Update()
    {

        //for debugging:
        if (Input.GetMouseButtonDown(0))
        {
         //   GenerateMap();
           

            
        }
    }

    void GenerateMap()
    {
        map = new int[width, height];

        DrawBorders();

        MakeBoard();
        DrawRoads();

        AddFinishPoint();
        

    }

    private void AddFinishPoint()
    {
        int end_x = width - 5;
        int end_y = height - 5;

        CreateChildPrefab(finishPoint, end_x, end_y);
        CreateChildPrefab(endFlag, end_x, end_y);

        questPointer.SetQuestLocation(end_x, end_y);
    }

    //algorithm to make board
    void MakeBoard()
    {

        //set all map to 0
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = 0;
            }
        }



        //start and end position -5 from the end

        //build 4 roads
        for (int i = 0; i < numRoads; i++)
        {
            CreatePath(2, 2, height - 3, width - 3);
        }



    }


    void CreatePath(int xstart, int ystart, int xend, int yend)
    {
        int roadSize = UnityEngine.Random.Range(3, 6);

        int direction = 1; //1 for up, 0 for horizontal
        int roadLength = UnityEngine.Random.Range(5, 10);

        int currentX = xstart;
        int currentY = ystart;

        int count = 0;
        //keep roading until we get to the end
        // do if either (not at end) or (y not at end) is true. And count is less than 100 to stop infinite loops
        while ((currentX != xend | currentY != yend) & count < 100)
        {
            //increment count
            count++;
            if (count > 98)
            {
                //for debugging
                Debug.Log("something wrong with road drawing count = " + count);
            }
            //create road sfor whole road lenght. And whole roadsize
            // i is the length and j is the width of the road
            for (int i = 0; i < roadLength; i++)
            {
                for (int j = 0; j < roadSize; j++)
                {
                    try
                    {
                        //for direction is up
                        if (direction == 1)
                        {
                            //road size is going out to the right
                            map[currentX + j, currentY + i] = 1;
                        }
                        //for direction horizontal
                        else
                        {
                            //in order to stop breaks we go up with roadsize. draw it in your head!
                            map[currentX + i, currentY + j] = 1;
                        }
                    }
                    catch (Exception e)
                    {
                        //see if we are leaving the bounds
                        Debug.Log("oh  no! " + e.ToString());
                    }


                }
            }

            //now set current position to where we are
            if (direction == 1)
            {
                currentY = currentY + roadLength;
            }
            else
            {
                currentX = currentX + roadLength;
            }


            //set new roadlength
            if (direction == 1)
            {
                direction = 0;
                roadLength = Math.Min(UnityEngine.Random.Range(1, xend - currentX), xend - currentX);

                //need to make sure this doens't go over
                roadSize = Math.Min(UnityEngine.Random.Range(3, 6), yend - currentY);
            }
            else
            {
                direction = 1;

                //we need this bounding so that when we get to the target we don't start doing between 1 and 0
                // random acts differently when doing 0-1 and 1-2. 
                roadLength = Math.Min(UnityEngine.Random.Range(1, yend - currentY),yend - currentY);
                
                
                //need to make sure this doens't go over
                roadSize = Math.Min(UnityEngine.Random.Range(3, 6), xend - currentX);
            }

            
        }
    }

    void DrawRoads()
    {
        for (int i = 1; i < width - 1; i++)
        {
            for (int j = 1; j < height - 1; j++)
            {
                if (map[i, j] == 0)
                {

                    //now we want to only add certain ones
                    //at the top by a road
                    //CreateChildPrefab(wall, i, j);

                    //lets do wall first. anytime road below we do a wall. Three choices. road left right or none
                    if(map[i+1,j] == 1 & map[i,j-1] == 1)
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("wallr")) ,i,j);
                    }
                    else if(map[i-1,j] == 1 & map[i,j-1] == 1)
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("walll")) ,i,j);
                    }else if(map[i,j-1] == 1)
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("wallm")) ,i,j);
                    }
                    

                    //now for roof top. same logic again
                    else if(map[i,j+1] == 1 & map[i-1,j] == 1  & map[i+1,j] == 1)
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("rooftenc")) ,i,j);
                    }
                     else if(map[i,j+1] == 1 & map[i-1,j] == 1)
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("rooftl")) ,i,j);
                    }
                     else if(map[i,j+1] == 1 & map[i+1,j] == 1)
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("rooftr")) ,i,j);
                    }
                    else if(map[i,j+1] == 1)
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("rooft")) ,i,j);
                    }

                    //now the filler
                    else if(map[i+1,j] == 1 & map[i-1,j] == 1)
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("roofmenc")) ,i,j);
                    }
                    else if(map[i+1,j] == 1)
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("roofr")) ,i,j);
                    }
                    else if(map[i-1,j] == 1)
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("roofl")) ,i,j);
                    }
                    
                    else
                    {
                        CreateChildPrefab(houses.FindLast(x => x.name.Equals("roofm")) ,i,j);
                    }

                    
                }
                else if (map[i, j] == 1)
                {
                    //if at least one adjacent tile is a road, draw pavement (8 adjacent tiles so any less than 8 means a wall)
                    
                    if( 
                      //  ((map[i+1, j] == 1) & (map[i-1, j] == 0)) | 
                        //((map[i, j+1] == 1) & (map[i, j-1] == 0)) |
                        //((map[i, j-1] == 1) & (map[i, j+1] == 0)) |
                        //((map[i-1, j] == 1) & (map[i+1, j] == 0)) |
                        map[i+1, j] + map[i, j+1] + map[i-1, j] + map[i, j-1] + map[i+1, j+1] + map[i+1, j-1] + map[i-1, j+1] + map[i-1, j-1]  < 8
                        )
                    {
                        CreateChildPrefab(roads.FindLast(x => x.name.Equals("pavement")), i, j);

                        DrawPavementStuff(i,j);
                    }else{
                        CreateChildPrefab(roads.FindLast(x => x.name.Equals("road")), i, j);

                        DrawRoadStuff(i,j);
                    }
                    
                }

            }
        }
    }

    //if we are on a road (not a pavement) then draw
    void DrawRoadStuff(int x, int y)
    {

    }

    void DrawPavementStuff(int x, int y)
    {

        //we only draw enemies beyond the player immediate vicinity. hardcoded

        if(x < 8 | y < 8)
        {
            return;
        }

        //we only draw enemies on the pavement
        //the equation we use for enemy probability is 1 - (enemyExponent+1)^(-1*Level)
        float chance = 1 - Mathf.Pow(enemyExponent+1, -1* LevelManager.level);
        

        if( UnityEngine.Random.Range(0, 1.0f) < chance){

            CreateChildPrefabInner(enemies[UnityEngine.Random.Range(0, enemies.Length)], enemiesTop, x, y);
           
            

            //don't add anything else to same pavement
            return;
        }

        //shall we draw a cart? if there is space and if the chance works
        if(map[x, y+1] == 1)
        {
            if( UnityEngine.Random.Range(0, 1.0f) < cartChance){
                CreateChildPrefab(carttop, x, y+1);
                CreateChildPrefab(cartbottoms[UnityEngine.Random.Range(0, cartbottoms.Length)], x, y);

                //what is the car selling?
                if(map[x+1, y] == 1)
                {
                    CreateChildPrefab(food[UnityEngine.Random.Range(0, food.Length)], x+1, y);
                }
                else if (map[x-1, y] == 1)
                {
                    CreateChildPrefab(food[UnityEngine.Random.Range(0, food.Length)], x-1, y);
                }

                //don't add anything else if there is a cart
                return;
            }
        }

        //shall we draw a lamppost?
       /* if(map[x, y+1] == 1)
        {
            if( UnityEngine.Random.Range(0, 1.0f) < lampChance){
                CreateChildPrefab(houses.FindLast(b => b.name.Equals("lampb")) ,x,y);
                CreateChildPrefab(houses.FindLast(b => b.name.Equals("lampm")) ,x,y+1);
                CreateChildPrefab(houses.FindLast(b => b.name.Equals("lampt")) ,x,y+2);


                //don't add anything else if there is a cart
                return;
            }
        }*/

    }

    void DrawBorders()
    {

        //draw the border
        for (int x = 1; x < width - 1; x++)
        {
            CreateChildPrefab(borderB, x, 0);
            CreateChildPrefab(borderT, x, height - 1);
        }

        for (int y = 1; y < height - 1; y++)
        {
            CreateChildPrefab(borderL, 0, y);
            CreateChildPrefab(borderR, width - 1, y);
        }


        //draw the corners
        CreateChildPrefab(borderTL, 0, height - 1);
        CreateChildPrefab(borderTR, width - 1, height - 1);
        CreateChildPrefab(borderBL, 0, 0);
        CreateChildPrefab(borderBR, width - 1, 0);



        //draw the polygon for camera holding

        var bc = Confiner.GetComponent<PolygonCollider2D>();
        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(0, 0);
        points[1] = new Vector2(0, height - 1);
        points[2] = new Vector2(width - 1, height - 1);
        points[3] = new Vector2(width - 1, 0);
        points[4] = new Vector2(0, 0);
        bc.SetPath(0, points);
        // Vector2[] polygonPoints = new Vector2[4];
    }



    void CreateChildPrefab(GameObject prefab, int x, int y)
    {
        CreateChildPrefabInner(prefab, tileParent, x, y);
    }
   

    void CreateChildPrefabInner(GameObject prefab, GameObject parent, int x, int y)
    {
        var myPrefab = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        myPrefab.transform.parent = parent.transform;

    }

}
