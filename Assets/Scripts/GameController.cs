using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public GameObject prefab;
    public float prefabRadius;
    public Transform parent;


    QuadTree<Point> quadTree;
    [Header("QuadTree parameters")]
    [SerializeField] float QuadTreeRadius;
    [SerializeField] int maxNumberOfNodes; //Max number of nodes before subdiving
    int totalPrefabsCount = 0;


    [Space(5)]
    public bool showBoundry;
    public float searchingRadius;


    Dictionary<int, SpriteRenderer> points;
    void Start()
    {
        quadTree = new QuadTree<Point>(new Rectangle(0, 0, QuadTreeRadius), maxNumberOfNodes);
        points = new Dictionary<int, SpriteRenderer>();
    }
    void Update()
    {
        ClickToAddPoint();
        quadTree.ShowBoundries();

        if(showBoundry)
        {  
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ShowBound(mousePosition.x, mousePosition.y, searchingRadius);

            List<int> pointIDs = quadTree.Query(new Rectangle(mousePosition.x, mousePosition.y, searchingRadius));

            //Turn the point which are in the searching radius to red and the rest to white
            if(pointIDs != null)
            {
                foreach (var key in points.Keys)
                {
                    if(pointIDs.Contains(key))
                        points[key].color = Color.red;
                    else
                        points[key].color = Color.white;
                }
            }
        }
    }

    void ClickToAddPoint()
    {
        if(Input.GetMouseButtonDown(0))
        {  
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            GameObject newObj = Instantiate(prefab, mousePosition, Quaternion.identity);
            newObj.transform.SetParent(parent);
            points.Add(totalPrefabsCount, newObj.GetComponent<SpriteRenderer>());

            InsertInQuadTree(new Point(new Rectangle(mousePosition.x, mousePosition.y, prefabRadius), totalPrefabsCount));
            totalPrefabsCount++;
        }
    }   
    void ShowBound(float x, float y, float radius) 
	{
		Vector2 bottomLeftPoint = new Vector2(x - radius, y - radius);
		Vector2 bottomRightPoint = new Vector2(x + radius, y - radius);
		Vector2 topRightPoint = new Vector2(x + radius, y + radius);
		Vector2 topLeftPoint = new Vector2(x - radius, y + radius);

		Debug.DrawLine(bottomLeftPoint, bottomRightPoint, Color.blue);	//bottomLine
		Debug.DrawLine(bottomLeftPoint, topLeftPoint,  Color.blue);		//leftLine
		Debug.DrawLine(bottomRightPoint, topRightPoint,  Color.blue);	//rightLine
		Debug.DrawLine(topLeftPoint, topRightPoint,  Color.blue);		//topLine
	}   
    void InsertInQuadTree(Point point)
    {
        quadTree.Insert(point);
    }
}
