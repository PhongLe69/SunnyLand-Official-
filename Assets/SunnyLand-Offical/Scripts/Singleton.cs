using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T: class, new()
{ 
    private static T instance;

    public static T INSTANCE { private set => instance = value; get
        {
            if (instance == null)
                instance = new T();

            return instance;
        }
    }
}
