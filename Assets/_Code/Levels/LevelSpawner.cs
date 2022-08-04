using System.Collections.Generic;
using Data;
using Data.Levels;
using EditorExtensions.Attributes;
using Profile;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    /// <summary>
    /// Example how to use signals for level generation.
    /// Can be removed or extended at will.
    /// </summary>
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField, RequireInput]
        private LevelList _levelList = default;

        private int loadedLevel = -1;

        private void Awake()
        {
            Hub.LoadLevel.Subscribe(x => LoadLevel()).AddTo(this);

            // TODO This should be moved where actual game logic is evaluated
            Hub.LevelProgressChanged.Subscribe(progress =>
            {
                if (progress >= 1f)
                {
                    Hub.LevelComplete.Fire();
                    PlayerProfile.CurrentLevel++;
                }
            }).AddTo(this);
        }

        private void Start()
        {
            Hub.LoadLevel.Fire();
        }

        private void LoadLevel()
        {
            if (loadedLevel >= 0)
            {
                AsyncOperation levelUnload = SceneManager.UnloadSceneAsync("Level" + loadedLevel);

                levelUnload.completed += x => LoadLevelScene();
            }
            else
            {
                LoadLevelScene();
            }
        }

        private void LoadLevelScene()
        {
            int level = PlayerProfile.CurrentLevel;

            List<LevelData> allLevels = _levelList.AllLevels;

            level = level >= allLevels.Count ? allLevels.Count - 1 : level;

            loadedLevel = level;

            AsyncOperation levelLoad = SceneManager.LoadSceneAsync("Level" + level, LoadSceneMode.Additive);

            levelLoad.completed += x =>
            {
                Hub.LevelSceneLoaded.Fire(level);
            };
        }
    }
}