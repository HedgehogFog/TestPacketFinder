using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerGameManager : NetworkBehaviour
{
    static public List<NetworkPlayerHelper> sPlayers = new List<NetworkPlayerHelper>();
    static public ServerGameManager sInstance = null;

    public List<Transform> pointSpawns;

    [Header("Gameplay")] public GameObject[] mobPrefab;


    void Awake()
    {
        sInstance = this;
    }


    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            StartCoroutine(MobCoroutine());
        }
    }
    
    public override void OnStartClient()
    {
        base.OnStartClient();

        foreach (GameObject obj in mobPrefab)
        {
            ClientScene.RegisterPrefab(obj);
        }
    }

    private IEnumerator MobCoroutine()
    {
        const float MIN_TIME = 1.0f;
        float MAX_TIME = 10.0f;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MIN_TIME, MAX_TIME));

            Vector3 position = pointSpawns[Random.Range(0, pointSpawns.Count)].position;

            GameObject mob = Instantiate(mobPrefab[Random.Range(0, mobPrefab.Length)], position,
                Quaternion.Euler(Random.value * 360.0f, Random.value * 360.0f, Random.value * 360.0f)) as GameObject;


            NetworkServer.Spawn(mob);

            if (MAX_TIME >= 1.0f)
                MAX_TIME -= 0.1f;
        }
    }
}