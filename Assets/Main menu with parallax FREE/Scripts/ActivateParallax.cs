using UnityEngine;
using System.Collections;

public class ActivateParallax : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	 if(gameObject.transform.GetSiblingIndex() == 0)
        {
          var parallaxx = gameObject.transform.GetComponentsInChildren<Parallaxx>();
            for (int i = 0; i < parallaxx.Length; i++)
            {
                parallaxx[i].isActive = true;
            }
        }else
        {
            var parallaxx = gameObject.transform.GetComponentsInChildren<Parallaxx>();
            for (int i = 0; i < parallaxx.Length; i++)
            {
                parallaxx[i].isActive = false;
            }
        }
	}
}
