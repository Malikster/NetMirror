using Mirror;
using UnityEngine;


public class Player : NetworkBehaviour
{
    [SerializeField]
    float speedModifier = 0.1f;
    void HandleMovement()
    {
        if(isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * speedModifier, moveVertical * speedModifier, 0);
            transform.position = transform.position + movement;
        }
    }

    void Update()
    {
        HandleMovement();
    }


}
