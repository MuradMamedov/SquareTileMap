using UnityEngine;
using UnityEditor;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts
{
    public class AssetsServices : IAssetsService
    {
        public void SaveAssetMap(GameObject objectName, string fileName)
        {
            if (objectName)
            {
                var savePath = "Assets/" + fileName + ".prefab";
                if (PrefabUtility.SaveAsPrefabAsset(objectName, savePath))
                {
                    EditorUtility.DisplayDialog("Tilemap saved", "Your Tilemap was saved under" + savePath, "Continue");
                }
                else
                {
                    EditorUtility.DisplayDialog("Tilemap NOT saved", "An ERROR occured while trying to saveTilemap under" + savePath, "Continue");
                }
            }
        }
    }
}