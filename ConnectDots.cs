using UnityEngine;
using System.Collections;

public class ConnectDots : MonoBehaviour
{
	GameObject[] dots;
	int nextPosToConnect;

	// Use this for initialization
	void Start ()
	{
		dots = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			dots [i] = transform.GetChild (i).gameObject;
		}
		nextPosToConnect = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (nextPosToConnect <= dots.Length - 1) {
			dots [nextPosToConnect].GetComponent<Renderer> ().material.color = Color.blue;
		}
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.tag == "dot") {
					for (int i = 0; i < dots.Length; i++) {
						if (hit.transform.gameObject == dots [i] && nextPosToConnect == i) {
							dots [i].GetComponent<Renderer> ().material.color = Color.red;
							if (i >= 1) {
								LineRenderer newLine = dots [i].AddComponent<LineRenderer> ();
								newLine.material = new Material(Shader.Find("Particles/Multiply"));
								newLine.SetWidth(0.2F, 0.2F);
								Color cLine1 = Color.yellow;
								//Color cLine2 = Color.yellow;
								newLine.SetColors(cLine1, cLine1);
								newLine.SetPosition (0, dots [i - 1].transform.position);
								newLine.SetPosition (1, dots [i].transform.position);
							}
							if (nextPosToConnect < dots.Length) {
								nextPosToConnect ++;
							}
							break;
						}

					}
				}
			}
		}
	}
}
