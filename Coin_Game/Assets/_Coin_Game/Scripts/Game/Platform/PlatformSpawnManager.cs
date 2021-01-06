using System.Collections.Generic;
using UnityEngine;

namespace _Coin_Game.Scripts.Game.Platform
{
   public class PlatformSpawnManager : MonoBehaviour
   {
      [SerializeField] private int maxNumberOfPlatform;
      [SerializeField] private ObstaclesPlatform startPlatformPrefab;
      [SerializeField] private ObstaclesPlatform endPlatformPrefab;
      [SerializeField] private List<ObstaclesPlatform> platformPrefabList;

      public List<ObstaclesPlatform> PlatformList => platformPrefabList;

      private void Awake()
      {
         InstantiatePlatform();
      }


      private void InstantiatePlatform()
      {
         ObstaclesPlatform firstPlatform = Instantiate(startPlatformPrefab, transform);
         ObstaclesPlatform secondPlatform;

         for (int i = 0; i < maxNumberOfPlatform; i++)
         {
            
            secondPlatform = Instantiate(platformPrefabList[Random.Range(0,platformPrefabList.Count)], transform);
            secondPlatform.StartTarget.parent = null;
            secondPlatform.transform.parent = secondPlatform.StartTarget;
            secondPlatform.transform.position = firstPlatform.EndTarget.position;
            
            firstPlatform = secondPlatform;
         }
         
         secondPlatform = Instantiate(endPlatformPrefab, transform);
         secondPlatform.StartTarget.parent = null;
         secondPlatform.transform.parent = secondPlatform.StartTarget;
         secondPlatform.transform.position = firstPlatform.EndTarget.position;
         

         
         
         

      }
      
   }
}
