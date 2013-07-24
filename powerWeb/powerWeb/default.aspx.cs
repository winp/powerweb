using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;

namespace powerWeb
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"C:\Windows\system32\cmd.exe";
            psi.WorkingDirectory = Path.GetDirectoryName(psi.FileName);
            Process.Start(psi);
        }
    }
}