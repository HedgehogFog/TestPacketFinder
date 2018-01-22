using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;	
using UnityEngine.UI;

public class NetworkPlayerHelper : NetworkBehaviour
{

	[SyncVar] public string playerName;
	[SyncVar] public Color playerColor;
	[SerializeField] private Text _text;

	[SyncVar(hook = "OnHealthChanged")] public int playerHealth;


	void OnHealthChanged(int newValue)
	{
		playerHealth = newValue;
		UpdateHealth();
	}

	private void UpdateHealth()
	{
		_text.text = "" + playerHealth;
	}

	[Command]
	public void CmdDamage()
	{
		
		Debug.Log(playerName + ":" + playerHealth);
		playerHealth--;

		if (playerHealth <= 0)
		{
			//we start the coroutine on the manager, as disabling a gameobject stop ALL coroutine started by it
//			NetworkGameManager.sInstance.StartCoroutine(NetworkGameManager.sInstance.WaitForRespawn(this));
		}
	
	}
	
	[ClientRpc]
	void RpcDamage()
	{
		//Ка бы нужен Destroy =>
//		LocalDamage();
	}
}
