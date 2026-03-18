using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BFS : MonoBehaviour
{
    [SerializeField]
    Transform roadTilemap;

    Transform playerTr, goalTr;
    Vector3 lastPlayerPos = -Vector3.one;

    Dictionary<float, int> xToCol;
    Dictionary<float, int> zToRow;
    Transform[,] mapTiles;
    int[,] mapData;

    Vector2[] dirs = { new Vector2(0, -1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0) };

    int[,] distFromStart;
    bool[,] visited;
    Vector2 checkingPos = Vector2.zero;
    Vector2[,] prevTile;
    Queue<Vector2> q = new Queue<Vector2>();
    HashSet<Transform> pathTiles = new HashSet<Transform>();


    public void InitialSetup(Transform player, Transform goal)
    {
        playerTr = player;
        goalTr = goal;

        // Obtiene las posiciones unicas en x y en z de las casillas
        HashSet<float> xs = new HashSet<float>();
        HashSet<float> zs = new HashSet<float>();
        foreach (Transform child in roadTilemap)
        {
            xs.Add(child.transform.position.x);
            zs.Add(child.transform.position.z);
        }

        // Ordena las posiciones de las casillas de izquierda a derecha (x de menor a mayor) y de arriba abajo (z de mayor a menor)
        List<float> sortedXs = xs.OrderBy(x => x).ToList();
        List<float> sortedZs = zs.OrderByDescending(x => x).ToList();

        // Se mapea cada posicion ordenada a un indice de casilla
        xToCol = sortedXs.Select((x, i) => new { x, i }).ToDictionary(item => item.x, item => item.i);
        zToRow = sortedZs.Select((z, i) => new { z, i }).ToDictionary(item => item.z, item => item.i);

        mapTiles = new Transform[sortedZs.Count, sortedXs.Count];
        mapData = new int[sortedZs.Count, sortedXs.Count];

        // Se recorren de nuevo todas las casillas
        foreach (Transform child in roadTilemap)
        {
            // Se obtienen las posiciones de la casilla
            float x = child.position.x;
            float z = child.position.z;

            // Si la casilla tiene un indice asociado tanto en x como en z
            if (xToCol.ContainsKey(x) && zToRow.ContainsKey(z))
            {
                // Se guarda su transform en los indices indicados y se guarda que hay una casilla navegable en dichos indices
                mapTiles[zToRow[z], xToCol[x]] = child;
                mapData[zToRow[z], xToCol[x]] = 1;
            }
        }

        distFromStart = new int[mapData.GetLength(0), mapData.GetLength(1)];
        visited = new bool[mapData.GetLength(0), mapData.GetLength(1)];
        prevTile = new Vector2[mapData.GetLength(0), mapData.GetLength(1)];
    }

    public void CalculateShortestPath()
    {
        GetShortestPath();
    }
    public int GetShortestPath()
    {
        foreach (Transform tile in roadTilemap)
        {
            PathHighlighter highlight = tile.GetComponent<PathHighlighter>();
            if (highlight != null)
            {
                highlight.ActivateHighlight(false);
            }
        }

        int startX = xToCol[playerTr.position.x];
        int startZ = zToRow[playerTr.position.z];

        int endX = xToCol[goalTr.position.x];
        int endZ = zToRow[goalTr.position.z];

        if (lastPlayerPos != playerTr.position)
        {
            lastPlayerPos = playerTr.position;
            
            // Se determina la direccion inicial del jugador para bloquear el recorrido en sentido contrario
            Defs.CarDirections playerDir = (Defs.CarDirections)((int)(playerTr.eulerAngles.y % 360) / 90);
            Vector2 blockedDir = Vector2.zero;
            switch (playerDir)
            {
                case Defs.CarDirections.FORWARD:
                    blockedDir = dirs[(int)Defs.CarDirections.BACK];
                    break;
                case Defs.CarDirections.RIGHT:
                    blockedDir = dirs[(int)Defs.CarDirections.LEFT];
                    break;
                case Defs.CarDirections.BACK:
                    blockedDir = dirs[(int)Defs.CarDirections.FORWARD];
                    break;
                case Defs.CarDirections.LEFT:
                    blockedDir = dirs[(int)Defs.CarDirections.RIGHT];

                    break;
            }


            // Se reinician las distancias y casillas visitadas
            for (int i = 0; i < distFromStart.GetLength(0); i++)
            {
                for (int j = 0; j < distFromStart.GetLength(1); j++)
                {
                    distFromStart[i, j] = -1;
                }
            }
            for (int i = 0; i < visited.GetLength(0); i++)
            {
                for (int j = 0; j < visited.GetLength(1); j++)
                {
                    visited[i, j] = false;
                }
            }
            q.Clear();

            // Se inicializa primera casilla
            distFromStart[startZ, startX] = 0;
            visited[startZ, startX] = true;

            checkingPos.x = startX;
            checkingPos.y = startZ;
            prevTile[startZ, startX] = checkingPos;

            // BFS
            q.Enqueue(checkingPos);
            while (q.Count > 0)
            {
                Vector2 v = q.Dequeue();

                foreach (Vector2 dir in dirs)
                {
                    if (dir != blockedDir)
                    {
                        int nextX = (int)(v.x + (int)dir.x);
                        int nextZ = (int)(v.y + (int)dir.y);

                        if (ValidPos(nextX, nextZ))
                        {
                            if (!visited[nextZ, nextX] && mapData[nextZ, nextX] > 0)
                            {
                                distFromStart[nextZ, nextX] = distFromStart[(int)v.y, (int)v.x] + 1;
                                visited[nextZ, nextX] = true;

                                prevTile[nextZ, nextX] = v;

                                checkingPos.x = nextX;
                                checkingPos.y = nextZ;
                                q.Enqueue(checkingPos);
                            }
                        }
                    }
                    else
                    {
                        blockedDir = Vector2.zero;
                    }
                }
            }

            //string prevDebug = "";
            //for (int z = 0; z < prevTile.GetLength(0); z++)
            //{
            //    for (int x = 0; x < prevTile.GetLength(1); x++)
            //    {
            //        prevDebug += $"({prevTile[z, x].y},{prevTile[z, x].x}) ";
            //    }
            //    prevDebug += "\n";
            //}

            pathTiles.Clear();
            int currX = endX;
            int currZ = endZ;

            while (currX != startX || currZ != startZ)
            {
                int prevX = currX;
                int prevZ = currZ;

                pathTiles.Add(mapTiles[currZ, currX]);

                currX = (int)prevTile[prevZ, prevX].x;
                currZ = (int)prevTile[prevZ, prevX].y;
            }
            pathTiles.Add(mapTiles[startZ, startX]);
        }

        //string pathDebug = "";
        //foreach (Transform lmao in pathTiles)
        //{
        //    pathDebug += $"({zToRow[lmao.position.z]},{xToCol[lmao.position.x]}) ";
        //}

        foreach (Transform tile in pathTiles)
        {
            PathHighlighter highlight = tile.GetComponent<PathHighlighter>();
            if (highlight != null)
            {
                highlight.ActivateHighlight(true);
            }
        }

        return distFromStart[endZ, endX];
    }

    bool ValidPos(int x, int z)
    {
        return x >= 0 && x < mapData.GetLength(1) && z >= 0 && z < mapData.GetLength(0);
    }
}
