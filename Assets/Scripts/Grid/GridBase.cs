﻿using System.Collections.Generic;
using UnityEngine;

public abstract class GridBase : MonoBehaviour
{
    [Header("Regen")]
    [SerializeField] bool regen;

    [Header("Grid Base")]
    [SerializeField] Vector3 startPosition = Vector3.zero;
    [Tooltip("When import texture, set Non-Power of 2 to None, and enable Read/Write")] [SerializeField] Texture2D gridImage = default;
    [SerializeField] Vector3 tileSize = Vector3.one;

    public Dictionary<Vector2Int, TileBase> Grid = new Dictionary<Vector2Int, TileBase>();

    void OnValidate()
    {
        //regen grid
        if(regen)
        {
            regen = false;

            RegenGrid();
            SetUndo();
        }
    }

    void Awake()
    {
        //update in editor doesn't save dictionary, so we need to regenerate it
        GenerateReferences();
    }

    #region regen grid

    void RegenGrid()
    {
        //remove old grid and generate new one
        RemoveOldGrid();
        GenerateGrid();
    }

    void SetUndo()
    {
        //set undo
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterFullObjectHierarchyUndo(gameObject, "Regen World");
#endif
        //foreach(Transform child in transform)
        //{
        //    Undo.RecordObject(child, "Regen World");
        //}

        //set scene dirty
        //using UnityEditor.SceneManagement;
        //EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    void RemoveOldGrid()
    {
        //remove every child
        foreach (Transform ch in transform)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += () =>
            {
                DestroyImmediate(ch.gameObject);
            };
#else
            Destroy(ch.gameObject);
#endif
        }

        //then clear dictionary
        Grid.Clear();
    }

    void GenerateGrid()
    {
        //for every pixel in image
        for(int x = 0; x < gridImage.width; x++)
        {
            for(int y = 0; y < gridImage.height; y++)
            {
                //get color pixel from grid image
                Color pixel = gridImage.GetPixel(x, y);

                //instantiate tile
                TileBase tile = Instantiate(SelectTile(pixel), transform);
                tile.transform.position = startPosition + new Vector3(x * tileSize.x, 0, y * tileSize.z);
                tile.transform.rotation = Quaternion.identity;

                //init tile and add to dictionary
                tile.Init(pixel);
                Grid.Add(new Vector2Int(x, y), tile);
            }
        }
    }

    #endregion

    #region awake

    void GenerateReferences()
    {
        //create dictionary
        Grid.Clear();
        foreach (Transform child in transform)
        {
            TileBase tile = child.GetComponent<TileBase>();
            if (tile != null)
            {
                Vector2Int index = new Vector2Int(
                    Mathf.FloorToInt(tile.transform.position.x / tileSize.x), 
                    Mathf.FloorToInt(tile.transform.position.z / tileSize.z));

                Grid.Add(index, tile);
            }
        }
    }

    #endregion

    protected abstract TileBase SelectTile(Color pixel);
}
