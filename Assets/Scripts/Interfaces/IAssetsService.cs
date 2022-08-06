using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IAssetsService
    {
        void SaveAssetMap(GameObject objectName, string fileName);
    }
}