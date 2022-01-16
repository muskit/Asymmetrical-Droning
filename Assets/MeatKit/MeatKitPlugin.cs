#if H3VR_IMPORTED
using System.IO;
using System.Reflection;
using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;

using muskit;

/*
 * SUPER LARGE WARNING ABOUT THIS CLASS
 * This class can be used to add custom behaviour to your generated BepInEx plugin.
 * Please note, however, that all of the things in here already are REQUIRED and CANNOT BE CHANGED.
 * There are LARGE TEXT WARNINGS above such items so you don't forget.
 * You may add to this class so long as you do not modify anything with those notices (lest you want build errors)
 *
 * The class name and BepInPlugin attribute are modified at build-time to reflect your build settings.
 * BepInDependency attributes will automatically be generated if they're required by a build item, otherwise
 * may add it yourself here.
 */

// DO NOT REMOVE OR CHANGE ANY OF THESE ATTRIBUTES
[BepInPlugin("MeatKit", "MeatKit Plugin", "1.0.0")]
[BepInProcess("h3vr.exe")]

// DO NOT CHANGE THE NAME OF THIS CLASS.
public class MeatKitPlugin : BaseUnityPlugin
{
    // DO NOT CHANGE OR REMOVE THIS FIELD.
    private static readonly string BasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    private bool initialized = false;

    // config options
    public static float mouseSpeed = 1f;

	private AssetBundle bundle;
	private GameObject desktopUI;

    private void Awake()
    {
        bundle = AssetBundle.LoadFromFile(Path.Combine(BasePath, "msk_asymmetricaldroning"));
        SceneManager.sceneLoaded += OnSceneChange;
        // TODO: Load config

        Instantiate(bundle.LoadAsset<GameObject>("IntroText"));

        LoadAssets(); // DO NOT REMOVE
    }

    // DO NOT CHANGE OR REMOVE THIS METHOD. It's contents will be overwritten when building your package.
    private void LoadAssets()
    {
    }

	private void Start()
	{
		desktopUI = Instantiate(bundle.LoadAsset<GameObject>("MasterUI"));
		desktopUI.GetComponent<MasterUI>().init(bundle);
        desktopUI.transform.GetChild(0).gameObject.SetActive(initialized);

        initialized = true;
		Logger.LogInfo("\"Finally, multiplayer in H3VR.\"");
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
            // disable only Canvas so we retain UI input
            desktopUI.transform.GetChild(0).gameObject.SetActive(!desktopUI.transform.GetChild(0).gameObject.activeInHierarchy);
    }

    private void OnSceneChange(Scene arg0, LoadSceneMode arg1)
    {
        Destroy(desktopUI);
        Start();
    }
}
#endif
