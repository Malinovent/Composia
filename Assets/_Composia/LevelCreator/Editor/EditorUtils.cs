using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEditor.Animations;


    public static class EditorUtils
    {

        //Create a new scene
        public static void NewScene()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        }

        //Remove all the elements of the scene
        public static void CleanScene()
        {
            GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                GameObject.DestroyImmediate(go);
            }
        }

        //Create a new scene capable to be used as a level
        public static void NewLevel()
        {
            NewScene();
            CleanScene();
            GameObject levelGo = new GameObject("Level");
            levelGo.transform.position = Vector3.zero;
            //levelGo.AddComponent<Level>();
        }

        public static List<T> GetAssetsWithScript<T>(string path) where T : MonoBehaviour
        {
            T tmp;
            string assetPath;
            GameObject asset;
            List<T> assetList = new List<T>();
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { path });
            for (int i = 0; i < guids.Length; i++)
            {
                assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;
                tmp = asset.GetComponent<T>();
                if (tmp != null)
                {
                    assetList.Add(tmp);
                }
            }
            return assetList;
        }

        private static T GetAssetWithScript<T>(string path) where T : MonoBehaviour
        {
             return AssetDatabase.LoadAssetAtPath<T>(path);     
        }

        public static List<T> GetListFromEnum<T>()
        {
            List<T> enumList = new List<T>();
            System.Array enums = System.Enum.GetValues(typeof(T));
            foreach (T e in enums)
            {
                enumList.Add(e);
            }
            return enumList;
        }

        public static List<AnimatorState> GetAnimatorStateInfo(Animator animator)
        {
            AnimatorController ac = animator.runtimeAnimatorController as AnimatorController;
            AnimatorControllerLayer[] acLayers = ac.layers;
            List<AnimatorState> allStates = new List<AnimatorState>();
            foreach (AnimatorControllerLayer i in acLayers)
            {
                ChildAnimatorState[] animStates = i.stateMachine.states;
                foreach (ChildAnimatorState j in animStates)
                {
                    allStates.Add(j.state);
                }
            }
            return allStates;
        }
    }

