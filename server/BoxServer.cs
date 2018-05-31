using System;
using Fleck;
using Point = System.Drawing.Point;

namespace Server
{
    public class BoxServer
    {
        private Point _point;
        public BoxServer()
        {
            Controller.MouseMoveTo(1920,1080);
            _point=new Point();
            Controller.GetCursorPos(ref _point);

            var server = new WebSocketServer("ws://0.0.0.0:8181");
            
            server.Start(socket =>
            {
                socket.OnOpen = () => Console.WriteLine(@"Open!");
                socket.OnClose = () => Console.WriteLine(@"Close!");
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    var paras=message.Split(',');
                    switch (paras[0])
                    {
                        case "click":
                            Controller.MouseClick();
                            break;
                        case "doubleclick":
                            Controller.MouseClick();
                            Controller.MouseClick();
                            break;
                        case "rightclick":
                            Controller.MouseRightClick();
                            break;
                        case "move":
                            var x = Convert.ToInt32(paras[1]);
                            var y = Convert.ToInt32(paras[2]);
                            Controller.MouseMove(x, y);
                            break;
                        case "moveto":
                            var x1 = Convert.ToInt32(paras[1]);
                            var y1 = Convert.ToInt32(paras[2]);
                            Controller.MouseMoveTo(x1, y1);
                            break;
                        case "openurl":
                            Controller.OpenUrlByChrome(paras[1]);
                            break;
                        case "getpos":
                            Controller.GetCursorPos(ref _point);
                            socket.Send(_point.X.ToString() + "," + _point.Y.ToString());
                            break;
                        default:
                            socket.Send("");
                            break;
                    }
                };
            });


        }

        public Point Point { get => _point; set => _point = value; }
    }
}
