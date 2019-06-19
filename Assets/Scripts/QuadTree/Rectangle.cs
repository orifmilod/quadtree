using UnityEngine;
public class Rectangle
{
	public float x; 
	public float y; 
	public float radius;
	public Rectangle(float x, float y, float radius) 
	{
		this.y = y;
		this.x = x;
		this.radius = radius ;
	}
	/// <summary>Checks if a point is in the boundry of the rectangle.</summary>
	public bool Contains(float posX, float posY) 
	{
		return 
		posX >= x - radius && 
		posX <= x + radius &&
		posY >= y - radius && 
		posY <= y + radius ;
	}

    public bool Overlaps(Rectangle rectangle)  
    { 
		Vector2 topLeft1 = new Vector2(x - radius, y + radius);
		Vector2 bottomRight1 = new Vector2(x + radius, y - radius);

		
		Vector2 topLeft2 = new Vector2(rectangle.x - rectangle.radius, rectangle.y + rectangle.radius);
		Vector2 bottomRight2 = new Vector2(rectangle.x + rectangle.radius, rectangle.y - rectangle.radius);

        // If one rectangle is on left side of other  
        if (topLeft1.x > bottomRight2.x || topLeft2.x > bottomRight1.x) 
            return false; 
  
        // If one rectangle is above other  
        if (topLeft1.y < bottomRight2.y || topLeft2.y < bottomRight1.y)  
            return false;

        return true; 
	}
}


