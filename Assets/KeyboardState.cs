using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardState : MonoBehaviour {

    private static KeyboardState _instance;
    private string searchString;
    public AudioClip clip;
    AudioSource audioSource;
    private bool isCaps;
    private bool isKeyboardActivated;
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
        isCaps = false;
        history = new Stack<KeyboardScript>();
    }

    public void AddToHistory(KeyboardScript k)
    {
        history.Push(k);
    }

    public void AddToSearch(string s)
    {
        changeSearchString(searchString + s);
    }

    public void toggleShift()
    {
        isCaps = !isCaps;
    }

    public bool getIsCaps()
    {
        return isCaps;
    }

    public void Backspace()
    {
        if (searchString.Length > 0)
        {
            changeSearchString(searchString.Substring(0, searchString.Length - 1));
        }
    }

    public void ResetSearch()
    {
        changeSearchString("");
    }

    public KeyboardScript GetActiveKeyboard()
    {
        return activeKeyboard;
    }

    void playClickyNoise()
    {
        audioSource.PlayOneShot(clip);
    }

    // Key controls
    void leftSwipe(int val)
    {
        if (isKeyboardActivated)
        {
            GetActiveKeyboard().leftActivate();
            playClickyNoise();
        }
    }

    void rightSwipe(int val)
    {
        if (isKeyboardActivated)
        {
            GetActiveKeyboard().rightActivate();
            playClickyNoise();
        }
    }

    void bottomSwipe(int val)
    {
        if (isKeyboardActivated)
        {
            GetActiveKeyboard().bottomActivate();
            playClickyNoise();
        }

    }

    void topSwipe(int val)
    {
        if (isKeyboardActivated)
        {
            GetActiveKeyboard().topActivate();
            playClickyNoise();
        }
    }

    void centerTap(int val)
    {
        if (isKeyboardActivated)
        {
            GetActiveKeyboard().centerTap();
            playClickyNoise();
        }

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

    public void DeactivateKeyboard()
    {
        ResetActiveKeyboard();
        activeKeyboard.transitionOut();
        isKeyboardActivated = false;
    }

    public void ReactivateKeyboard()
    {
        activeKeyboard.transitionIn();
        isKeyboardActivated = true;
    }

    public void ActivatePreviousKeyboard()
    {
        if (history.Count == 0)
        {
            ResetActiveKeyboard();
            return;
        }
        KeyboardScript next = history.Pop();
        ChangeActiveKeyboard(next);
    }

    void setSearchText()
    {
        Component[] components = GetComponentsInChildren<Component>();
        foreach (Component c in components)
        {
            UnityEngine.UI.Text text = c.gameObject.GetComponent<UnityEngine.UI.Text>();
            if (text && text.CompareTag("Filename"))
            {
                text.text = searchString;
            }
        }
    }

    public string getSearchText()
    {
        return searchString;
    }

    void changeSearchString(string newStr)
    {
        searchString = newStr;
        setSearchText();
    }

    // Use this for initialization
    void Start () {
        initial = activeKeyboard;
        audioSource = GetComponent<AudioSource>();
        isKeyboardActivated = true;
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
