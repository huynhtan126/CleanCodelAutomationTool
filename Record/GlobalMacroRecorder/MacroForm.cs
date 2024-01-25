using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MouseKeyboardLibrary;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace GlobalMacroRecorder
{
    public partial class MacroForm : MetroFramework.Forms.MetroForm
    {

        List<MacroEvent> events = new List<MacroEvent>();
        int lastTimeRecorded = 0;

        MouseHook mouseHook = new MouseHook();
        KeyboardHook keyboardHook = new KeyboardHook();

        public MacroForm()
        {

            InitializeComponent();

            mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);

            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);

        }

        void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.MouseMove,
                    e.X+"&&"+e.Y,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.MouseDown,
                    e.Button.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.MouseUp,
                    e.Button.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.KeyDown,
                     e.KeyCode.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.KeyUp,
                    e.KeyCode.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        private void recordStartButton_Click(object sender, EventArgs e)
        {

            if (recordButton.Text == "Record")
            {
                events.Clear();
                lastTimeRecorded = Environment.TickCount;

                keyboardHook.Start();
                mouseHook.Start();
                recordButton.Text = "Stop";
            }
            else
            {

                keyboardHook.Stop();
                mouseHook.Stop();
                ExportJson();
                recordButton.Text = "Record";

            }

        }
        public void LoadRecordedMacro()
        {
            foreach (MacroEvent macroEvent in events)
            {

            }
        }
        public string duongdanfolder = "C:\\Users\\huynh\\Documents\\";
        public void ExportJson()
        {
            var vanbanKho = JsonConvert.SerializeObject(events);
            File.WriteAllText(duongdanfolder + "\\testcast1.dat", vanbanKho);
        }

        private void playBackMacroButton_Click(object sender, EventArgs e)
        {
            var duongdan = duongdanfolder + "\\testcast1.dat";
            if (File.Exists(duongdan))
            {
                var vanban = File.ReadAllText(duongdan);
                var docMacro = JsonConvert.DeserializeObject<List<MacroEvent>>(vanban);
                this.events = docMacro;
            }

            foreach (MacroEvent macroEvent in events)
            {

                Thread.Sleep(macroEvent.TimeSinceLastEvent);

                switch (macroEvent.MacroEventType)
                {
                    case MacroEventType.MouseMove:
                        {

                            //MouseEventArgs mouseArgs = (MouseEventArgs)macroEvent.EventArgs;

                            var click = Regex.Split(macroEvent.EventArgs, "&&");

                            MouseSimulator.X = int.Parse(click[0]);
                            MouseSimulator.Y = int.Parse(click[1]);

                        }
                        break;
                    case MacroEventType.MouseDown:
                        {

                            //MouseEventArgs mouseArgs = macroEvent.EventArgs  as MouseEventArgs;
                            MouseButton button = (MouseButton)Enum.Parse(typeof(MouseButton), macroEvent.EventArgs);
                            MouseSimulator.MouseDown(button);

                        }
                        break;
                    case MacroEventType.MouseUp:
                        {

                            MouseButton button = (MouseButton)Enum.Parse(typeof(MouseButton), macroEvent.EventArgs);

                            MouseSimulator.MouseUp(button);

                        }
                        break;
                    case MacroEventType.KeyDown:
                        {

                            //KeyEventArgs keyArgs = (KeyEventArgs)macroEvent.EventArgs;
                            Keys keys = (Keys)Enum.Parse(typeof(Keys), macroEvent.EventArgs);
                            KeyboardSimulator.KeyDown(keys);

                        }
                        break;
                    case MacroEventType.KeyUp:
                        {

                            //KeyEventArgs keyArgs = (KeyEventArgs)macroEvent.EventArgs;
                            Keys keys = (Keys)Enum.Parse(typeof(Keys), macroEvent.EventArgs);
                            KeyboardSimulator.KeyUp(keys);

                        }
                        break;
                    default:
                        break;
                }

            }

        }

        private void MacroForm_Load(object sender, EventArgs e)
        {

        }

    }
}
