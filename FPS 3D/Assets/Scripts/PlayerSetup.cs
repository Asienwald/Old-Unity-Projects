using UnityEngine;
using Mirror;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    Camera sceneCam;

    private void Start()
    {
        // if this obj not controlled by system
        // disable all components not controlled by local player so inputs dont clash
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
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

        GetComponent<Player>().Setup();
    }

    // called everytime a client is set up locally
    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, _player);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
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

        GameManager.UnregisterPlayer(transform.name);
    }
}
