using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public static Spawner instance;

    public GameObject triangle;
    public GameObject circle;
    public ArrayList[] Enemies;
    private int totalEnemies;
   
    private void Awake ()
    {
        if (instance != null)
            instance = this;
        else Destroy(gameObject);
    }
	
	private void Start ()
    {
        
		
	}
    private void Spawn()
    {
        for(int i=0;i<Enemies.Length;i++)
        {


        }

    }
}
