using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    public ShipController ship;
    public WinLocation winLocation;

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ship != null)
            ship.Jump(ctx);
    }

    public void Thrust(InputAction.CallbackContext ctx)
    {
        if (ship != null)
            ship.Thrust(ctx);
    }

    public void Rotate(InputAction.CallbackContext ctx)
    {
        if (ship != null)
            ship.Rotate(ctx);
    }

    public void ResetLevel(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            LevelManager.singleton.ResetLevel();
    }

    public void TryWin()
    {
        winLocation.Win();
    }
}
