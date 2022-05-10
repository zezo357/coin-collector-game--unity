using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSpwaner : MonoBehaviour
{
     public GameObject coin;
     public int xPosRangeStart=0;
     public int xPosRangeEnd=1600;
     public int yPos=100;
     public int zPosRangeStart=0;
     public int zPosRangeEnd=1600;
     public int coinlimit = 100;
     public int coinCount = 1;


     
     void Start()
     {
     }
 
     void Update()
     {
         if (coinCount < coinlimit){
             SpawnCoins();
         }
     }
 
 
     void SpawnCoins()
     {
         while (coinCount < coinlimit)
         {
             //Debug.Log("Coin spawned!");
             int xPos = Random.Range(xPosRangeStart, xPosRangeEnd);
             int zPos = Random.Range(zPosRangeStart, zPosRangeEnd);
             Instantiate(coin, new Vector3(xPos, yPos, zPos), Quaternion.identity);
             coinCount += 1;
         }
     }
       
     
}
