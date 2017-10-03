using System;
using UnityEngine;
using System.Collections.Generic;

public class MeshGenerator
{
    public static Mesh GetBoardMesh(BattleConfig config)
	{
        int sideSize = config.SideWidth;
        int sizeY = config.Height;
        int neutralSize = config.NeutralWidth;

        int sizeX = sideSize * 2 + neutralSize;

		Vector3[] vertices = new Vector3[sizeX * sizeY * 4];
		int[] triangles = new int[sizeX * sizeY * 6];
		Vector2[] uv = new Vector2[vertices.Length];

		//TODO

        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                int vX = (y * sizeX + x) * 4;
                vertices[vX]        = new Vector3(x, y);
                vertices[vX + 1]    = new Vector3(x + 1, y);
                vertices[vX + 2]    = new Vector3(x + 1, y + 1);
                vertices[vX + 3]    = new Vector3(x, y + 1);

                int tX = (y * sizeX + x) * 6;
                triangles[tX]       = vX;
                triangles[tX + 1]   = vX + 1;
                triangles[tX + 2]   = vX + 2;
                triangles[tX + 3]   = vX;
                triangles[tX + 4]   = vX + 2;
                triangles[tX + 5]   = vX + 3;

                //TODO varies depending on the X
                float xOffset = 0;
                if (x < sideSize)
                    xOffset = 0;
                else if (x < sideSize + 3)
                    xOffset = 0.25f;
                else
                    xOffset = 0.5f;

                uv[vX]      = new Vector2(xOffset, 0);
                uv[vX + 1]  = new Vector2(xOffset + 0.25f, 0);
                uv[vX + 2]  = new Vector2(xOffset + 0.25f, 1);
                uv[vX + 3]  = new Vector2(xOffset, 1);
            }
        }
		
		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;
		mesh.RecalculateNormals ();

		return mesh;
	}

    public static Mesh GetPatternMesh(WarriorPattern pattern, PatternFlags flags, Vector2 position)
    {
        List<Vector2> locations = pattern.GetLocationsForFlags(flags);
        int sizeX = GameData.CurrentBattle.Board.Width;

        Vector3[] vertices = new Vector3[locations.Count * 4];
        int[] triangles = new int[locations.Count * 6];
        Vector2[] uv = new Vector2[vertices.Length];

        position = BoardUtils.BoardToWorldPosition(position);

        //TODO

        int count = 0;
        foreach (Vector2 location in locations)
        {
            int x = (int)(position.x + location.x); //TODO
            int y = (int)(position.y + location.y); //TODO

            if (!BoardUtils.IsInsideBoard(new Vector2(x, y)))
                continue;

            int vX = count * 4;
            vertices[vX]        = new Vector3(x, y);
            vertices[vX + 1]    = new Vector3(x + 1, y);
            vertices[vX + 2]    = new Vector3(x + 1, y + 1);
            vertices[vX + 3]    = new Vector3(x, y + 1);

            int tX = count * 6;
            triangles[tX]       = vX;
            triangles[tX + 1]   = vX + 1;
            triangles[tX + 2]   = vX + 2;
            triangles[tX + 3]   = vX;
            triangles[tX + 4]   = vX + 2;
            triangles[tX + 5]   = vX + 3;

            //TODO varies depending on the X
            float xOffset = 0.75f;
            uv[vX]      = new Vector2(xOffset, 0);
            uv[vX + 1]  = new Vector2(xOffset + 0.25f, 0);
            uv[vX + 2]  = new Vector2(xOffset + 0.25f, 1);
            uv[vX + 3]  = new Vector2(xOffset, 1);

            count++;
        }

        Mesh mesh = new Mesh ();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals ();

        return mesh;
    }
}