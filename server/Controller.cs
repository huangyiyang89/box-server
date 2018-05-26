
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Server
{
    public class Controller
    {
        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void MouseEvent(int flag, int dx, int dy, int data, int extraInfo);

        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public static extern int SetCursorPos(int x, int y);

        public static void MouseMove(int x, int y)
        {
            MouseEvent(0x0001, x, y, 0, 0);
        }
        public static void MouseMoveTo(int x, int y)
        {
            SetCursorPos(x, y);
            //mouse_event((0x8000|0x0001), x*65536 / 1920, y * 65536 / 1080, 0, 0);
        }
        public static void MouseClick()
        {
            MouseEvent(0x0002, 0, 0, 0, 0);//点击
            MouseEvent(0x0004, 0, 0, 0, 0);//抬起
        }

        public static void MouseRightClick()
        {
            MouseEvent(0x0008, 0, 0, 0, 0);//点击
            MouseEvent(0x00010, 0, 0, 0, 0);//抬起
        }

        public static void OpenUrlByChrome(string url)
        {
            var urlShoud = url.Replace("m.youku.com/video","v.youku.com/v_show");
            var myprocess = Process.GetProcessesByName("chrome");
            foreach (var process in myprocess)
            {
                process.CloseMainWindow();
            }
            var processInfo = new ProcessStartInfo("chrome")
            {
                Arguments = "--app=" + urlShoud+" --start-maximized"
            };
            Process.Start(processInfo);
        }
    }
}
