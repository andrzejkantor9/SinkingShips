using UnityEngine;

namespace SinkingShips.Control
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private GameObject[] _persistentObjects;
        #endregion

        #region States
        private static bool _hasSpawned;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            if (_hasSpawned)
                return;

            SpawnPersistentObjects();
            _hasSpawned = true;
        }
        #endregion

        #region Private
        private void SpawnPersistentObjects()
        {
            foreach(GameObject persistentObject in _persistentObjects)
            {
                GameObject spawnedObject = Instantiate(persistentObject);
                DontDestroyOnLoad(spawnedObject);
            }
        }
        #endregion
    }
}
