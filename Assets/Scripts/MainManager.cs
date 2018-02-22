using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static bool Completed_1 = false;
    public static bool Completed_2 = false;
    public static bool Completed_3 = false;
    public static bool Completed_4 = false;
    public static bool Completed_5 = false;


    public static bool isLevel2Success = false;

    static int countlevel =0;

    void Start ()
    {
       
        DontDestroyOnLoad(this);
	}
	
	
	void Update ()
    {
        if (isLevel2Success==true&&countlevel==0)
        {
            //BROWN ENTRY
            isLevel2Success = false;
            Completed_1 = true;
            countlevel++;

        }
        if (isLevel2Success == true && countlevel == 1)
        {
            //PINK ENTRY
            isLevel2Success = false;
            Completed_2 = true;
            countlevel++;

        }
        if (isLevel2Success == true && countlevel == 2)
        {
            //BLUE ENTRY
            isLevel2Success = false;
            Completed_3 = true;
            countlevel++;

        }
        if (isLevel2Success == true && countlevel == 3)
        {
            //ORANGE ENTRY
            isLevel2Success = false;
            Completed_4 = true;
            countlevel++;

        }
        if (isLevel2Success == true && countlevel == 4)
        {
            //ORANGE ENTRY
            isLevel2Success = false;
            Completed_5 = true;
            

        }




    }
}
