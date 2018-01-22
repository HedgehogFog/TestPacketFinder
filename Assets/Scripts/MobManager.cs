using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MobManager : NetworkBehaviour
{
    private Vector3 target;

    private float dir = 15f;
    public float speed = 1f;

    private void Start()
    {
        target = transform.position;
        target.y += dir;
    }

    private void Update()
    {
        var x = Mathf.Lerp(transform.position.x, target.x, Time.deltaTime * speed);
        var y = Mathf.Lerp(transform.position.y, target.y, Time.deltaTime * speed);
        var z = Mathf.Lerp(transform.position.z, target.z, Time.deltaTime * speed);

        transform.position = new Vector3(x, y, z);
        Vector3 relativePos = (target - transform.position) * Time.deltaTime * speed;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;

        if (Vector3.Distance(transform.position, target) <= 3f)
        {
            dir = -dir;
            target.y += dir;
        }
    }

    [ServerCallback]
    void OnCollisionEnter(Collision collision)
    {
        NetworkServer.Destroy(gameObject);
    }
}