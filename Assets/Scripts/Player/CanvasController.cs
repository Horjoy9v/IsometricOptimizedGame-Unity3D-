using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public FasterFireRateDecorator fasterFireRateDecorator;

    public void ActivateAcceleration()
    {
        fasterFireRateDecorator.ActivateAcceleration();
    }

}
