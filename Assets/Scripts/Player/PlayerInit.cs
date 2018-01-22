using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInit : NetworkBehaviour
{

	[SerializeField] private Camera _camera;
	[SerializeField] private AudioListener _listener;
	[SerializeField] private PlayerControll _controll;
	[SerializeField] private GameObject _canvas;

	public override void OnStartLocalPlayer()
	{
		_camera.enabled = true;
		_listener.enabled = true;
		_controll.enabled = true;
		_canvas.SetActive(true);
	}
}
