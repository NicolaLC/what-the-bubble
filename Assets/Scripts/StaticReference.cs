using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


	public class StaticReference<T> : MonoBehaviour where T : StaticReference<T>
	{
		protected static T instance;

		public static T Instance
		{
			get
			{
				if (!IsInitialized && searchForInstance)
				{
					searchForInstance = false;
					T[] objects = FindObjectsOfType<T>();
					if (objects.Length == 1)
					{
						instance = objects[0];

						bool isSingleton = typeof(T).IsSubclassOf(typeof(StaticReference<T>));
						if (Application.isPlaying && isSingleton)
						{
							DontDestroyOnLoad(objects[0].gameObject);
						}
					}
					else if (objects.Length > 1)
					{
						Debug.LogErrorFormat("Expected exactly 1 {0} but found {1}.", typeof(T).Name, objects.Length);
					}
				}
				return instance;
			}
		}

		protected static bool searchForInstance = true;

		public static bool IsInitialized
		{
			get
			{
				return instance != null;
			}
		}

		public static void AssertIsInitialized()
		{
			Debug.Assert(IsInitialized, string.Format("The {0} StaticReference has not been initialized.", typeof(T).Name));
		}

		protected virtual void Awake()
		{
			if (IsInitialized && instance != this)
			{
#if UNITY_EDITOR
				if (Application.isEditor)
				{
					if (EditorApplication.isPlaying)
					{
						DestroyImmediate(gameObject);
						Debug.LogWarningFormat("Trying to instantiate a second instance of StaticReference class {0}. Additional Instance was destroyed", GetType().Name);
					}
				}
#else
			Destroy(this.gameObject);
			Debug.LogWarningFormat("Trying to instantiate a second instance of StaticReference class {0}. Additional Instance was destroyed", GetType().Name);
#endif
			}
			else if (!IsInitialized)
			{
				instance = (T)this;
				searchForInstance = false;
			}
		}

		protected virtual void OnDestroy()
		{
			if (instance == this)
			{
				instance = null;
				searchForInstance = true;
			}
		}
	}