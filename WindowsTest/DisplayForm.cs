using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsTest
{
    public partial class DisplayForm : Form
    {
        private GLProgram program;

        protected override void CreateHandle()
        {
            base.CreateHandle();
            program.Init();
            RenderTimer.Start();
        }

        public DisplayForm()
        {
            program = new GLProgram(this);
            InitializeComponent();
        }

        private void RenderTick(object sender, EventArgs e)
        {
            program.Render();
        }
    }
}
