﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
 
[DisallowMultipleComponent]
public class Line : MonoBehaviour
{
		/// <summary>
		/// The width of the line renderer.
		/// </summary>
		private float lineWidth;
	private float lineHeight;

		/// <summary>
		/// The material of the line renderer.
		/// </summary>
		private Material lineMaterial;

		/// <summary>
		/// The line texure.
		/// </summary>
		public Texture2D lineTexture;

		/// <summary>
		/// The Points of the Line .
		/// </summary>
		private List<Vector3>points = new List<Vector3>();
	private List<RectTransform> rects = new List<RectTransform>();

		/// <summary>
		/// The line pieces.
		/// A line piece is a line renderer contains only two points
		/// </summary>
		//private List<LineRenderer> linePieces;

	private List<Image> linePieces;

		/// <summary>
		/// The temp line piece game object.
		/// </summary>
		private GameObject tempLinePieceGameObject;
	private RectTransform linePieceRect;

		/// <summary>
		/// The temp line piece line renderer.
		/// </summary>
	private Image tempLinePieceImage;

		/// <summary>
		/// Temporary sprite renderer.
		/// </summary>
	private Image tempSpriteRenderer;

		/// <summary>
		/// The number of points in the line.
		/// </summary>
		private int numberOfPoints;

		/// <summary>
		/// The temp first point.
		/// </summary>
		private Vector3 tempFirstPoint;

		/// <summary>
		/// The temp second point.
		/// </summary>
		private Vector3 tempSecondPoint;

		/// <summary>
		/// The traced path in the Grid for the line.
		/// Contains the indexes of the traced grid cells
		/// </summary>
		public List<int> path;

		/// <summary>
		/// Whether the line is completed (correctly connected between the pairs).
		/// </summary>
		public bool completedLine;

		/// <summary>
		/// Line index.
		/// </summary>
		public int index;

		/// <summary>
		/// The line piece prefab.
		/// </summary>
		public GameObject linePiecePrefab;
	public GridCell gridCell;

		void Start ()
		{
				///Initiate instances
				points = new List<Vector3> ();
		rects = new List<RectTransform> ();
		linePieces = new List<Image> ();
				path = new List<int> ();
		}

		/// <summary>
		/// Add a point to the line.
		/// </summary>
		/// <param name="point">Vector3 Point.</param>
	public void AddPoint (RectTransform cellTransform)
		{
		Vector3 point = cellTransform.position;
        if (point == null) Debug.LogError("passed point is null!");

				///If the given point already exists ,then skip it
				if (points.Contains (point)) {
            Debug.Log("point already exists??");
						return;
				}

		if (rects.Contains (cellTransform)) {
			Debug.Log("rect already exists??");
			return;
		}

				///Increase the number of points in the line
				numberOfPoints++;
				///Add the point to the points list
				points.Add (point);
		rects.Add (cellTransform);
        Debug.Log("number of Points! : " + numberOfPoints + " | points count : " + points.Count);
        ///Create new line piece
        if (points.Count > 1) {
            
						tempFirstPoint = points [points.Count - 2];
						tempSecondPoint = points [points.Count - 1];
			RectTransform firstRect = rects [points.Count - 2];
			RectTransform secondRect = rects [points.Count - 1];
						//Create Line Piece
			tempLinePieceGameObject = Instantiate (linePiecePrefab, transform.position, transform.rotation) as GameObject;
						tempLinePieceGameObject.transform.parent = transform;
						tempLinePieceGameObject.name = "LinePiece-[" + (numberOfPoints - 2) + "," + (numberOfPoints - 1) + "]";
			tempLinePieceImage = tempLinePieceGameObject.GetComponent<Image> ();
						tempLinePieceImage.material = lineMaterial;
			linePieceRect = tempLinePieceGameObject.GetComponent<RectTransform> ();
			tempLinePieceImage.GetComponent<RectTransform> ().sizeDelta = new Vector2 (lineWidth, lineWidth);
						//tempLinePieceImage.SetWidth (lineWidth, lineWidth);
						//tempLinePieceImage.SetVertexCount (2);

						//Fixing LineRenderer point x-position to make line pieces connected
						if (tempSecondPoint.x > tempFirstPoint.x) {
								tempSecondPoint.x += lineWidth / 2.0f;
								tempFirstPoint.x -= lineWidth / 2.0f;

						} else if (tempSecondPoint.x < tempFirstPoint.x) {
								tempSecondPoint.x -= lineWidth / 2.0f;
								tempFirstPoint.x += lineWidth / 2.0f;
						}

						//Fixing LineRenderer point y-position to make line pieces connected
						if (tempSecondPoint.y > tempFirstPoint.y) {
								tempSecondPoint.y += lineWidth / 2.0f;
								tempFirstPoint.y -= lineWidth / 2.0f;
				
						} else if (tempSecondPoint.y < tempFirstPoint.y) {
								tempSecondPoint.y -= lineWidth / 2.0f;
								tempFirstPoint.y += lineWidth / 2.0f;
						}
			Vector3 firstLocalPoint = firstRect.localPosition;
			Vector3 secondLocalPoint = secondRect.localPosition;
			SetPosition (tempFirstPoint, tempSecondPoint);
			SetRotation (firstLocalPoint, secondLocalPoint);
						//tempLinePieceImage.SetPosition (0, tempFirstPoint);//first point
						//tempLinePieceImage.SetPosition (1, tempSecondPoint);//second point
						///Add the line picece to the list
						linePieces.Add (tempLinePieceImage);
				}
		}

	/// <summary>
	/// Sets the position of the image.
	/// </summary>
	public void SetPosition (Vector3 firstPoint, Vector3 secondPoint) {
		linePieceRect.localScale = Vector3.one;
		linePieceRect.position = firstPoint + (secondPoint - firstPoint) / 2;
	}

	public void SetRotation (Vector3 firstPoint, Vector3 secondPoint) {
		print ("First y " + firstPoint.y);
		print ("Second y " + secondPoint.y);
		float delta = Mathf.Abs (firstPoint.y - secondPoint.y);
		if (delta == 0.0f) {
			linePieceRect.localEulerAngles = new Vector3 (0, 0, 90);
			linePieceRect.localScale = new Vector3 (0.35f, 1.0f, 1.0f);
		} else {
			linePieceRect.localScale = new Vector3 (1.0f, 0.45f, 1.0f);
		}
	}

		/// <summary>
		/// Sets the color of the line.
		/// </summary>
		/// <param name="lineColor">Line color.</param>
		public void SetColor (Color lineColor)
		{
				if (lineMaterial == null) {
						///Create the material of the line
						lineMaterial = new Material (Shader.Find ("Sprites/Default"));
						lineMaterial.mainTexture = lineTexture;
				}
				///setting up the color of the material
				lineMaterial.color = lineColor;
		}

		/// <summary>
		/// Set the width of the line.
		/// </summary>
		/// <param name="lineWidth">Line width.</param>
		public void SetWidth (float lineWidth)
		{
				this.lineWidth = lineWidth;
		}

		/// <summary>
		/// Get the first path element.
		/// </summary>
		/// <returns>The first path element.</returns>
		public int GetFirstPathElement ()
		{
				if (path.Count == 0) {
						return -1;
				}
				return path [0];
		}

		/// <summary>
		/// Get the last path element.
		/// </summary>
		/// <returns>The last path element.</returns>
		public int GetLastPathElement ()
		{
				if (path.Count == 0) {
						return -1;
				}
				return path [path.Count - 1];
		}

		/// <summary>
		/// Clear the path.
		/// </summary>
		public void ClearPath ()
		{
				numberOfPoints = 0;
				for (int i = 0; i < linePieces.Count; i++) {
						GameObject.Destroy (linePieces [i].gameObject);
				}

				linePieces.Clear ();
				points.Clear ();
		rects.Clear ();
				Transform gridCellContent = null;

				for (int i = 0; i < path.Count; i++) {
						gridCell = PuzzleManager.gridCells [path [i]];
						if (i == 0 || i == path.Count - 1) {
								//Reset elements(dots) pair sprites
								gridCellContent = GameObjectUtil.FindChildByTag (gridCell.transform, "GridCellContent");
								if (gridCellContent != null) {
										gridCellContent.GetComponent<SpriteRenderer> ().sprite = PuzzleManager.currentLevel.dotsPairs [gridCell.elementPairIndex].sprite;
								}
						}
						ResetGridCell (gridCell);
				}

				completedLine = false;
				path.Clear ();
		}

		//Remove the line from an index to the end of the list
		public void RemoveElements (int gridCellIndex)
		{
				///Find the index of the grid cell in the path
				int fromIndex = -1, toIndex = path.Count - 1;
				for (int i = 0; i < path.Count; i++) {
						if (path [i] == gridCellIndex) {
								fromIndex = i;
								break;
						}
				}

				int linePieceIndex;
				for (int i = toIndex; i>fromIndex; i--) {//from last to the first
						linePieceIndex = i - 1;//setting up line piece index
						ResetGridCell (PuzzleManager.gridCells [path [i]]);//reset the grid cell
						points.RemoveAt (i);//remove point from points list
						if (linePieceIndex >= 0 && linePieceIndex < linePieces.Count) {
								GameObject.Destroy (linePieces [linePieceIndex].gameObject);//destroy line piece
								linePieces.RemoveAt (linePieceIndex);//remove line piece reference
						}
						path.RemoveAt (i);//remove grid cell reference from the path
				}

				numberOfPoints = points.Count;
		}

		/// <summary>
		/// Reset the grid cell.
		/// </summary>
		/// <param name="gridcell">Grid cell instance.</param>
		public void ResetGridCell (GridCell gridcell)
		{
				gridcell.Reset ();
				tempSpriteRenderer = gridcell.transform.Find ("background").GetComponent<Image> ();
				tempSpriteRenderer.color = Color.white;
				tempSpriteRenderer.enabled = false;
		}
}