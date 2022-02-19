using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class contains convenient functions for finding all the children contained inside a GameObject parent.
/// For example, in the "Main Menu" scene there is a GameObject called "Main Menu". One of its children is "Main Buttons".
/// The children of "Main Buttons" are "TitleText", "Puzzles", "Sandbox"... etc.
/// 
/// If we wanted to find all the children of "Main Menu", we could use GetAllChildren to get back a list of 
/// all those children, grandchildren, great-grandchildren.
/// 
/// These functions are generally useful in any Unity3D project, and not just coulombic.
/// </summary>
public static class ParentChildFunctions
{
    /// <summary>
    /// Returns a list of all children, grandchildren, great-grandchildren... etc, all the way down to the deepest ancestor.
    /// </summary>
    public static ArrayList GetAllChildren(GameObject parentGameObject, bool includeParent = false)
    {
        string[] excludeSubstrings = new string[0];
        return GetAllChildren(parentGameObject, excludeSubstrings, includeParent);
    }

    /// <summary>
    /// Return a list of the parent, grandparent, great-grandparent... etc, all the way up to the root of the scene.
    /// </summary>
    public static List<GameObject> GetAllParents(GameObject childGameObject)
    {
        List<GameObject> parents = new List<GameObject>();
        while (childGameObject.transform.parent != null)
        {
            GameObject parent = childGameObject.transform.parent.gameObject;
            parents.Add(parent);
            childGameObject = parent;
        }
        return parents;
    }

    /// <summary>
    /// Enable or disable the colliders (the code that detects collisions between objects) of all the children of some parent GameObject.
    /// </summary>
    public static void SetCollidersOfChildren(GameObject parentGameObject, bool isColliderEnabled, bool includeParent = false)
    {
        foreach (GameObject child in GetAllChildren(parentGameObject, includeParent))
        {
            if (child.GetComponent<MeshCollider>() != null)
                child.GetComponent<MeshCollider>().enabled = isColliderEnabled;
            if (child.GetComponent<Collider>() != null)
                child.GetComponent<Collider>().enabled = isColliderEnabled;
            if (child.GetComponent<SphereCollider>() != null)
                child.GetComponent<SphereCollider>().enabled = isColliderEnabled;
            if (child.GetComponent<BoxCollider>() != null)
                child.GetComponent<BoxCollider>().enabled = isColliderEnabled;
        }
    }

    /// <summary>
    /// Returns a list of all children, grandchildren, great-grandchildren... etc, all the way down to the deepest ancestor.
    /// Exclude any children if their name contains any of the strings in the array excludeSubstrings.
    /// </summary>
    public static ArrayList GetAllChildren(GameObject parentGameObject, string[] excludeSubstrings, bool includeParent = false)
    {
        ArrayList children = new ArrayList();

        if (includeParent)
            children.Add(parentGameObject);

        for (int i = 0; i < parentGameObject.transform.childCount; i++)
        {
            GameObject child = parentGameObject.transform.GetChild(i).gameObject;
            bool excludeChild = false;
            foreach (string substring in excludeSubstrings)
            {
                if (child.name.Contains(substring))
                {
                    excludeChild = true;
                    break;
                }
            }
            if (excludeChild)
                continue;

            children.Add(child);
            if (child.transform.childCount > 0)
                children.AddRange(GetAllChildren(child, excludeSubstrings, false));
        }
        return children;
    }
}
