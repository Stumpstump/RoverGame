﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextMeshRenderLayering : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<MeshRenderer> ().sortingLayerName = "PuzzleForeground";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}