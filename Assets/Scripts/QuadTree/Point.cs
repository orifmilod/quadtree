struct Point : IHasRect, IHasID
{
	public Rectangle rect { get; }
	public int ID { get; }
	public Point(Rectangle rectangle, int ID)
	{
		this.rect = rectangle;
		this.ID = ID;
	}
	public Point(float x, float y, float radius, int ID)
	{
		this.rect = new Rectangle(x, y, radius);
		this.ID = ID;
	}
}
