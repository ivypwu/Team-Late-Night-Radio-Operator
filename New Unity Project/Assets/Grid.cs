using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
  public LayerMask UnwalkableMask;
  public Vector2 GridWorldSize;
  public float NodeRadius;
  Node[,] GridArray;

  float NodeDiameter;
  int GridSizeX, GridSizeY;

  void Start()
  {
    NodeDiameter = NodeRadius * 2.0f;
    GridSizeX = Mathf.RoundToInt(GridWorldSize.x / NodeDiameter);
    GridSizeY = Mathf.RoundToInt(GridWorldSize.y / NodeDiameter);
    CreateGrid();
  }

  void CreateGrid()
  {
    GridArray = new Node[GridSizeX, GridSizeY];
    Vector3 worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

    for (int x = 0; x < GridSizeX; ++x)
    {
      for (int y = 0; y < GridSizeY; ++y)
      {
        Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius) + Vector3.forward * (y * NodeDiameter + NodeRadius);

        bool walkable = !(Physics.CheckSphere(worldPoint, NodeRadius, UnwalkableMask));

        GridArray[x, y] = new Node(walkable, worldPoint, x, y);
      }
    }
  }

  public List<Node> GetNeighbors(Node node)
  {
    List<Node> neighbors = new List<Node>();

    for(int x = -1; x <= 1; ++x)
    {
      for(int y = -1; y <= 1; ++y)
      {
        if (x == 0 && y == 0)
          continue;

        int checkX = node.GridX + x;
        int checkY = node.GridY + y;

        if(checkX >= 0 && checkX < GridSizeX && checkY >= 0 && checkY < GridSizeY)
        {
          neighbors.Add(GridArray[checkX, checkY]);
        }
      }
    }

    return neighbors;
  }

  public Node NodeFromWorldPoint(Vector3 worldPos)
  {
    float percentX = (worldPos.x + GridWorldSize.x / 2) / GridWorldSize.x;
    float percentY = (worldPos.z + GridWorldSize.y / 2) / GridWorldSize.y;
    percentX = Mathf.Clamp01(percentX);
    percentY = Mathf.Clamp01(percentY);

    int x = Mathf.RoundToInt((GridSizeX - 1) * percentX);
    int y = Mathf.RoundToInt((GridSizeY - 1) * percentY);

    return GridArray[x, y];
  }

  public List<Node> Path;

  void OnDrawGizmos()
  {
    Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1.0f, GridWorldSize.y));

    if (GridArray != null)
    {
      foreach(Node n in GridArray)
      {
        Gizmos.color = (n.Walkable) ? Color.white : Color.red;
        if(Path != null)
          if(Path.Contains(n))
            Gizmos.color = Color.black;
        Gizmos.DrawCube(n.WorldPosition, Vector3.one * (NodeDiameter - 0.1f));
      }
    }
  }
}
