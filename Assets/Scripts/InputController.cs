using UnityEngine;
public sealed class InputController:IExecute
{
     private readonly BasePlayer _basePlayer;
     private readonly BaseCamera _baseCamera;

     public InputController(BasePlayer player,BaseCamera baseCamera)
     {
          _basePlayer = player;
          _baseCamera = baseCamera;
     }

     public void Execute()
     {
          _basePlayer.CmdMove(MoveInput().x,0.0f,MoveInput().y);
          _basePlayer.Rotation(_baseCamera,InputMouse().x,InputMouse().y);
          _basePlayer.CmdClick(Input.GetMouseButtonDown(0));
     }

     private Vector2 InputMouse()
     {
          var mouseX = Input.GetAxis("Mouse X");
          var mouseY = Input.GetAxis("Mouse Y");
          return new Vector2(mouseX,mouseY);
     }

     private Vector2 MoveInput()
     {
          var x = Input.GetAxis("Horizontal");
          var y = Input.GetAxis("Vertical");
          return new Vector2(x, y);
     }
}
