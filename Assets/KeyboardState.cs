using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardState : MonoBehaviour {

    private static KeyboardState _instance;
    private string searchString;
    private Stack<KeyboardScript> history;
    public KeyboardScript activeKeyboard;
    private KeyboardScript initial;

    public static KeyboardState Instance {  get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        searchString = "";
        history = new Stack<KeyboardScript>();
    }

    public void AddToHistory(KeyboardScript k)
    {
        history.Push(k);
    }

    public void AddToSearch(string s)
    {
        searchString += s;
        Debug.Log(searchString);
    }

    public void Backspace()
    {
        if (searchString.Length > 0)
        {
            searchString = searchString.Substring(0, searchString.Length - 1);
        }
    }

    public void ResetSearch()
    {
        searchString = "";
    }

    public KeyboardScript GetActiveKeyboard()
    {
        return activeKeyboard;
    }

    // Key controls
    void leftSwipe()
    {
        GetActiveKeyboard().leftActivate();
    }

    // @cute boys who code, call me 🔥🔥🔥🔥🔥🔥
    void rightSwipe()
    {
        GetActiveKeyboard().rightActivate();
    }

    void bottomSwipe()
    {
        GetActiveKeyboard().bottomActivate();
    }

    void topSwipe()
    {
        GetActiveKeyboard().topActivate();
    }

    void centerTap()
    {
        GetActiveKeyboard().centerTap();
    }

    public void ChangeActiveKeyboard(KeyboardScript k)
    {
        activeKeyboard.transitionOut();
        k.transitionIn();
        activeKeyboard = k;
    }

    public void ResetActiveKeyboard()
    {
        ChangeActiveKeyboard(initial);
        history.Clear();
    }

    public void ActivatePreviousKeyboard()
    {
        KeyboardScript next = history.Pop();
        ChangeActiveKeyboard(next);
    }

    // Use this for initialization
    void Start () {
        initial = activeKeyboard;
        KeyboardScript[] keyboards = GetComponentsInChildren<KeyboardScript>();
        foreach (KeyboardScript ks in keyboards) {
            ks.transitionOut();
        }
        activeKeyboard.transitionIn();
        KeyboardEventManager.StartListening("LeftSwipe", leftSwipe);
        KeyboardEventManager.StartListening("RightSwipe", rightSwipe);
        KeyboardEventManager.StartListening("TopSwipe", topSwipe);
        KeyboardEventManager.StartListening("BottomSwipe", bottomSwipe);
        KeyboardEventManager.StartListening("CenterTap", centerTap);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
