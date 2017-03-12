using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	[SerializeField] private int gridSizeX = 10;
	[SerializeField] private int gridSizeY = 10;//How many symbols will be drawn per line of poem
	[SerializeField] private int gridSpace = 1;//distance between symbols
	//[SerializeField] private string gizmoName = "point.png";//for debugging 
	[SerializeField] private Sprite[] sprites;//The sprites that will change in the prefab
	[SerializeField] private GameObject cellPrefab;//prefab where the sprites will be stored/changed
	private GameObject[,] objectArray;
	private SpriteRenderer[,] rendererArray;

	// Use this for initialization
	void Start () {
		InitializeGrid(gridSizeX, gridSizeY, gridSpace);
		Debug.Log(Application.persistentDataPath);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.Log("changing sprite");
			for (int i = 0; i < gridSizeX; i++)
			{
				for (int j = 0; j < gridSizeY; j++)
				{
					if(rendererArray[i, j] != null){
						int p = Random.Range(0, sprites.Length);
						ChangeSprite(i, j, sprites[p]);
					}
				}
			}
		}
	}

	//debugging using Gizmos
	//shows empty spaces in the grid
	void OnDrawGizmos () {
		//InitializeGrid(gridSizeX, gridSizeY, gridSpace);
	}

	//draws the grid of GameObjects
	void InitializeGrid (int _sizeX, int _sizeY, int _space) {
		objectArray = new GameObject[gridSizeX,gridSizeY];
		rendererArray = new SpriteRenderer[gridSizeX,gridSizeY];
		for(int i = 0; i < _sizeX; i+=2) {
			for(int j = 0; j < _sizeY; j+=2){
					//Gizmos.DrawIcon(new Vector3(i * _space, j * _space), gizmoName, true);
					objectArray[i,j] = (GameObject) Instantiate(cellPrefab, 
					new Vector3(i * _space, j * _space, 0), Quaternion.Euler(0,0,45));// as GameObject;
			}
		}
		for(int i = 1; i < _sizeX; i+=2) {
			for(int j = 1; j < _sizeY; j+=2){
					//Gizmos.DrawIcon(new Vector3(i * _space, j * _space), gizmoName, true);
					objectArray[i,j] = (GameObject) Instantiate(cellPrefab, 
					new Vector3(i * _space, j * _space, 0), Quaternion.Euler(0,0,45));// as GameObject;
			}
		}

		for (int i = 0; i < _sizeX; i++)
		{
			for (int j = 0; j < _sizeY; j++)
			{
				if(objectArray[i,j]/*.GetComponent<SpriteRenderer>()*/ == null){
					Debug.Log(i + " , " + j + " does not have a SpriteRenderer");
					continue;
				} else {
					rendererArray[i,j] = objectArray[i,j].GetComponent<SpriteRenderer>();
				}
			}
		}
	}

void ChangeSprite(int x, int y, Sprite sprite){
	rendererArray[x, y].sprite = sprite;
}
}