﻿using UnityEngine;
using System.Collections;

public class holemaker : MonoBehaviour {

	public Terrain myTerrain;
	TerrainData tData;
	int xResolution;
	int zResolution;
	int xSize = 1;
	int zSize = 1;
	public float speed = 0.05f;
	float [,] heights;

	
	// Use this for initialization
	void Start () {
		tData = myTerrain.terrainData;
		xResolution = tData.heightmapWidth;
		zResolution = tData.heightmapHeight;
		heights = tData.GetHeights (0, 0, xResolution, zResolution);

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton (0)) 
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit))
			{
				lowerTerrain(hit.point);

			}
		}
	}
	
	private void lowerTerrain(Vector3 point)
	{
		int mouseX = (int)((point.x / tData.size.x) * xResolution);
		int mouseZ = (int)((point.z / tData.size.z) * zResolution);
		float[,] modHeights = new float [xSize,zSize];
		float y = heights [mouseX, mouseZ];
		y -= speed * Time.deltaTime;
		if (y < 0.0f)
			y = 0.0f;
		modHeights [0, 0] = y;
		heights [mouseX, mouseZ] = y;
		tData.SetHeights (mouseX, mouseZ, modHeights);
		
	}
}