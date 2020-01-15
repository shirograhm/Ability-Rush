using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public float speed;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput = Input.GetAxis("Vertical");

      Vector3 horizontalMovement = Vector3.right * horizontalInput;
      Vector3 verticalMovement = Vector3.forward * verticalInput;

      Vector3 timeAdjustedNormalized = (horizontalMovement + verticalMovement).normalized * Time.deltaTime * speed;

      transform.Translate(timeAdjustedNormalized);
   }
}
