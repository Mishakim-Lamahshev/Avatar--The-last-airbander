using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ButtonClickToSkipTimeline : MonoBehaviour
{
    public PlayableDirector timeline;
    public double skipAmountInSeconds;

    // Start is called before the first frame update
    void Start()
    {
        // Get the button component attached to this GameObject
        Button button = GetComponent<Button>();
        
        // Add a listener for the button's click event
        button.onClick.AddListener(SkipTimeline);
    }

    // This method will be called when the button is clicked
    public void SkipTimeline()
    {
        timeline.time += skipAmountInSeconds;
        Debug.Log("Timeline skipped to " + timeline.time);
    }
}
