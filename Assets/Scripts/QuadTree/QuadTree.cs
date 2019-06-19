using UnityEngine;
using System.Collections.Generic;
class QuadTree<T> where T : IHasRect, IHasID
{
	public Rectangle boundry;
	T[] nodes;
	bool root = false;
	bool divided = false;
	int numberOfNodesInserted = 0;
    int maxSize;
	QuadTree<T> northEast, northWest, southEast, southWest;
	public int Count()
	{ 
		int result = numberOfNodesInserted;
		if (divided && root)
		{
			result += northEast.Count();
			result += northWest.Count();
			result += southEast.Count();
			result += southWest.Count();
		}
		return result;
	}
	public QuadTree(Rectangle boundry, int size) 
	{
		if(boundry.radius == 0)
			Debug.LogError("Radius of the boundry cannot be zero.");
		
		nodes = new T[size];
        maxSize = size;
		this.boundry = boundry;
	}	
	
	#region Methods

	//Clear all the nodes in the Quad-Tree
	public void ClearAllNodes() 
	{
		if(numberOfNodesInserted == 0 && !root) return;
		numberOfNodesInserted = 0;
		root = false;

		if(divided) 
		{
			northEast.ClearAllNodes();
			northWest.ClearAllNodes();
			southEast.ClearAllNodes();
			southWest.ClearAllNodes();
		}
		divided = false;
	}
	/// <summary>Insert a node in the Quad-Tree</summary>
	public bool Insert(T node) 
	{
		if(node.rect.radius == 0)
			Debug.LogError("Radius of the inserted boundry cannot be zero.");
		
		//Checking if the position is in the boundries of the node.
		if(!boundry.Contains(node.rect.x, node.rect.y)) return false;
		if(numberOfNodesInserted < maxSize && !root) 
		{
			nodes[numberOfNodesInserted] = node;
			numberOfNodesInserted++;
			return true;
		}
		else if(root)
		{
			if(northEast.Insert(node)) return true;			
			if(northWest.Insert(node)) return true;		
			if(southEast.Insert(node)) return true;
			if(southWest.Insert(node)) return true;	
		}
		else if(!root && numberOfNodesInserted == maxSize)
		{
			root = true;
			numberOfNodesInserted = 0;
            
			if(!divided)
				SubDivide();
            

            //Moving current nodes to subnodes
			for (int i = 0; i < maxSize; i++)
			{
				if(!northEast.Insert(nodes[i]))			
				if(!northWest.Insert(nodes[i]))		
				if(!southEast.Insert(nodes[i]))
				if(!southWest.Insert(nodes[i])) { Debug.LogError("It should not reach here"); }
			}

			if(!northEast.Insert(node))			
			if(!northWest.Insert(node))		
			if(!southEast.Insert(node))
			if(!southWest.Insert(node)) { Debug.LogError("It should not reach here"); }
			return true;
		}
		return false;
	}

	/// <summary>Query through QuadTree with a give rectangle and return ID's of all nodes inside the searching area.</summary>
    public List<int> Query(Rectangle searchingArea)
	{
		List<int> foundedPoints = new List<int>();
		if(numberOfNodesInserted == 0 && !root) return foundedPoints;
		if(!boundry.Overlaps(searchingArea)) return foundedPoints;

		if(!root && numberOfNodesInserted != 0)
		{
			for (int i = 0; i < numberOfNodesInserted; i++)
			{
				if(searchingArea.Overlaps(nodes[i].rect)) 
                    foundedPoints.Add(nodes[i].ID);	
			}
			return foundedPoints;
		}
		else if(root && numberOfNodesInserted == 0)
		{ 
			foundedPoints.AddRange(northEast.Query(searchingArea));
			foundedPoints.AddRange(northWest.Query(searchingArea));
			foundedPoints.AddRange(southEast.Query(searchingArea));
			foundedPoints.AddRange(southWest.Query(searchingArea));
		}
		return foundedPoints;
	}
	#endregion
	
	#region HelperMethods

	/// <summary>Divide the Quadtree into 4 equal parts and set it's boundries, NorthEast, NorthWest, SouthEast and SouthWest.</summary>
	private void SubDivide() 
	{
		//Size of the sub boundries 
		if(northEast == null) 
		{	
			float x = boundry.x;
			float y = boundry.y;
			float radius = boundry.radius / 2;
	
			northEast = new QuadTree<T>(new Rectangle(x + radius, y + radius, radius), maxSize);
			northWest = new QuadTree<T>(new Rectangle(x - radius, y + radius, radius), maxSize);
			southEast = new QuadTree<T>(new Rectangle(x + radius, y - radius, radius), maxSize);
			southWest = new QuadTree<T>(new Rectangle(x - radius, y - radius, radius), maxSize);
		} 
		divided = true; 
	}

	// Shows boundires of the quadtree and SubNodes
	// public void LogNodes() 
	// {
	// 	if(numberOfNodesInserted == 0 && !root) return;
	// 	else if(root)
	// 	{
	// 		northEast.LogNodes();
	// 		northWest.LogNodes();
	// 		southWest.LogNodes();
	// 		southEast.LogNodes();
	// 		return;
	// 	}
	// 	else if(numberOfNodesInserted > 0)
	// 	{
	// 		for (int i = 0; i < numberOfNodesInserted; i++)
	// 		{	
	// 			Debug.Log(nodes[i].rect.x + " " + nodes[i].rect.y + " id:" + nodes[i].ID);	
	// 		}
	// 		return;
	// 	}
	// }
	
	public void ShowBoundries()
	{ 
		float r = boundry.radius;
		float x = boundry.x;
		float y = boundry.y;

		Vector2 bottomLeftPoint = new Vector2(x - r, y - r);
		Vector2 bottomRightPoint = new Vector2(x + r, y - r);
		Vector2 topRightPoint = new Vector2(x + r, y + r);
		Vector2 topLeftPoint = new Vector2(x - r, y + r);
		
		Debug.DrawLine(bottomLeftPoint, bottomRightPoint, Color.red);	//bottomLine
		Debug.DrawLine(bottomLeftPoint, topLeftPoint, Color.red);		//leftLine
		Debug.DrawLine(bottomRightPoint, topRightPoint, Color.red);		//rightLine
		Debug.DrawLine(topLeftPoint, topRightPoint, Color.red);			//topLine

		if(divided)
		{
			northEast.ShowBoundries();
			northWest.ShowBoundries();
			southEast.ShowBoundries();
			southWest.ShowBoundries();
		}
	}
	
	public void ShowBound(float x, float y, float h, float w) 
	{
		Vector2 bottomLeftPoint = new Vector2(x - w, y - h);
		Vector2 bottomRightPoint = new Vector2(x + w, y - h);
		Vector2 topRightPoint = new Vector2(x + w, y + h);
		Vector2 topLeftPoint = new Vector2(x - w, y + h);

		Debug.DrawLine(bottomLeftPoint, bottomRightPoint, Color.blue, 5);	//bottomLine
		Debug.DrawLine(bottomLeftPoint, topLeftPoint,  Color.blue, 5);		//leftLine
		Debug.DrawLine(bottomRightPoint, topRightPoint,  Color.blue, 5);	//rightLine
		Debug.DrawLine(topLeftPoint, topRightPoint,  Color.blue, 5);		//topLine
	}
	#endregion
}
