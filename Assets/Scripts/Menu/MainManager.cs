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

    public static bool Completed_0_5 = false;
    public static bool Completed_1_5 = false;
    public static bool Completed_2_5 = false;
    public static bool Completed_3_5 = false;
    public static bool Completed_4_5 = false;
    public static bool isLevel2Success = false;


    public static bool Triangle = false;
    public static bool Circle = false;
    static int countlevel = 0;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
       if(isLevel2Success==true &&countlevel==0)
       {
            //DESTROYED
            isLevel2Success = false;
            Completed_0_5 = true;
            countlevel++;

       }

        if (isLevel2Success == true && countlevel == 1)
        {
            //BROWN ENTRY OPENED
            isLevel2Success = false;
            Completed_1 = true;
            countlevel++;
        }

        if (isLevel2Success == true && countlevel == 2)
        {   //DESTROYED
            isLevel2Success = false;
            Completed_1_5 = true;
            countlevel++;

        }

        if (isLevel2Success == true && countlevel == 3)
        {
            //PINK ENTRY OPENED
            isLevel2Success = false;
            Completed_2 = true;
            countlevel++;
        }
        if (isLevel2Success == true && countlevel == 4)
        {   //DESTROYED
            isLevel2Success = false;
            Completed_2_5 = true;
            countlevel++;

        }

        if (isLevel2Success == true && countlevel == 5)
        {
            //BLUE ENTRY OPENED
            isLevel2Success = false;
            Completed_3 = true;
            countlevel++;
        }
        if (isLevel2Success == true && countlevel == 6)
        {   //DESTROYED
            isLevel2Success = false;
            Completed_3_5 = true;
            countlevel++;

        }
        if (isLevel2Success == true && countlevel == 7)
        {
            //ORANGE ENTRY OPENED
            isLevel2Success = false;
            Completed_4 = true;
            countlevel++;
        }
        if (isLevel2Success == true && countlevel == 8)
        {   //DESTROYED
            isLevel2Success = false;
            Completed_4_5 = true;
            countlevel++;

        }
        if (isLevel2Success == true && countlevel == 9)
        {
            //ORANGE ENTRY OPENED
            isLevel2Success = false;
            Completed_5 = true;
        }
    }
}
