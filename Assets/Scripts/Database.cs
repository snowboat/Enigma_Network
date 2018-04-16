using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Database : MonoBehaviour {

	public static Database _instance;

	public enum Roles{None, Facilitator, Cryptanalyst, Linguist, Mathematician, Journalist};
	public static int totalNumOfScene = 3;

	public static Roles role;
	public static int currentScene = 0;
}
