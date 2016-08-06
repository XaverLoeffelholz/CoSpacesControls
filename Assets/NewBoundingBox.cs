using UnityEngine;
using System.Collections;

public class NewBoundingBox : MonoBehaviour {

	public GameObject[] coordinates;
	public Vector3[] coordinatesBoundingBox;
	public GameObject linesPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DeleteBoundingBox(){
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}
	}

	public void DrawBoundingBox(){	
		DeleteBoundingBox ();

		CalculateBoundingBox ();	
		GameObject linesGO = Instantiate(linesPrefab);
		linesGO.transform.SetParent(transform);

		Lines lines = linesGO.GetComponent<Lines> ();

		lines.DrawLinesWorldCoordinate(new Vector3[] {coordinatesBoundingBox[0], coordinatesBoundingBox[1], coordinatesBoundingBox[2], coordinatesBoundingBox[3]});
		lines.DrawLinesWorldCoordinate(new Vector3[] {coordinatesBoundingBox[4], coordinatesBoundingBox[5], coordinatesBoundingBox[6], coordinatesBoundingBox[7]});

		lines.DrawLinesWorldCoordinate(new Vector3[] {coordinatesBoundingBox[0], coordinatesBoundingBox[4]});
		lines.DrawLinesWorldCoordinate(new Vector3[] {coordinatesBoundingBox[1], coordinatesBoundingBox[5]});
		lines.DrawLinesWorldCoordinate(new Vector3[] {coordinatesBoundingBox[2], coordinatesBoundingBox[6]});
		lines.DrawLinesWorldCoordinate(new Vector3[] {coordinatesBoundingBox[3], coordinatesBoundingBox[7]});
	}

	public void CalculateBoundingBox()
	{
		coordinatesBoundingBox = new Vector3[8];

		// get highest and lowest values for x,y,z
		Vector3 minima = GetBoundingBoxMinima();
		Vector3 maxima = GetBoundingBoxMaxima();

		// set all points
		coordinatesBoundingBox[0] = new Vector3(maxima.x, maxima.y, maxima.z);
		coordinatesBoundingBox[1] = new Vector3(maxima.x, maxima.y, minima.z);
		coordinatesBoundingBox[2] = new Vector3(minima.x, maxima.y, minima.z);
		coordinatesBoundingBox[3] = new Vector3(minima.x, maxima.y, maxima.z);

		coordinatesBoundingBox[4] = new Vector3(maxima.x, minima.y, maxima.z);
		coordinatesBoundingBox[5] = new Vector3(maxima.x, minima.y, minima.z);
		coordinatesBoundingBox[6] = new Vector3(minima.x, minima.y, minima.z);
		coordinatesBoundingBox[7] = new Vector3(minima.x, minima.y, maxima.z);
	}


	public Vector3 GetBoundingBoxMinima()
	{
		Vector3 minima = new Vector3 (9999f, 9999f, 9999f);

		for (int i = 0; i < coordinates.Length; i++)
		{
			Vector3 current = coordinates[i].transform.position;

			if (current.x < minima.x) {
				minima.x = current.x;
			}

			if (current.y < minima.y) {
				minima.y = current.y;
			}

			if (current.z < minima.z) {
				minima.z = current.z;
			}

		}
		return minima;
	}


	public Vector3 GetBoundingBoxMaxima()
	{
		Vector3 maxima = new Vector3 (-9999f, -9999f, -9999f);

		for (int i = 0; i < coordinates.Length; i++)
		{
			Vector3 current = coordinates[i].transform.position;

			if (current.x > maxima.x) {
				maxima.x = current.x;
			}

			if (current.y > maxima.y) {
				maxima.y = current.y;
			}

			if (current.z > maxima.z) {
				maxima.z = current.z;
			}

		}
			
		return maxima;
	}

}