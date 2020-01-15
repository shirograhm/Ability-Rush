using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

public class EnemyMovement : MonoBehaviour
{
   public GameObject playerPrefab;
   public float speed;

   private GameObject playerInstance;
   

   // Start is called before the first frame update
   void Start()
   {
      playerInstance = GetPlayerPrefabInstance(playerPrefab);
      if(playerInstance == null) Debug.LogError("Player not instantiated.");
   }

   // Update is called once per frame
   void Update()
   {
      Vector3 offsetFromPlayer = playerInstance.transform.position - transform.position;

      transform.Translate(offsetFromPlayer.normalized * Time.deltaTime * speed);
   }

   GameObject GetPlayerPrefabInstance(Object myPrefab)
   {
      GameObject[] allObjects = (GameObject[]) FindObjectsOfType(typeof(GameObject));
      // Iterate through all objects to find player
      foreach(GameObject obj in allObjects) {
         if (obj.tag == "Player") {
            return obj;
         }
      }
      return new GameObject();
   }
}
