using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MobAngryManager : NetworkBehaviour
{


	public float timeLife = 10f;
	private float _timeLife;
	
	[ServerCallback]
	private void Update()
	{
		if (_timeLife >= timeLife)
		{
			NetworkServer.Destroy(gameObject);
		}
		else
		{
			_timeLife += Time.deltaTime;	
		}
	}

	[ServerCallback]
	void OnCollisionEnter(Collision collision)
	{
		NetworkPlayerHelper player = collision.gameObject.GetComponentInParent<NetworkPlayerHelper>();
		if (player)
		{
			player.CmdDamage();
			NetworkServer.Destroy(gameObject);
		}
		
	}
}
