using UnityEngine;
using System.Collections;

using NLua;

public class BBB
{
    public void test( )
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaa");
    }
};
public class ExampleBehaviour : MonoBehaviour {
	Lua env;

	void Awake() {
		env = new Lua();
		env.LoadCLRPackage();
		
		env["this"] = this; // Give the script access to the gameobject.
		env["transform"] = transform;
		
		//System.Object[] result = new System.Object[0];
		try {
			//result = env.DoString(source);
			env.DoFile(Application.streamingAssetsPath+"/lua/test.lua");
		} catch(NLua.Exceptions.LuaException e) {
			Debug.LogError(FormatException(e), gameObject);
		}

	}
    public void Test(Vector3 pot, Vector3 ax, float n)
    {
        transform.RotateAround(pot, ax, n);
        //transform.RotateAround (transform.position, Vector3.up, 5 * Time.deltaTime);
    }
	void Start () {
		//Call("Start");
	}
	
	void Update () {
		Call("Update");
	}

	void OnGUI() {
		//Call("OnGUI");
	}

	public System.Object[] Call(string function, params System.Object[] args) {
		System.Object[] result = new System.Object[0];
		if(env == null) return result;
		LuaFunction lf = env.GetFunction(function);
		if(lf == null) return result;
		try {
			// Note: calling a function that does not 
			// exist does not throw an exception.
			if(args != null) {
				result = lf.Call(args);
			} else {
				result = lf.Call();
			}
		} catch(NLua.Exceptions.LuaException e) {
			Debug.LogError(FormatException(e), gameObject);
		}
		return result;
	}

	public System.Object[] Call(string function) {
		return Call(function, null);
	}

	public static string FormatException(NLua.Exceptions.LuaException e) {
		string source = (string.IsNullOrEmpty(e.Source)) ? "<no source>" : e.Source.Substring(0, e.Source.Length - 2);
		return string.Format("{0}\nLua (at {2})", e.Message, string.Empty, source);
	}
}
