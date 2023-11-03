using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public FasterFireRateDecorator fasterFireRateDecorator;

    public void ActivateAcceleration()
    {
        fasterFireRateDecorator.ActivateAcceleration();
    }

}
