//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class testAstar {

//    public Transform StartPosition;//Starting position to pathfind from
//    public Transform TargetPosition;//Starting position to pathfind to

//    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos) {
//        Node StartNode = GridReference.NodeFromWorldPoint(a_StartPos);//Gets the node closest to the starting position
//        Node TargetNode = GridReference.NodeFromWorldPoint(a_TargetPos);//Gets the node closest to the target position

//        List<Node> OpenList = new List<Node>();//List of nodes for the open list
//        HashSet<Node> ClosedList = new HashSet<Node>();//Hashset of nodes for the closed list

//        OpenList.Add(StartNode);//Add the starting node to the open list to begin the program

//        while (OpenList.Count > 0)//Whilst there is something in the open list
//        {
//            Node CurrentNode = OpenList[0];//Create a node and set it to the first item in the open list
//            for (int i = 1; i < OpenList.Count; i++)//Loop through the open list starting from the second object
//            {
//                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].ihCost < CurrentNode.ihCost)//If the f cost of that object is less than or equal to the f cost of the current node
//                {
//                    CurrentNode = OpenList[i];//Set the current node to that object
//                }
//            }
//            OpenList.Remove(CurrentNode);//Remove that from the open list
//            ClosedList.Add(CurrentNode);//And add it to the closed list

//            if (CurrentNode == TargetNode)//If the current node is the same as the target node
//            {
//                GetFinalPath(StartNode, TargetNode);//Calculate the final path
//            }

//            foreach (Node NeighborNode in GridReference.GetNeighboringNodes(CurrentNode))//Loop through each neighbor of the current node
//            {
//                if (!NeighborNode.bIsWall || ClosedList.Contains(NeighborNode))//If the neighbor is a wall or has already been checked
//                {
//                    continue;//Skip it
//                }
//                int MoveCost = CurrentNode.igCost + GetManhattenDistance(CurrentNode, NeighborNode);//Get the F cost of that neighbor

//                if (MoveCost < NeighborNode.igCost || !OpenList.Contains(NeighborNode))//If the f cost is greater than the g cost or it is not in the open list
//                {
//                    NeighborNode.igCost = MoveCost;//Set the g cost to the f cost
//                    NeighborNode.ihCost = GetManhattenDistance(NeighborNode, TargetNode);//Set the h cost
//                    NeighborNode.ParentNode = CurrentNode;//Set the parent of the node for retracing steps

//                    if (!OpenList.Contains(NeighborNode))//If the neighbor is not in the openlist
//                    {
//                        OpenList.Add(NeighborNode);//Add it to the list
//                    }
//                }
//            }

//        }
//    }



//    void GetFinalPath(Node a_StartingNode, Node a_EndNode) {
//        List<Node> FinalPath = new List<Node>();//List to hold the path sequentially 
//        Node CurrentNode = a_EndNode;//Node to store the current node being checked

//        while (CurrentNode != a_StartingNode)//While loop to work through each node going through the parents to the beginning of the path
//        {
//            FinalPath.Add(CurrentNode);//Add that node to the final path
//            CurrentNode = CurrentNode.ParentNode;//Move onto its parent node
//        }

//        FinalPath.Reverse();//Reverse the path to get the correct order

//        GridReference.FinalPath = FinalPath;//Set the final path

//    }

//    int GetManhattenDistance(Node a_nodeA, Node a_nodeB) {
//        int ix = Mathf.Abs(a_nodeA.iGridX - a_nodeB.iGridX);//x1-x2
//        int iy = Mathf.Abs(a_nodeA.iGridY - a_nodeB.iGridY);//y1-y2

//        return ix + iy;//Return the sum
//    }


//    public class Node {

//        public int iGridX;//X Position in the Node Array
//        public int iGridY;//Y Position in the Node Array

//        public bool bIsWall;//Tells the program if this node is being obstructed.
//        public Vector3 vPosition;//The world position of the node.

//        public Node ParentNode;//For the AStar algoritm, will store what node it previously came from so it cn trace the shortest path.

//        public int igCost;//The cost of moving to the next square.
//        public int ihCost;//The distance to the goal from this node.

//        public int FCost {
//            get {
//                return igCost + ihCost;
//            }
//        }//Quick get function to add G cost and H Cost, and since we'll never need to edit FCost, we dont need a set function.

//        public Node(bool a_bIsWall, Vector3 a_vPos, int a_igridX, int a_igridY)//Constructor
//        {
//            bIsWall = a_bIsWall;//Tells the program if this node is being obstructed.
//            vPosition = a_vPos;//The world position of the node.
//            iGridX = a_igridX;//X Position in the Node Array
//            iGridY = a_igridY;//Y Position in the Node Array
//        }

//    }
//}
