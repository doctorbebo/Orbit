using UnityEngine;
using TMPro;


public class HowToPlayScript : MonoBehaviour
{
    public ShipController shipController;

    float pauseAngle = 10;
    public TextMeshProUGUI howToPlayText;

    float buffer = 0.25f; 

    int showHowToPlayInterations = 0;

    private void Start()
    {
        if (ShipController.gameMode == ShipController.GameMode.Playing)
            this.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (ShipController.gameMode == ShipController.GameMode.Learning)
        {
            //this Makes sure the ship completes at lease half a rotation before pausing. 
            if (shipController.angle <= 3 && shipController.angle >= 2)
            {
                pauseAngle = 6.2f;
            }

            else if (shipController.angle <= pauseAngle + buffer && shipController.angle >= pauseAngle - buffer)
            {
                switch (showHowToPlayInterations)
                {
                    case 0:
                        Time.timeScale = 0;
                        ShowOrHideHelpText(true);
                        pauseAngle = 10;
                        showHowToPlayInterations++;
                        break;
                    case 1:
                        Time.timeScale = 0;
                        ShowOrHideHelpText(true);
                        pauseAngle = 10;
                        showHowToPlayInterations++;
                        break;
                    case 2:
                        Time.timeScale = 0;
                        ShowOrHideHelpText(true);
                        pauseAngle = 10;
                        showHowToPlayInterations++;
                        break;
                    default:
                        ShowOrHideHelpText(false);
                        ShipController.gameMode = ShipController.GameMode.Playing;
                        this.enabled = false;
                        break;

                }
            }
        }
        else if (ShipController.gameMode == ShipController.GameMode.Playing)
        {
            this.enabled = false;
        }
    }      
    public void ShowOrHideHelpText (bool show)
    {
        howToPlayText.enabled = show;
    }
}
