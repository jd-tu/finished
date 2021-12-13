using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void OpenGithubPage()
	{
		#if !UNITY_EDITOR
		openWindow("https://github.com/jd-tu/finished");
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}