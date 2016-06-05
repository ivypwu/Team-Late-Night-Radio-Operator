using UnityEngine;
using System.Collections;

public class Node
{
  public bool Walkable;
  public Vector3 WorldPosition;
  public int GridX;
  public int GridY;

  public int gCost;
  public int hCost;
  public Node Parent;

  public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
  {
    Walkable = _walkable;
    WorldPosition = _worldPos;
    GridX = _gridX;
    GridY = _gridY;
  }

  public int fCost
  {
    get
    {
      return gCost + hCost;
    }
  }
}
