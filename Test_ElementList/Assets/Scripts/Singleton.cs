using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T Instance;

    protected void Awake()
    {
        if(Instance == null)
        {
            Instance = gameObject.GetComponent<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
