using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    int x;
    int y;
    Vector3 toPos;

    GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    
        Vector3 cappedScreenPos = Camera.main.WorldToScreenPoint(toPos);
        bool offScreen = cappedScreenPos.x >= Screen.width | cappedScreenPos.y >= Screen.height;
        if(offScreen)
        {
            if(cappedScreenPos.x >= Screen.width)
            {
                cappedScreenPos.x = Screen.width - 100;
            }
            if(cappedScreenPos.y >= Screen.height)
            {
                cappedScreenPos.y = Screen.height - 50;
            }
        }
        else{
            cappedScreenPos.x += - 50;
            cappedScreenPos.y += - 50;
        }

        transform.position = transform.position +  (cappedScreenPos - transform.position)*0.5f*Time.deltaTime;


        var worldPos =  Camera.main.ScreenToWorldPoint(transform.position) ;

        Vector3 dir = toPos - worldPos;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

       
    }

    public void SetQuestLocation(int x_t, int y_t)
    {
        x = x_t;
        y = y_t;
        toPos = new Vector3(x,y,0);
    }
}
