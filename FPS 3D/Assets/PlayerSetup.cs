using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCam;

    private void Start()
    {
        // if this obj not controlled by system
        // disable all components not controlled by local player so inputs dont clash
        if (!isLocalPlayer)
        {
            for(int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            // disable main cam when player join
            sceneCam = Camera.main;
            if(sceneCam != null)
            {
                sceneCam.gameObject.SetActive(false);
            }
        }
    }

    // called when object is disabled/destroyed
    private void OnDisable()
    {
        // renable scene cam when player leave
        if(sceneCam != null)
        {
            sceneCam.gameObject.SetActive(true);
        }
    }
}
