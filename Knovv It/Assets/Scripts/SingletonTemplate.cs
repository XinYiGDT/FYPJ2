using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool isDontDestroyOnLoad = false;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.Log("Find Success");
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");

                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                        isDontDestroyOnLoad = true;

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so '" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);

                        if (!isDontDestroyOnLoad)
                        {
                            DontDestroyOnLoad(_instance.gameObject);
                            isDontDestroyOnLoad = true;
                            Debug.Log("[Singleton] Setting Instance that is Active In Hierarchy into DontDestroyOnLoad: " + _instance.gameObject.name);
                        }
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;

    public void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }
}
