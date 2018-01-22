using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooter : NetworkBehaviour
{
	private GameObject mazlePrefab;

	private RaycastHit hit;

	private Camera _camera;
	private void Start()
	{
		mazlePrefab = Resources.Load<GameObject>("Prefab/Mazle");
		_camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;

		if (Input.GetMouseButton(0))
		{
			if (Physics.Raycast(_camera.transform.position, Vector3.forward, out hit, 500))
			{
				NetworkPlayerHelper playerHelper = hit.collider.GetComponentInParent<NetworkPlayerHelper>();
				Debug.Log(hit.collider.name + ": " + playerHelper);
				if (playerHelper)
				{
					playerHelper.CmdDamage();
				}
				else
				{
					Quaternion hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

					GameObject mazle = Instantiate(mazlePrefab, hit.point + (hit.normal * 0.0001f), hitRotation);
					mazle.transform.parent = hit.transform;
					Destroy(mazle.gameObject, 3);
				}
				
			}
		}
	}
}
