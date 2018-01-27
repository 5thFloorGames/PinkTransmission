using UnityEditor;
using UnityEngine;

public class CreateBoardEditor : MonoBehaviour {
	static int YSize = 9;
	static int XSize = 9;
	static GameObject tile;
	static GameObject tileParent;

	[MenuItem ("Create/TileGrid")]
	static void GenerateGrid(){
		tile = Resources.Load<GameObject>("Tile");
		tileParent = GameObject.Find("Plane");
		for(int i = 0;i<=XSize;i++){
			for(int j = 0;j<=YSize;j++){
				GameObject g = (GameObject)PrefabUtility.InstantiatePrefab (tile);
				g.transform.parent = tileParent.transform;
				g.transform.localPosition = new Vector3(i - XSize/2f,0,j - YSize/2f);
				
			}
		}
	}

}
