using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

    public class Singleton<T> : StaticReference<T> where T : Singleton<T> {
    protected override void Awake() {
        UnityEngine.Debug.Log(string.Format("{0} Awake", GetType().ToString()));

        if (IsInitialized && instance != this) {
#if UNITY_EDITOR
            if (Application.isEditor) {
                if (EditorApplication.isPlaying) {
                    DestroyImmediate(gameObject);
                    UnityEngine.Debug.LogWarningFormat("Trying to instantiate a second instance of Singleton class {0}. Additional Instance was destroyed", GetType().Name);
                }
            }
#else
			Destroy(this.gameObject);
			UnityEngine.Debug.LogWarningFormat("Trying to instantiate a second instance of Singleton class {0}. Additional Instance was destroyed", GetType().Name);
#endif
        }
        else if (!IsInitialized) {
            instance = (T)this;
            searchForInstance = false;

            if (Application.isPlaying) {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}