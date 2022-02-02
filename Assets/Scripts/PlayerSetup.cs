using UnityEngine;
using Mirror;
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;
    // Start is called before the first frame update
    void Start()
    {
        if(!isLocalPlayer)
        {
            for (int i=0;i<componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        } 
        else 
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
   /*  void Update()
    {
        
    } */
}
