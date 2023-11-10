using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionMenu : MonoBehaviour
{
    private float cameraMovementSmoothing = 0.3f;
    private byte cameraMoveDistance = 7;
    private Camera mainCamera;
    private Vector3 targetCameraPosition;
    private byte maxCharacterCount = 3;
    private byte selectedCharacterIndex = 1;
    private Text characterNameText;

    private void Start()
    {
        mainCamera = Camera.main;
        targetCameraPosition = mainCamera.transform.position;
        characterNameText = GameObject.FindWithTag("characterName").GetComponent<Text>();

        UpdateCharacterName();
    }

    public void MoveCameraLeft()
    {
        if (selectedCharacterIndex > 1)
        {
            targetCameraPosition -= new Vector3(cameraMoveDistance, 0, 0);
            selectedCharacterIndex--;
            StartCoroutine(MoveCamera(targetCameraPosition));
            UpdateCharacterName();
        }
    }

    public void MoveCameraRight()
    {
        if (selectedCharacterIndex < maxCharacterCount)
        {
            targetCameraPosition += new Vector3(cameraMoveDistance, 0, 0);
            selectedCharacterIndex++;
            StartCoroutine(MoveCamera(targetCameraPosition));
            UpdateCharacterName();
        }
    }

    public void OnPlayButton()
    {
        SetPlayerIndex.CharacterIndex = selectedCharacterIndex;
        SceneManager.LoadScene(1);
    }

    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
        float elapsedTime = 0;
        Vector3 initialPosition = mainCamera.transform.position;

        while (elapsedTime < cameraMovementSmoothing)
        {
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / cameraMovementSmoothing);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
    }

    private void UpdateCharacterName()
    {
        JsonManager.UpdateText($"name.character.{selectedCharacterIndex}", characterNameText);
    }
}
