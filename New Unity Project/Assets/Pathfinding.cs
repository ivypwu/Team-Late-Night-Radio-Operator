using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
  public Transform Seeker, Target;

  Grid GridComp;

  void Awake()
  {
    GridComp = GetComponent<Grid>();
  }

  void Update()
  {
    FindPath(Seeker.position, Target.position);
  }

  void FindPath(Vector3 startPos, Vector3 targetPos)
  {
    Node startNode = GridComp.NodeFromWorldPoint(startPos);
    Node targetNode = GridComp.NodeFromWorldPoint(targetPos);

    List<Node> openSet = new List<Node>();
    HashSet<Node> closedSet = new HashSet<Node>();
    openSet.Add(startNode);

    while (openSet.Count > 0)
    {
      Node currentNode = openSet[0];
      for (int i = 1; i < openSet.Count; ++i)
      {
        if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
          currentNode = openSet[i];
      }

      openSet.Remove(currentNode);
      closedSet.Add(currentNode);

      if (currentNode == targetNode)
      {
        RetracePath(startNode, targetNode);
        return;
      }

      foreach(Node neighbor in GridComp.GetNeighbors(currentNode))
      {
        if (!neighbor.Walkable || closedSet.Contains(neighbor))
          continue;

        int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
        if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
        {
          neighbor.gCost = newMovementCostToNeighbor;
          neighbor.hCost = GetDistance(neighbor, targetNode);
          neighbor.Parent = currentNode;

          if (!openSet.Contains(neighbor))
            openSet.Add(neighbor);
        }
      }
    }
  }

  void RetracePath(Node startNode, Node endNode)
  {
    List<Node> path = new List<Node>();
    Node currentNode = endNode;

    while(currentNode != startNode)
    {
      path.Add(currentNode);
      currentNode = currentNode.Parent;
    }

    path.Reverse();

    GridComp.Path = path;
  }

  int GetDistance(Node nodeA, Node nodeB)
  {
    int distX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
    int distY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

    if (distX > distY)
      return 14*distY + 10*(distX - distY);
    return 14*distX + 10*(distY - distX);
  }
}
