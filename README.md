# QuadTree-Unity 
```
QuadTree<Point> quadTree = new QuadTree<Point>(new Rectangle(0, 0, 100), maxNodesBeforeSubdividing);
```

<h3>A Generic Quad-Tree data-structure implemented in Unity3D.</h3>
<p> Watch video to see an example of how it works :neckbeard: : <a href="https://www.youtube.com/watch?v=NmN3hqPfVY0" target="_blank">VIDEO LINK</a> </p>

<p>
This QuadTree accepts nodes which has inherited interfaces, <i>IHasRect</i> and <i>IHasID</i>.

<h2> Insert Method 🔑</h2>
<p>
You can Insert a node or objects which has inherited <b>IHasRect</b> and <b>IHasID</b> interfaces in the QuadTree and 
you can define how many nodes can a leaf accept before subdividing. 

```
 float positionX = 5;
 float positionY = 5;
 float radius = 1;
 
 // Inserts a point in the QuadTree
 
 Point point = new Point(new Rectangle(positionX, positionY, radius));
 quadTree.Insert(point);
```
 
<p> Here is an representational example of <i>3</i> nodes before subdividing.</p>

<img width="400px" src="https://user-images.githubusercontent.com/25881325/59792394-5ba87700-92d4-11e9-85c0-76c1f425d4a2.png"/>
<img width="400px" src="https://user-images.githubusercontent.com/25881325/59792393-5ba87700-92d4-11e9-9b1c-173672853b22.png"/>
<img width="400px" src="https://user-images.githubusercontent.com/25881325/59792391-5ba87700-92d4-11e9-9cc5-9541cb7cedcc.png"/>
<img width="400px" src="https://user-images.githubusercontent.com/25881325/59792392-5ba87700-92d4-11e9-91cc-894e3c674d8a.png"/>
<img width="400px" src="https://user-images.githubusercontent.com/25881325/59792390-5ba87700-92d4-11e9-96cd-f770687a9b32.png"/>
</p>

<h2> Query Method 🔍 </h2>
<p>
This Query Method returns a List<int> of ID's of the points inside the searching area.
You can Query an area of with an instance of Rectangle which has an <i>X</i> and <i>Y</i> and a <i>Radius</i> to search the points inside the QuadTree.
</p>

 
 ```
 List<int> pointIDs = quadTree.Query(new Rectangle(mousePosition.x, mousePosition.y, searchingRadius));
 ```
 
<h2> ClearAllNodes ❌</h2>
<p>
  This Method sets <i>root = false</i> to and <i>numberOfNodesInserted = 0</i> which makes the Quadtree's array of nodes to be overwritten next time when inserting,
  I don't like to set the array of node to null because next time when inserting we should make a new instance of the array and it reduces performance, 
  so what happens is next time when reusing the inserted nodes overwrites on the array nodes.
</p>

 
 ```
 quadTree.ClearAllNodes();
 ```
 
 
<h5> Any suggestion or advice are welcome and would be appriciated.🙏</h5>

<p> Some other good resources to take a look at. ✅</p>
<a href="https://referencesource.microsoft.com/#System.Activities.Presentation/System.Activities.Presentation/System/Activities/Presentation/View/QuadTree.cs" target="_blank">Microsoft Reference</a>
<br/>
<a href="https://stackoverflow.com/questions/41946007/efficient-and-well-explained-implementation-of-a-quadtree-for-2d-collision-det" target="_blank">Stackoverflow explanation</a>
<br/>
 <a href="https://stackoverflow.com/questions/42873508/quad-tree-and-kd-tree" target="_blank">Stackoverflow explanation 2</a>
