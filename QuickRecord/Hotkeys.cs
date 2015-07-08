using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Hotkeys
{
    public class GlobalHotkey
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vkc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int modifier;
        private int key;
        private IntPtr hWnd;
        private int id;

        public GlobalHotkey(int modifier, Keys key, Form form)
        {
            this.modifier = modifier;
            this.key = (int)key;
            this.hWnd = form.Handle;
            id = this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return modifier ^ key ^ hWnd.ToInt32();
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, id, modifier, key);
        }

        public bool Unregister()
        {
            return UnregisterHotKey(hWnd, id);
        }
    }

    public static class Constants
    {
        //modifiers
        public const int NOMOD = 0x0000;
        public const int ALT = 0x0001;
        public const int CTRL = 0x0002;
        public const int SHIFT = 0x0004;
        public const int WIN = 0x0008;

        //windows message id for hotkey
        public const int WM_HOTKEY_MSG_ID = 0x0312;

        internal static int ToInt(Keys modifier)
        {
            if (modifier == Keys.Shift)
                return SHIFT;
            else if (modifier == Keys.Control)
                return CTRL;
            else if (modifier == Keys.Alt)
                return ALT;
            else if (modifier == (Keys.Control | Keys.Shift))
                return CTRL + SHIFT;
            else if (modifier == (Keys.Control | Keys.Alt))
                return CTRL + ALT;
            else if (modifier == (Keys.Alt | Keys.Shift))
                return ALT + SHIFT;
            else if (modifier == (Keys.Alt | Keys.Shift | Keys.Control))
                return ALT + SHIFT + CTRL;
            else
                return NOMOD;
        }
    }

}
