using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasePlayerMovement : MonoBehaviour {
    private bool movementIsFast = false;
    private float movementFactorSlow = 1f;
    private float movementFactorFast = 2f;
    private PlayerMovementStatus movementStatus;
    private KeyboardWalkDirections keyboardWalkDirections;

    public void SetModeIdle () {
        if (movementStatus == PlayerMovementStatus.idle)
            return;

        movementStatus = PlayerMovementStatus.idle;
        BasePlayer.localPlayer.Stand ();
        BasePlayer.localPlayer.playerRotation.ShouldFollowPlayer(false);
        BasePlayer.localPlayer.ignoreWallCollisions = false;
        if (HUD.instance != null)
            HUD.instance.positionText.text = "Stehen";

        if (keyboardWalkDirections == null)
            keyboardWalkDirections = new KeyboardWalkDirections ();
        keyboardWalkDirections.Reset ();
    }

    public void SetModeWalk () {
        if (movementStatus == PlayerMovementStatus.walk)
            return;

        movementStatus = PlayerMovementStatus.walk;
        BasePlayer.localPlayer.Stand ();
        BasePlayer.localPlayer.playerRotation.ShouldFollowPlayer(false);
        BasePlayer.localPlayer.ignoreWallCollisions = false;
        if (HUD.instance != null)
            HUD.instance.positionText.text = "Laufen";
    }

    public void SetModeCrawl () {
        if (movementStatus == PlayerMovementStatus.crawl)
            return;

        movementStatus = PlayerMovementStatus.crawl;
        BasePlayer.localPlayer.Crawl ();
        BasePlayer.localPlayer.playerRotation.ShouldFollowPlayer(false);
        BasePlayer.localPlayer.ignoreWallCollisions = false;
        if (HUD.instance != null)
            HUD.instance.positionText.text = "Hocken";

        if (keyboardWalkDirections == null)
            keyboardWalkDirections = new KeyboardWalkDirections ();
        keyboardWalkDirections.Reset ();
    }

    public void SetModeFollow () {
        if (movementStatus == PlayerMovementStatus.follow)
            return;

        movementStatus = PlayerMovementStatus.follow;
        BasePlayer.localPlayer.Stand ();
        BasePlayer.localPlayer.playerRotation.ShouldFollowPlayer(true);
        BasePlayer.localPlayer.ignoreWallCollisions = true;
        if (HUD.instance != null)
            HUD.instance.positionText.text = "Folgen";

        if (keyboardWalkDirections == null)
            keyboardWalkDirections = new KeyboardWalkDirections ();
        keyboardWalkDirections.Reset ();
    }

    void Start () {
        SetModeIdle ();
    }

    void Update () {
        CheckKeyboardInput ();
        UpdateMovement ();
    }

    void CheckKeyboardInput () {
        if (keyboardWalkDirections == null)
            keyboardWalkDirections = new KeyboardWalkDirections ();

        bool shouldFollow = false;
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;
        if (keyboard != null) {
            // check movement input
            if (mouse != null)
                keyboardWalkDirections.Forward (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed || mouse.leftButton.isPressed);
            else
                keyboardWalkDirections.Forward (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed );
            keyboardWalkDirections.Backward (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed );
            keyboardWalkDirections.Left (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed );
            keyboardWalkDirections.Right (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed );

            // check follow-mode
            if (keyboard.spaceKey.isPressed) {
                shouldFollow = true;
            }
        }

        if (shouldFollow) {
            SetModeFollow();
        } else if (keyboardWalkDirections.AnyDirectionActive ()) {
            // Key currently pressed
            SetModeWalk ();
        } else if (keyboardWalkDirections.keyboardActive) {
            // no Key currently pressed, but Keyboard was active recently
            SetModeIdle ();
        }
    }

    public void ToggleMovementSpeed () {
        movementIsFast = !movementIsFast;
    }

    void UpdateMovement () {
        if (movementStatus == PlayerMovementStatus.walk) {
            Vector3 forwardMovement = Camera.main.transform.forward;
            forwardMovement.y = 0;
            Vector3 sidewayMovement = Camera.main.transform.right;
            sidewayMovement.y = 0;

            if (keyboardWalkDirections.forward || !keyboardWalkDirections.AnyDirectionActive ()) {
                if (movementIsFast) transform.position += forwardMovement * movementFactorFast * Time.deltaTime;
                if (!movementIsFast) transform.position += forwardMovement * movementFactorSlow * Time.deltaTime;
            }
            if (keyboardWalkDirections.backward) {
                if (movementIsFast) transform.position -= forwardMovement * movementFactorFast * Time.deltaTime;
                if (!movementIsFast) transform.position -= forwardMovement * movementFactorSlow * Time.deltaTime;
            }
            if (keyboardWalkDirections.right) {
                if (movementIsFast) transform.position += sidewayMovement * movementFactorFast * Time.deltaTime;
                if (!movementIsFast) transform.position += sidewayMovement * movementFactorSlow * Time.deltaTime;
            }
            if (keyboardWalkDirections.left) {
                if (movementIsFast) transform.position -= sidewayMovement * movementFactorFast * Time.deltaTime;
                if (!movementIsFast) transform.position -= sidewayMovement * movementFactorSlow * Time.deltaTime;
            }
        } else if (movementStatus == PlayerMovementStatus.follow) {
            GameObject target = OnlinePlayer.followPlayer?.player?.followPlayerLookAnchor;
            if (target != null) {
                Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                Vector3 followPositionKeepDistance = Vector3.MoveTowards(targetPosition, transform.position, 3f);
                transform.position = Vector3.MoveTowards(transform.position, followPositionKeepDistance, movementFactorFast * Time.deltaTime);
            }
        }
    }

    public void ToggleModeCrawl () {
        if (movementStatus != PlayerMovementStatus.crawl) {
            SetModeCrawl ();
        } else {
            SetModeIdle ();
        }
    }
}

public enum PlayerMovementStatus { load, idle, walk, crawl, follow }
public class KeyboardWalkDirections {
    public bool keyboardActive = false;
    public bool forward = false;
    public bool backward = false;
    public bool left = false;
    public bool right = false;

    public bool AnyDirectionActive () {
        return forward || backward || left || right;
    }

    public void Forward (bool status) {
        SetKeyboardActive (status);
        forward = status;
    }

    public void Backward (bool status) {
        SetKeyboardActive (status);
        backward = status;
    }

    public void Left (bool status) {
        SetKeyboardActive (status);
        left = status;
    }

    public void Right (bool status) {
        SetKeyboardActive (status);
        right = status;
    }

    private void SetKeyboardActive (bool stauts) {
        if (stauts == true)
            keyboardActive = true;
    }

    public void Reset () {
        keyboardActive = false;
        forward = false;
        backward = false;
        left = false;
        right = false;
    }
}
