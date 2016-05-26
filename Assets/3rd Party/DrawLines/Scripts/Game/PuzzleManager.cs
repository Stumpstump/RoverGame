﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Sol;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268

#pragma warning disable 0168 // variable declared but not used.

[DisallowMultipleComponent]
public class PuzzleManager : MonoBehaviour
{
    public AudioClip puzzleCompleteEffect;
    public AudioClip lineDrawEffect;

	/// <summary>
	/// World Space attributes
	/// </summary>
	public bool WorldSpacePuzzle;
	public GameObject contentCellPrefab;
	GameObject puzzleCanvas;
	GridLayoutGroup contentsGrid;
	RectTransform worldLinesTransform;
	public GameObject worldLinePrefab;
	private Sprite connectedSprite;

	/// <summary>
	/// 
	/// </summary>
    public Camera mainCamera;
		/// <summary>
		/// The grid cell prefab.
		/// </summary>
		public GameObject gridCellPrefab;
		[Range(0,1)]
		/// <summary>
	   /// The grid cell top background alpha.
	   /// </summary>
		public float gridCellTopBackgroundAlpha = 0.5f;

		/// <summary>
		/// The grid cell z-position.
		/// </summary>
		[Range(-20,20)]
		public float gridCellZPosition = 0;
	
		/// <summary>
		/// The grid line prefab.
		/// </summary>
		public GameObject gridLinePrefab;
	
		/// <summary>
		/// The grid line z-position.
		/// </summary>
		[Range(-50,50)]
		public float gridLineZPosition = -2;
	
		/// <summary>
		/// The grid line width factor.
		/// </summary>
		[Range(0,10)]
		public float gridLineWidthFactor = 0.5f;

		/// <summary>
		/// The cell content prefab.
		/// </summary>
		public GameObject cellContentPrefab;

		/// <summary>
		/// The cell content scale factor.
		/// </summary>
		[Range(0.1f,1)]
		public float cellContentScaleFactor = 0.6f;

		/// <summary>
		/// The cell content z-position.
		/// </summary>
		//[Range(-20,20)]
		public float cellContentZPosition = -0.005f;

		/// <summary>
		/// The dragging element prefab.
		/// </summary>
		public GameObject draggingElementPrefab;
		[Range(0,1)]
		/// <summary>
	   /// The dragging element alpha.
	   /// </summary>
		public float draggingElementAlpha = 0.47f;

		/// <summary>
		/// The dragging element z-position.
		/// </summary>
		[Range(-50,50)]
		public float draggingElementZPosition = 0;

		/// <summary>
		/// The dragging element scale factor.
		/// </summary>
		[Range(0,10)]
		public float draggingElementScaleFactor = 1;

		/// <summary>
		/// The grid.
		/// </summary>
		public GameObject grid;

		/// <summary>
		/// The grid contents transform.
		/// </summary>
		public Transform gridContentsTransform;

		/// <summary>
		/// The grid top pivot transform.
		/// </summary>
		public Transform gridTopPivotTransform;
	
		/// <summary>
		/// The grid bottom pivot transform.
		/// </summary>
		public Transform gridBottomPivotTransform;

		/// <summary>
		/// The grid lines transfrom.
		/// </summary>
		public Transform gridLinesTransfrom;
	
		/// <summary>
		/// The level text.
		/// </summary>
		public Text levelText;

		/// <summary>
		/// The mission text.
		/// </summary>
		public Text missionText;

    /// <summary>
    /// The grid cells in the grid.
    /// </summary>
    public static GridCell[] gridCells;

		/// <summary>
		/// The grid lines in the grid.
		/// </summary>
		public static Line[] gridLines;

		/// <summary>
		/// The number of rows of the grid.
		/// </summary>
		public static int numberOfRows;

		/// <summary>
		/// The number of columns of the grid.
		/// </summary>
		public static int numberOfColumns;

		/// <summary>
		/// The water bubble sound effect.
		/// </summary>
		public AudioClip waterBubbleSFX;

		/// <summary>
		/// The level locked sound effect.
		/// </summary>
		public AudioClip levelLockedSFX;

		/// <summary>
		/// The connected sound effect.
		/// </summary>
		public AudioClip connectedSFX;

		/// <summary>
		/// The next button image.
		/// </summary>
		public Image nextButtonImage;

		/// <summary>
		/// The back button image.
		/// </summary>
		public Image backButtonImage;

		/// <summary>
		/// The movements counter.
		/// </summary>
		public static int movements;

	public int Moves;

		/// <summary>
		/// current line in the grid.
		/// </summary>
		private Line currentLine;

		/// <summary>
		/// Temp ray cast hit 2d for ray casting.
		/// </summary>
		private RaycastHit2D tempRayCastHit2D;

		/// <summary>
		/// Temp click position.
		/// </summary>
		private Vector3 tempClickPosition;

		private GridCell beginGridCell;
	public Ingredient beginWireIngredient;
	public int beginWireCount;
	private int AlumEndMoves;
	private int CopperEndMoves;
	private int GoldEndMoves;
	private int SilverEndMoves;

		/// <summary>
		/// The current(selected) grid cell.
		/// </summary>
		private GridCell currentGridCell;

		/// <summary>
		/// The previous grid cell.
		/// </summary>
		private GridCell previousGridCell;
		
		/// <summary>
		/// The current level.
		/// </summary>
		public static Level currentLevel;

    /// <summary>
    /// The size of the grid.
    /// </summary>
    public Vector2 gridSize;

    /// <summary>
    /// Temp point.
    /// </summary>
    public Vector3 tempPoint;

    /// <summary>
    /// Temp scale.
    /// </summary>
    public Vector3 tempScale;

    /// <summary>
    /// Temp color.
    /// </summary>
    public Color tempColor;

	/// <summary>
	/// Temp collider 2d.
	/// </summary>
	public Collider tempCollider3D;

    /// <summary>
    /// Temp collider 2d.
    /// </summary>
    public Collider2D tempCollider2D;

    /// <summary>
    /// Temporary sprite renderer.
    /// </summary>
	public Image tempSpriteRendererd;

    /// <summary>
    /// Whether the dragging element is rendering(dragging)
    /// </summary>
    public bool drawDraggingElement;

    /// <summary>
    ///Whether the current click is moving 
    /// </summary>
    public bool clickMoving;

    /// <summary>
    /// Whether the GameManager is running.
    /// </summary>
    public bool isRunning;

		/// <summary>
		/// The dragging element reference.
		/// </summary>
		public GameObject draggingElement;

		/// <summary>
		/// The dragging element sprite renderer.
		/// </summary>
		public SpriteRenderer draggingElementSpriteRenderer;

		/// <summary>
		/// The timer reference.
		/// </summary>
		public Timer timer;

		/// <summary>
		/// The effects audio source.
		/// </summary>
		[HideInInspector]
		private AudioSource effectsAudioSource;

    private SoundManager cachedSoundManager;
    private SoundSource cachedSoundSource = null;
    private List<GameObject> bullshitInstantiated = new List<GameObject>();

    public SoundManager CachedSoundManager
    {
        get
        {
            if (cachedSoundManager == null) cachedSoundManager = GameManager.Get<SoundManager>();
            if (cachedSoundManager == null) cachedSoundManager = GameObject.FindObjectOfType<SoundManager>();

            return cachedSoundManager;
        }
    }



    void Awake ()
		{
				///Setting up the references
				if (timer == null) {
						//timer = GameObject.Find ("Time").GetComponent<Timer> ();
				}

				if (nextButtonImage == null) {
						//nextButtonImage = GameObject.Find ("NextButton").GetComponent<Image> ();
				}

				if (backButtonImage == null) {
						//backButtonImage = GameObject.Find ("BackButton").GetComponent<Image> ();
				}

				if (effectsAudioSource == null) {
						//effectsAudioSource = GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1];
				}
		}


    public void Close()
    {
        Cleanup(false);
    }


    /// <summary>
    /// When the GameObject becomes visible
    /// </summary>
	public void InitializePuzzle(GameObject puzzle_Canvas)
	{
		if (levelText == null) {
			levelText = GameObject.Find ("GameLevel").GetComponent<Text> ();
		}

		if (missionText == null) {
			missionText = GameObject.Find ("MissionTitle").GetComponent<Text> ();
		}

		if (grid == null) {
			grid = GameObject.Find ("Grid");
		}

		if (gridContentsTransform == null) {
			gridContentsTransform = grid.transform.Find ("Contents").transform;
		}

		if (gridTopPivotTransform == null) {
			gridTopPivotTransform = GameObject.Find ("GridTopPivot").transform;
		}

		if (gridBottomPivotTransform == null) {
			gridBottomPivotTransform = GameObject.Find ("GridBottomPivot").transform;
		}
		if (gridLinesTransfrom == null) {
			gridLinesTransfrom = GameObject.Find ("GridLines").transform;
		}

		try {
			///Setting up Attributes
			numberOfRows = Mission.wantedMission.rowsNumber;
			numberOfColumns = Mission.wantedMission.colsNumber;
			puzzleCanvas = puzzle_Canvas;
			worldLinesTransform = puzzleCanvas.transform.GetChild(1).GetComponent<RectTransform>();
			//levelText.color = Mission.wantedMission.missionColor;
			missionText.text = Mission.wantedMission.missionTitle;
			grid.name = numberOfRows + "x" + numberOfRows + "-Grid";
		} catch (Exception ex) {
			Debug.Log (ex.Message);
		}


		///Determine the size of the Grid
		Vector3 gridTopPivotPostion = gridTopPivotTransform.transform.position;
		Vector3 gridBottomPiviotPosition = gridBottomPivotTransform.transform.position;

		gridSize = gridBottomPiviotPosition - gridTopPivotPostion;
		gridSize = new Vector2 (Mathf.Abs (gridSize.x), Mathf.Abs (gridSize.y));

		///Create New level (the selected level);
		puzzleCanvas.SetActive(true);
        CreateNewLevel();

    }

    /// <summary	>
    /// When the GameObject becomes invisible.
    /// </summary>
    void OnDisable ()
		{
				///stop the timer
				if (timer != null)
						timer.Stop ();
		}


    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
		isRunning = true;
    }
	
		// Update is called once per frame
	void Update (){
		Moves = movements;
		if (!isRunning) {
				return;
		}

//		if (gridLines == null || gridCells == null) {
//				return;
//		}
		if (Input.GetMouseButtonDown (0)) {
				RayCast (Input.mousePosition, ClickType.Began);
            if (cachedSoundSource == null) cachedSoundSource = CachedSoundManager.Play(lineDrawEffect);
        }
        else if (Input.GetMouseButtonUp (0)) {
				Release (currentLine);
            if (cachedSoundSource != null) CachedSoundManager.Stop(cachedSoundSource);
        }

		if (clickMoving) {
				RayCast (Input.mousePosition, ClickType.Moved);
		}

		if (drawDraggingElement) {
				if (!draggingElementSpriteRenderer.enabled) {
						draggingElementSpriteRenderer.enabled = true;
				}

				DrawDraggingElement (Input.mousePosition);
		} else {
				if (draggingElementSpriteRenderer.enabled) {
						draggingElementSpriteRenderer.enabled = false;
				}
		}
	}

		/// <summary>
		/// On Click Release event.
		/// </summary>
		/// <param name="line">The current Line.</param>
		private void Release (Line line)
		{
				clickMoving = false;
				drawDraggingElement = false;
		draggingElement.GetComponentInChildren<ParticleSystem>().enableEmission = false;
				draggingElementSpriteRenderer.enabled = false;

				if (line != null) {
			if (!line.completedLine) {
				line.ClearPath ();
			}
			UIManager.GetMenu<PuzzleMenu> ().SetCurrentWireCounts (beginWireIngredient, beginWireCount);
			movements = 0;
						

				}

				previousGridCell = null;
				currentGridCell = null;
				currentLine = null;
		}

		/// <summary>
		/// Raycast the click (touch) on the screen.
		/// </summary>
		/// <param name="clickPosition">The position of the click (touch).</param>
		/// <param name="clickType">The type of the click(touch).</param>
		private void RayCast (Vector3 clickPosition, ClickType clickType)
		{
				tempClickPosition = mainCamera.ScreenToWorldPoint (clickPosition);
				//tempRayCastHit2D = Physics2D.Raycast (tempClickPosition, Vector2.zero);
				//tempCollider2D = tempRayCastHit2D.collider;
		RaycastHit[] hits;
		Ray playerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(playerRay);

		foreach(RaycastHit hit in hits)
        {
            tempCollider3D = hit.collider;

            if (tempCollider3D != null)
            {
                //Debug.Log(tempCollider3D.tag + " : " + tempCollider3D.gameObject.name);
                ///When a ray hit a grid cell
                if (tempCollider3D.tag == "GridCell")
                {
                    currentGridCell = tempCollider3D.GetComponent<GridCell>();
                    Debug.Log("clicktype ? " + clickType);
                    if (clickType == ClickType.Began)
                    {
                        previousGridCell = currentGridCell;
                        drawDraggingElement = true;
                        draggingElement.GetComponentInChildren<ParticleSystem>().enableEmission = true;
                        GridCellClickBegan();

                        GridCellClickMoved();
                    }
                    else if (clickType == ClickType.Moved)
                    {
                        //Debug.Log(4);
                        drawDraggingElement = true;
                        GridCellClickMoved();
                    }
                }
            }
        }
		}

		/// <summary>
		/// When a click(touch) began on the GridCell.
		/// </summary>
		private void GridCellClickBegan ()
		{
				///If the current grid cell is not currently used and it's empty,then ignore it
				if (currentGridCell.isEmpty && !currentGridCell.currentlyUsed) {
						Debug.Log ("Current grid cell of index " + currentGridCell.index + " is Ignored [Reason : Empty,Not Currently Used]");
						return;
				} 

				///Increase the movements counter
				//IncreaseMovements ();

				///If the grid cell is currently used
				if (currentGridCell.currentlyUsed) {

						if (currentGridCell.gridLineIndex == -1) {
								return;
						}

						///If the grid line of the current grid cell is completed,then reset the grid cells of the line
						if (gridLines [currentGridCell.gridLineIndex].completedLine) {
								Debug.Log ("Reset Grid cells of the Line Path of the index " + currentGridCell.gridLineIndex);
								gridLines [currentGridCell.gridLineIndex].completedLine = false;
				//TODO - Bug on asana, UI count does not reset after this event happens
								Release (gridLines [currentGridCell.gridLineIndex]);
								return;
						} 

				} else if (!currentGridCell.isEmpty) {//If the grid is not currently used and it's not empty
						///Setting up dragging element color
						tempColor = currentGridCell.topBackgroundColor;
						tempColor.a = gridCellTopBackgroundAlpha;
						//draggingElementSpriteRenderer = draggingElement.GetComponent<Image> ();
						draggingElementSpriteRenderer.color = tempColor;

						///Setting up the attributes for the current grid cell
						clickMoving = true;
				beginGridCell = currentGridCell;
						currentGridCell.currentlyUsed = true;
						if (currentLine == null) {
								currentLine = gridLines [currentGridCell.gridLineIndex];
						}

						Debug.Log ("New GridCell of Index " + currentGridCell.index + " added to the Line Path of index " + currentLine.index);

						///Add the current grid cell index to the current traced grid cells list
						currentLine.path.Add (currentGridCell.index);

						///Determine the New Line Point
						tempPoint = currentGridCell.transform.position;
						//tempPoint.z = gridLineZPosition;

						///Add the position of the New Line Point to the current line
						gridLines [currentGridCell.gridLineIndex].AddPoint (tempPoint);
				}
		}

		/// <summary>
		/// When a click(touch) moved over the GridCell.
		/// </summary>
		private void GridCellClickMoved ()
		{
        Debug.Log("grid cell click moved!!");
		if (EnoughWiresOfType())
        {
            if (currentLine == null)
            {
                Debug.Log("Current Line is undefined");
                return;
            }
            if (currentGridCell == null)
            {
                Debug.Log("Current GridCell is undefined");
                return;
            }
            if (previousGridCell == null)
            {
                Debug.Log("Previous GridCell is undefined");
                return;
            }
            if (currentGridCell.index == previousGridCell.index)
            {
                return;
            }
            ///If the current grid cell is not adjacent of the previous grid cell,then ignore it
            if (!previousGridCell.OneOfAdjacents(currentGridCell.index))
            {
                Debug.Log("Current grid cell of index " + currentGridCell.index + " is Ignored [Reason : Not Adjacent Of Previous GridCell " + previousGridCell.index);
                return;
            }

            ///If the current grid cell is currently used
            if (currentGridCell.currentlyUsed)
            {
                Debug.Log("current grid cell used! index : "+ currentGridCell.gridLineIndex);
                if (currentGridCell.gridLineIndex == -1)
                {
                    return;
                }

                if (currentGridCell.gridLineIndex == previousGridCell.gridLineIndex)
                {
                        gridLines[currentGridCell.gridLineIndex].RemoveElements(currentGridCell.index);
                        previousGridCell = currentGridCell;
                        Debug.Log("Remove some Elements from the Line Path of index " + currentGridCell.gridLineIndex);
                        DecreaseMovements();
                        return;//skip next
                }
                else {
                    Debug.Log("Clear the Line Path of index " + currentGridCell.gridLineIndex);
					Line pathLine = gridLines [currentGridCell.gridLineIndex];
					pathLine.ClearPath();
					Ingredient gridIng = pathLine.gridCell.gridIngredient;
					int count = UIManager.GetMenu<Inventory> ().GetIngredientAmount (gridIng);
					UIManager.GetMenu<PuzzleMenu> ().SetCurrentWireCounts (gridIng, count);
                }
            }

            ///If the current grid cell is not empty or it's not a partner of the previous grid cell
            if (!currentGridCell.isEmpty && currentGridCell.index != previousGridCell.tragetIndex)
            {
                Debug.Log("Current grid cell of index " + currentGridCell.index + " is Ignored [Reason : Not the wanted Traget]");
                return;//skip next
            }

            ///Increase the movements counter
            IncreaseMovements();
            Debug.Log("increasing movement counter");
            ///Setting up the attributes for the current grid cell
            currentGridCell.currentlyUsed = true;
            currentGridCell.gridLineIndex = previousGridCell.gridLineIndex;
            if (currentGridCell.gridLineIndex == -1)
            {
                return;
            }
            if (currentGridCell.isEmpty)
                currentGridCell.tragetIndex = previousGridCell.tragetIndex;

            ///Link the color of top background of the current grid cell with the top background color of the previous grid cell
            currentGridCell.topBackgroundColor = previousGridCell.topBackgroundColor;

            ///Add the current grid cell index to the current traced grid cells list
            currentLine.path.Add(currentGridCell.index);

            ///Determine the New Line Point
            tempPoint = currentGridCell.transform.position;
            //tempPoint.z = gridLineZPosition;

            ///Add the position of the New Line Point to the current line
            gridLines[currentGridCell.gridLineIndex].AddPoint(tempPoint);

            bool playBubble = true;
            if (!currentGridCell.isEmpty)
            {
                //Two pairs connected
                if (previousGridCell.tragetIndex == currentGridCell.index)
                {
                    Debug.Log("Two GridCells connected [GridCell " + (gridLines[currentGridCell.gridLineIndex].GetFirstPathElement()) + " with GridCell " + (gridLines[currentGridCell.gridLineIndex].GetLastPathElement()) + "]");
                    currentLine.completedLine = true;
                    GridCell gridCell = null;
                    for (int i = 0; i < currentLine.path.Count; i++)
                    {
                        gridCell = gridCells[currentLine.path[i]];

                        if (i == 0 || i == currentLine.path.Count - 1)
                        {
                            //Setting up the connect pairs
                            if (gridCell == null) Debug.LogError("gridcell is null!");
                            Transform transform = GameObjectUtil.FindChildByTag(gridCell.transform, "GridCellContent");
							if (transform == null) transform = gridCell.GetComponentInChildren<Image>().transform;

                            Debug.Log("pair index : " + gridCell.elementPairIndex + " | pair count : " + currentLevel.dotsPairs.Count);
							Image bgImage = transform.transform.Find("background").GetComponent<Image>();
							bgImage.sprite = currentLevel.dotsPairs[gridCell.elementPairIndex].connectSprite;
							bgImage.transform.localPosition = new Vector3 (0.0f, 0.0f, cellContentZPosition / 2);
							bgImage.enabled = true;
                        }
                        ///Setting up the color of the top background of the grid cell
                        tempColor = previousGridCell.topBackgroundColor;
                        tempColor.a = gridCellTopBackgroundAlpha;
						tempSpriteRendererd = gridCell.transform.Find("background").GetComponent<Image>();
						print ("1 " + gridLines [currentGridCell.gridLineIndex]);

                        //tempSpriteRendererd.color = tempColor;
                        ///Enable the top backgroud of the grid cell
                        //tempSpriteRendererd.enabled = true;
                    }

                    ///Play the connected sound effect at the center of the unity world
                    if (connectedSFX != null && effectsAudioSource != null)
                    {
                        AudioSource.PlayClipAtPoint(connectedSFX, Vector3.zero, effectsAudioSource.volume);
                    }
                    playBubble = false;
                    Release(null);
                    CheckLevelComplete();
					//print ("2 " + gridLines [currentGridCell.gridLineIndex]);
                    return;
                }
            }
            if (playBubble)
            {
                ///Play the water buttle sound effect at the center of the unity world
                if (waterBubbleSFX != null && effectsAudioSource != null)
                {
                    AudioSource.PlayClipAtPoint(waterBubbleSFX, Vector3.zero, effectsAudioSource.volume);
                }
            }
            previousGridCell = currentGridCell;
        }
        else
        {
            Debug.Log("not enough wires!");
        }
		}

	public bool EnoughWiresOfType(){
		beginWireIngredient = beginGridCell.gridIngredient;
		beginWireCount = UIManager.GetMenu<Inventory> ().GetIngredientAmount (beginWireIngredient);
		if (beginWireCount > movements) {
			return true;
		} else {
			return false;
		}
	}
		/// <summary>
		/// Refreshs(Reset) the grid.
		/// </summary>
		public void RefreshGrid ()
		{
				movements = 0;
       // UIManager.GetMenu<PuzzleMenu>().SetWiresUsed(movements);
				timer.Stop ();

				if (gridLines != null) {
						for (int i = 0; i < gridLines.Length; i++) {
								if (gridLines [i].completedLine)
										gridLines [i].ClearPath ();
						}
				}
				currentLine = null;
				currentGridCell = previousGridCell = null;
				timer.Start ();
		}

		/// <summary>
		/// Draw(Drag) the dragging element.
		/// </summary>
		/// <param name="clickPosition">Click position.</param>
		private void DrawDraggingElement (Vector3 clickPosition)
		{
				if (draggingElement == null) {
						return;
				}
				tempClickPosition = Camera.main.ScreenToWorldPoint (clickPosition);
				tempClickPosition.z = draggingElementZPosition;
				//draggingElement.transform.position = tempClickPosition;

		RaycastHit hit;
		Ray dragRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay (dragRay.origin, dragRay.direction * 50.0f, Color.green, 1.0f, false);

		if (Physics.Raycast (dragRay, out hit)) {
			float distRatio = 0.9f;
			Vector3 dragPosition = Vector3.Lerp (dragRay.origin, hit.point, distRatio);
			draggingElement.transform.position = dragPosition;
		}
		}

		/// <summary>
		/// Create a new level.
		/// </summary>
		private void CreateNewLevel ()
		{
				try {
						movements = 0;
			UIManager.GetMenu<PuzzleMenu>().SetInitialWireCounts();

            levelText.text = "Level " + TableLevel.wantedLevel.ID;
						//ResetGameContents ();
						BuildTheGrid ();
						SettingUpPairs ();
                        SettingUpObstacles();
						SettingUpNextBackAlpha ();
						timer.Stop ();
						timer.Start ();
			StartCoroutine(Delay());
				} catch (Exception ex) {
						Debug.Log ("Make sure you have selected a level, and there are no empty references in GameManager component");
				}
		}

		/// <summary>
		/// Build the grid.
		/// </summary>
		private void BuildTheGrid ()
		{
				Debug.Log ("Building the Grid " + numberOfRows + "x" + numberOfColumns);
		if (WorldSpacePuzzle) {
			
			gridCells = puzzleCanvas.GetComponentsInChildren<GridCell>();
			GridCell worldCellComponent;
			GameObject worldCell = null;
			int worldCellindex;


			for (int i = 0; i < numberOfRows; i++) {
				for (int j = 0; j < numberOfColumns; j++) {
					worldCellindex = i * numberOfColumns + j;
					//worldCell = Instantiate (worldCellPrefab, Vector3.zero, world.transform.rotation) as GameObject;
					worldCell = gridCells[worldCellindex].gameObject;
					worldCellComponent = worldCell.GetComponent<GridCell> ();
					worldCellComponent.index = worldCellindex;
					worldCellComponent.DefineAdjacents (i, j);
					worldCell.name = "GridCell-" + (worldCellindex + 1);
				}
			}

		} else{
			Vector3 gridCellScale = Vector3.one;
			Vector3 gridCellPosition = Vector2.zero;
			Vector3 gridPosition = gridTopPivotTransform.position;
			gridCells = new GridCell[numberOfRows * numberOfColumns];
			GridCell gridCellComponent;
			GameObject gridCell = null;
			int gridCellIndex;

			for (int i = 0; i < numberOfRows; i++) {
				for (int j = 0; j < numberOfColumns; j++) {
					gridCellIndex = i * numberOfColumns + j;
					gridCellPosition.x = gridPosition.x + gridCellScale.x * j + gridCellScale.x / 2;
					gridCellPosition.y = gridPosition.y - gridCellScale.y * i - gridCellScale.x / 2;
					gridCellPosition.z = gridCellZPosition;
					gridCell = Instantiate (gridCellPrefab, gridCellPosition, Quaternion.identity) as GameObject;
					//Set the color for each grid cell
					//gridCell.GetComponent<SpriteRenderer> ().color = Mission.wantedMission.missionColor;
					gridCellComponent = gridCell.GetComponent<GridCell> ();
					gridCellComponent.index = gridCellIndex;
					///Define the adjacents of the grid cell
					gridCellComponent.DefineAdjacents (i, j);
					//gridCell.transform.localScale = gridCellScale;
					gridCell.transform.parent = gridContentsTransform;
					gridCell.name = "GridCell-" + (gridCellIndex + 1);
					gridCells [gridCellIndex] = gridCell.GetComponent<GridCell> ();
				}
			}
		}
	}

	private void SetGridPairIngredient(GridCell cell, Level.WireTypes wireType){
		if (wireType == Level.WireTypes.Aluminum) {
			cell.gridIngredient = UIManager.GetMenu<PuzzleMenu> ().AluminumWire;
		} else if (wireType == Level.WireTypes.Copper) {
			cell.gridIngredient = UIManager.GetMenu<PuzzleMenu> ().CopperWire;
		} else if (wireType == Level.WireTypes.Gold) {
			cell.gridIngredient = UIManager.GetMenu<PuzzleMenu> ().GoldWire;
		} else if (wireType == Level.WireTypes.Silver) {
			cell.gridIngredient = UIManager.GetMenu<PuzzleMenu> ().SilverWire;
		}
	}

		/// <summary>
		/// Setting up the cells pairs.
		/// </summary>
		private void SettingUpPairs ()
		{
				Debug.Log ("Setting up the Cells Pairs");

		if (WorldSpacePuzzle) {
			currentLevel = Mission.wantedMission.levelsManagerComponent.levels [TableLevel.wantedLevel.ID - 1];

			if (currentLevel == null) {
				Debug.Log ("level is undefined");
				return;
			}

			TextMesh numberTextmesh;
			Color numberColor = Color.white;
			Level.DotsPair elementsPair = null;
			Transform worldCellTransform;
			GridCell gridCell;
			Vector3 cellContentPosition = new Vector3 (0, 0, cellContentZPosition);
			Vector3 gridCellScale = Vector3.zero;
			GameObject firstElement = null;
			GameObject secondElement = null;
			Level.WireTypes gridType;
			float cellContentScale = 0;

			gridLines = new Line[currentLevel.dotsPairs.Count];

			for (int i = 0; i < currentLevel.dotsPairs.Count; i++) {
		
				elementsPair = currentLevel.dotsPairs [i];
				numberColor = new Color (1 - elementsPair.color.r, 1 - elementsPair.color.g, 1 - elementsPair.color.b, 1);//opposite color

				//Setting up the First Dot(Element)
				gridCell = gridCells [elementsPair.firstDot.index];
			
				gridCell.gridLineIndex = i;
				gridCell.elementPairIndex = i;
				gridCell.topBackgroundColor = elementsPair.lineColor;
				gridCell.isEmpty = false;
				gridCell.tragetIndex = elementsPair.secondDot.index;

				SetGridPairIngredient (gridCell, elementsPair.wireType);
			

				worldCellTransform = gridCell.gameObject.transform;
				//gridCellScale = gridCellTransform.localScale;
				//cellContentScale = (Mathf.Max (gridCellScale.x, gridCellScale.y) / Mathf.Min (gridCellScale.x, gridCellScale.y)) * cellContentScaleFactor;

				firstElement = Instantiate (contentCellPrefab) as GameObject;
                bullshitInstantiated.Add(firstElement);
				firstElement.transform.SetParent (worldCellTransform, true);
				firstElement.transform.localPosition = cellContentPosition;
				firstElement.transform.rotation = worldCellTransform.rotation;

				//firstElement.transform.localScale = new Vector3 (cellContentScale, cellContentScale, cellContentScale);
				firstElement.name = "Pair" + (i + 1) + "-FirstElement";
				firstElement.GetComponent<Image> ().sprite = elementsPair.sprite;

			
				if (elementsPair.applyColorOnSprite) {
					firstElement.GetComponent<Image> ().color = elementsPair.color;//apply the sprite color
				} else {
					firstElement.GetComponent<Image> ().color = Color.white;//apply the white color
				}

//				numberTextmesh = firstElement.transform.Find ("Number").GetComponent<TextMesh> ();
//				if (currentLevel.showPairsNumber) {
//					numberTextmesh.text = (i + 1).ToString ();
//					numberTextmesh.color = Color.white;//numberColor; 
//				} else {
//					numberTextmesh.text = "";//empty value
//				}

				//Setting up the Second Dot(Element)
				gridCell = gridCells [elementsPair.secondDot.index];
				gridCell.gridLineIndex = i;
				gridCell.elementPairIndex = i;
				gridCell.topBackgroundColor = elementsPair.lineColor;
				gridCell.isEmpty = false;
				gridCell.tragetIndex = elementsPair.firstDot.index;

				SetGridPairIngredient (gridCell, elementsPair.wireType);


				worldCellTransform = gridCell.gameObject.transform;
				//gridCellScale = gridCellTransform.localScale;
				//cellContentScale = (Mathf.Max (gridCellScale.x, gridCellScale.y) / Mathf.Min (gridCellScale.x, gridCellScale.y)) * cellContentScaleFactor;

				secondElement = Instantiate (contentCellPrefab) as GameObject;
                bullshitInstantiated.Add(secondElement);

				secondElement.transform.SetParent (worldCellTransform, true);
				secondElement.transform.localPosition = cellContentPosition;
				secondElement.transform.rotation = worldCellTransform.rotation;
				//secondElement.transform.localScale = new Vector3 (cellContentScale, cellContentScale, cellContentScale);
				secondElement.name = "Pair" + (i + 1) + "-SecondElement";
				secondElement.GetComponent<Image> ().sprite = elementsPair.sprite;
		

				if (elementsPair.applyColorOnSprite) {
					secondElement.GetComponent<Image> ().color = elementsPair.color;//apply the sprite color
				} else {
					secondElement.GetComponent<Image> ().color = Color.white;//apply the white color
				}
//				numberTextmesh = secondElement.transform.Find ("Number").GetComponent<TextMesh> ();
//				if (currentLevel.showPairsNumber) {
//					numberTextmesh.text = (i + 1).ToString ();
//					numberTextmesh.color = Color.white;//numberColor; 
//				} else {
//					numberTextmesh.text = "";//empty value
//				}

				///Create Grid Line
				CreateGridLine (0.01f, elementsPair.lineColor, "Line " + elementsPair.firstDot.index + "-" + elementsPair.secondDot.index, i);
			}
			Color tempColor = Mission.wantedMission.missionColor;
			tempColor.a = draggingElementAlpha;
			CreateDraggingElement (tempColor, new Vector3 (cellContentScale * draggingElementScaleFactor, cellContentScale * draggingElementScaleFactor, cellContentScale));
		} else {
		/*
			currentLevel = Mission.wantedMission.levelsManagerComponent.levels [TableLevel.wantedLevel.ID - 1];

			if (currentLevel == null) {
				Debug.Log ("level is undefined");
				return;
			}

			TextMesh numberTextmesh;
			Color numberColor = Color.white;
			Level.DotsPair elementsPair = null;
			Transform gridCellTransform;
			GridCell gridCell;
			Vector3 cellContentPosition = new Vector3 (0, 0, cellContentZPosition);
			Vector3 gridCellScale = Vector3.zero;
			GameObject firstElement = null;
			GameObject secondElement = null;
			Level.WireTypes gridType;
			float cellContentScale = 0;
			gridLines = new Line[currentLevel.dotsPairs.Count];
		
			for (int i = 0; i < currentLevel.dotsPairs.Count; i++) {
				elementsPair = currentLevel.dotsPairs [i];
				numberColor = new Color (1 - elementsPair.color.r, 1 - elementsPair.color.g, 1 - elementsPair.color.b, 1);//opposite color
			
				//Setting up the First Dot(Element)
				gridCell = gridCells [elementsPair.firstDot.index];
                        
				gridCell.gridLineIndex = i;
				gridCell.elementPairIndex = i;
				gridCell.topBackgroundColor = elementsPair.lineColor;
				gridCell.isEmpty = false;
				gridCell.tragetIndex = elementsPair.secondDot.index;

				SetGridPairIngredient (gridCell, elementsPair.wireType);
			
				gridCellTransform = gridCell.gameObject.transform;
				gridCellScale = gridCellTransform.localScale;
				cellContentScale = (Mathf.Max (gridCellScale.x, gridCellScale.y) / Mathf.Min (gridCellScale.x, gridCellScale.y)) * cellContentScaleFactor;
			
				firstElement = Instantiate (cellContentPrefab) as GameObject;
				firstElement.transform.SetParent (gridCellTransform);
				firstElement.transform.localPosition = cellContentPosition;
				firstElement.transform.localScale = new Vector3 (cellContentScale, cellContentScale, cellContentScale);
				firstElement.name = "Pair" + (i + 1) + "-FirstElement";
				firstElement.GetComponent<SpriteRenderer> ().sprite = elementsPair.sprite;
			
				if (elementsPair.applyColorOnSprite) {
					firstElement.GetComponent<SpriteRenderer> ().color = elementsPair.color;//apply the sprite color
				} else {
					firstElement.GetComponent<SpriteRenderer> ().color = Color.white;//apply the white color
				}

				numberTextmesh = firstElement.transform.Find ("Number").GetComponent<TextMesh> ();
				if (currentLevel.showPairsNumber) {
					numberTextmesh.text = (i + 1).ToString ();
					numberTextmesh.color = Color.white;//numberColor; 
				} else {
					numberTextmesh.text = "";//empty value
				}

				//Setting up the Second Dot(Element)
				gridCell = gridCells [elementsPair.secondDot.index];
				gridCell.gridLineIndex = i;
				gridCell.elementPairIndex = i;
				gridCell.topBackgroundColor = elementsPair.lineColor;
				gridCell.isEmpty = false;
				gridCell.tragetIndex = elementsPair.firstDot.index;

				SetGridPairIngredient (gridCell, elementsPair.wireType);
			
				gridCellTransform = gridCell.gameObject.transform;
				gridCellScale = gridCellTransform.localScale;
				cellContentScale = (Mathf.Max (gridCellScale.x, gridCellScale.y) / Mathf.Min (gridCellScale.x, gridCellScale.y)) * cellContentScaleFactor;
			
				secondElement = Instantiate (cellContentPrefab) as GameObject;
				secondElement.transform.parent = gridCellTransform;
				secondElement.transform.localPosition = cellContentPosition;
				secondElement.transform.localScale = new Vector3 (cellContentScale, cellContentScale, cellContentScale);
				secondElement.name = "Pair" + (i + 1) + "-SecondElement";
				secondElement.GetComponent<SpriteRenderer> ().sprite = elementsPair.sprite;

				if (elementsPair.applyColorOnSprite) {
					secondElement.GetComponent<SpriteRenderer> ().color = elementsPair.color;//apply the sprite color
				} else {
					secondElement.GetComponent<SpriteRenderer> ().color = Color.white;//apply the white color
				}

				numberTextmesh = secondElement.transform.Find ("Number").GetComponent<TextMesh> ();
				if (currentLevel.showPairsNumber) {
					numberTextmesh.text = (i + 1).ToString ();
					numberTextmesh.color = Color.white;//numberColor; 
				} else {
					numberTextmesh.text = "";//empty value
				}

				///Create Grid Line
				CreateGridLine (0.1f * gridLineWidthFactor, elementsPair.lineColor, "Line " + elementsPair.firstDot.index + "-" + elementsPair.secondDot.index, i);
			}
		
			Color tempColor = Mission.wantedMission.missionColor;
			tempColor.a = draggingElementAlpha;
		
			CreateDraggingElement (tempColor, new Vector3 (cellContentScale * draggingElementScaleFactor, cellContentScale * draggingElementScaleFactor, cellContentScale));
			*/
		}
		}

    private void SettingUpObstacles()
    {
		if (WorldSpacePuzzle) {
			
			Level.Barrier barrier = null;
			Transform worldCellTransform;
			GridCell worldCell;
			Vector3 cellContentPosition = new Vector3 (0, 0, cellContentZPosition);
			//Vector3 gridCellScale = Vector3.zero;
			GameObject firstElement = null;
			TextMesh numberTextmesh;
			float cellContentScale = 0;

			for (int i = 0; i < currentLevel.barriers.Count; i++) {
				barrier = currentLevel.barriers [i];

				//Setting up the First Dot(Element)
				worldCell = gridCells [barrier.index];

				worldCell.gridLineIndex = -1;
				worldCell.elementPairIndex = i;
				worldCell.currentlyUsed = true;
				worldCell.isEmpty = false;
				worldCell.tragetIndex = barrier.index;

				worldCellTransform = worldCell.gameObject.transform;
				//gridCellScale = gridCellTransform.localScale;
				//cellContentScale = (Mathf.Max (gridCellScale.x, gridCellScale.y) / Mathf.Min (gridCellScale.x, gridCellScale.y)) * cellContentScaleFactor;

				firstElement = Instantiate (contentCellPrefab) as GameObject;
                bullshitInstantiated.Add(firstElement);

				firstElement.transform.SetParent (worldCellTransform);
				firstElement.transform.localPosition = cellContentPosition;
				firstElement.transform.rotation = worldCellTransform.rotation;
				//firstElement.transform.localScale = new Vector3 (cellContentScale, cellContentScale, cellContentScale);
				firstElement.name = "barrier" + (i + 1) + "-FirstElement";
				firstElement.GetComponent<Image> ().sprite = barrier.sprite;

				//numberTextmesh = firstElement.transform.Find ("Number").GetComponent<TextMesh> ();
				//numberTextmesh.text = "";//empty value

				firstElement.GetComponent<Image> ().color = barrier.color;
			}
		}else {
			/*
			Level.Barrier barrier = null;
			Transform gridCellTransform;
			GridCell gridCell;
			Vector3 cellContentPosition = new Vector3 (0, 0, cellContentZPosition);
			Vector3 gridCellScale = Vector3.zero;
			GameObject firstElement = null;
			TextMesh numberTextmesh;
			float cellContentScale = 0;

			for (int i = 0; i < currentLevel.barriers.Count; i++) {
				barrier = currentLevel.barriers [i];

				//Setting up the First Dot(Element)
				gridCell = gridCells [barrier.index];

				gridCell.gridLineIndex = -1;
				gridCell.elementPairIndex = i;
				gridCell.currentlyUsed = true;
				gridCell.isEmpty = false;
				gridCell.tragetIndex = barrier.index;

				gridCellTransform = gridCell.gameObject.transform;
				gridCellScale = gridCellTransform.localScale;
				cellContentScale = (Mathf.Max (gridCellScale.x, gridCellScale.y) / Mathf.Min (gridCellScale.x, gridCellScale.y)) * cellContentScaleFactor;

				firstElement = Instantiate (cellContentPrefab) as GameObject;
				firstElement.transform.SetParent (gridCellTransform);
				firstElement.transform.localPosition = cellContentPosition;
				firstElement.transform.localScale = new Vector3 (cellContentScale, cellContentScale, cellContentScale);
				firstElement.name = "barrier" + (i + 1) + "-FirstElement";
				firstElement.GetComponent<SpriteRenderer> ().sprite = barrier.sprite;

				numberTextmesh = firstElement.transform.Find ("Number").GetComponent<TextMesh> ();
				numberTextmesh.text = "";//empty value

				firstElement.GetComponent<SpriteRenderer> ().color = barrier.color;
				*/
			}
		}

    /// <summary>
    /// Setting up Grid Lines
    /// </summary>
    /// <param name="lineWidth">Line width.</param>
    /// <param name="lineColor">Line color.</param>
    /// <param name="name">Name of grid line.</param>
    /// <param name="index">Index.</param>
    private void CreateGridLine (float lineWidth, Color lineColor, string name, int index)
		{
		GameObject gridLine = Instantiate (worldLinePrefab, puzzleCanvas.transform.position, puzzleCanvas.transform.rotation) as GameObject;
		gridLine.transform.parent = worldLinesTransform;
				gridLine.name = name;
				Line line = gridLine.GetComponent<Line> ();
				line.SetWidth (0.08f);
				line.SetColor (lineColor);
				if (gridLines != null) {
						gridLines [index] = line;
						gridLines [index].index = index;
				}
		}

		/// <summary>
		/// Creates the dragging element.
		/// </summary>
		/// <param name="color">Color of the dragging element.</param>
		/// <param name="scale">Scale of the dragging element.</param>
		private void CreateDraggingElement (Color color, Vector3 scale)
		{
				GameObject currentDraggingElement = GameObject.Find ("DraggingElement");
				if (draggingElement == null) {
			

						draggingElement = Instantiate (draggingElementPrefab) as GameObject;
						draggingElement.transform.parent = GameObject.Find ("GameScene").transform;

						draggingElement.name = "DraggingElement";
						draggingElement.transform.Find ("ColorsEffect").GetComponent<ParticleEmitter> ().emit = false;
				} else {
						draggingElement = currentDraggingElement;
			draggingElement.transform.GetComponentInChildren<ParticleSystem> ().enableEmission = false;
						//draggingElement.transform.Find ("ColorsEffect").GetComponent<ParticleEmitter> ().emit = false;
				}
	

				draggingElement.transform.localScale = scale;
		draggingElementSpriteRenderer = draggingElement.GetComponent<SpriteRenderer> ();
				draggingElementSpriteRenderer.color = color;
				draggingElementSpriteRenderer.enabled = false;
		}

		/// <summary>
		/// Go to the next level.
		/// </summary>
		public void NextLevel ()
		{
				if (LevelsTable.currentLevelID >= 1 && LevelsTable.currentLevelID < LevelsTable.tableLevels.Count) {
						///Get the next level and check if it's locked , then do not load the next level
						DataManager.MissionData currentMissionData = DataManager.FindMissionDataById (Mission.wantedMission.ID, DataManager.instance.filterdMissionsData);//Get the current mission
						if (LevelsTable.currentLevelID + 1 <= currentMissionData.levelsData.Count) {
								DataManager.LevelData nextLevelData = currentMissionData.FindLevelDataById (LevelsTable.currentLevelID + 1);///Get the next level
								if (nextLevelData.isLocked) {
										///Play lock sound effectd
										if (levelLockedSFX != null && effectsAudioSource != null) {
												AudioSource.PlayClipAtPoint (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
										}
										///Skip the next
										return;
								}
						}
						TableLevel.wantedLevel = LevelsTable.tableLevels [LevelsTable.currentLevelID];///Set the wanted level
						CreateNewLevel ();///Create new level
						LevelsTable.currentLevelID++;///Increase level ID
			
				} else {
						///Play lock sound effectd
						if (levelLockedSFX != null && effectsAudioSource!=null) {
							AudioSource.PlayClipAtPoint (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
						}
				}
		}

		//// <summary>
		/// Back to the previous level.
		/// </summary>
		public void PreviousLevel ()
		{
				if (LevelsTable.currentLevelID > 1 && LevelsTable.currentLevelID <= LevelsTable.tableLevels.Count) {
						LevelsTable.currentLevelID--;
						TableLevel.wantedLevel = LevelsTable.tableLevels [LevelsTable.currentLevelID - 1];
						CreateNewLevel ();
				} else {
						///Play lock sound effectd
						if (levelLockedSFX != null && effectsAudioSource!=null) {
								AudioSource.PlayClipAtPoint (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
						}
				}
		}

		/// <summary>
		/// Setting up alpha value for the next and back buttons.
		/// </summary>
		private void SettingUpNextBackAlpha ()
		{
				if (TableLevel.wantedLevel.ID == 1) {
						tempColor = backButtonImage.color;
						tempColor.a = 0.5f;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = false;

						tempColor = nextButtonImage.color;
						tempColor.a = 1;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = true;
				} else if (TableLevel.wantedLevel.ID == LevelsTable.tableLevels.Count) {
						tempColor = backButtonImage.color;
						tempColor.a = 1;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = true;

						tempColor = nextButtonImage.color;
						tempColor.a = 0.5f;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = false;
				} else {
						tempColor = backButtonImage.color;
						tempColor.a = 1;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = true;

						tempColor = nextButtonImage.color;
						tempColor.a = 1;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = true;
				}
		}

		/// <summary>
		/// Resets the game contents.
		/// </summary>
		private void ResetGameContents ()
		{
				GameObject [] gridCells = GameObject.FindGameObjectsWithTag ("GridCell");
				foreach (GameObject gridCell in gridCells) {
						Destroy (gridCell);
				}

				GameObject [] gridLines = GameObject.FindGameObjectsWithTag ("GridLine");
				foreach (GameObject gridLine in gridLines) {
						Destroy (gridLine);
				}
		}

		/// <summary>
		/// Checks Wheter the level is completed.
		/// </summary>
		private void CheckLevelComplete ()
		{
		SetEndMoves ();
		movements = 0;
				if (gridLines == null) {
						return;
				}

				bool isLevelComplete = true;

				for (int i = 0; i < gridLines.Length; i++) {
						if (!gridLines [i].completedLine) {
								isLevelComplete = false;
								break;
						}
				}

			if (isLevelComplete) {
            if (cachedSoundSource != null) CachedSoundManager.Stop(cachedSoundSource);
            CachedSoundManager.Play(puzzleCompleteEffect);
				timer.Stop ();
				isRunning = false;
			RemoveInventoryWires ();
            Cleanup(true);
        }
            else
        {
            if (cachedSoundSource != null) CachedSoundManager.Stop(cachedSoundSource);
            CachedSoundManager.Play(puzzleCompleteEffect);
            timer.Stop();
            isRunning = false;
            Cleanup(false);
        }
		}

	private void SetEndMoves(){
		if (beginGridCell.gridIngredient == UIManager.GetMenu<PuzzleMenu> ().AluminumWire) {
			AlumEndMoves = movements;
		} else if (beginGridCell.gridIngredient == UIManager.GetMenu<PuzzleMenu> ().CopperWire) {
			CopperEndMoves = movements;
		} else if (beginGridCell.gridIngredient == UIManager.GetMenu<PuzzleMenu> ().GoldWire) {
			GoldEndMoves = movements;
		} else if (beginGridCell.gridIngredient == UIManager.GetMenu<PuzzleMenu> ().SilverWire) {
			SilverEndMoves = movements;
		}
	}

	private void RemoveInventoryWires(){
		UIManager.GetMenu<Inventory>().RemoveInventoryItem(UIManager.GetMenu<PuzzleMenu>().AluminumWire, AlumEndMoves);
		UIManager.GetMenu<Inventory>().RemoveInventoryItem(UIManager.GetMenu<PuzzleMenu>().CopperWire, CopperEndMoves);
		UIManager.GetMenu<Inventory>().RemoveInventoryItem(UIManager.GetMenu<PuzzleMenu>().GoldWire, GoldEndMoves);
		UIManager.GetMenu<Inventory>().RemoveInventoryItem(UIManager.GetMenu<PuzzleMenu>().SilverWire, SilverEndMoves);
	}


    private void Cleanup(bool completed)
    {
        PuzzleMenu pm = UIManager.GetMenu<PuzzleMenu>();
        pm.Close(completed);

		if (completed) {
			puzzleCanvas.GetComponent<Animator> ().enabled = false;
		}

    }

		/// <summary>
		/// Increase the movements counter.
		/// </summary>
	private void IncreaseMovements (){
		movements++;
		UIManager.GetMenu<PuzzleMenu> ().SetCurrentWireCounts (beginWireIngredient, beginWireCount - movements);
	}

	/// <summary>
	/// Decrease the movements counter.
	/// </summary>
	private void DecreaseMovements ()
	{
		movements--;
		UIManager.GetMenu<PuzzleMenu> ().SetCurrentWireCounts (beginWireIngredient, beginWireCount - movements);
	}

		public enum ClickType
		{
				Began,
				Moved,
				Ended
		}
}