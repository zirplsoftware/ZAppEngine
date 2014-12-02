using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using EnvDTE80;

namespace Zirpl.AppEngine.CodeGeneration.VisualStudio
{
    public static class VisualStudio
    {
        [DllImport("ole32.dll")]
        private static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);
        [DllImport("ole32.dll")]
        private static extern void GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

        public static DTE2 GetCurrentInstance()
        {
            // TODO: this method needs to handle case of multiple VS's being open
            // TODO: this method needs to handle other VS versions

            //rot entry for visual studio running under current process.

            var expectedEntryName = System.Diagnostics.Debugger.IsAttached
                ? "!VisualStudio.DTE.12.0"
                : String.Format("!VisualStudio.DTE.12.0:{0}", System.Diagnostics.Process.GetCurrentProcess().Id);

            IRunningObjectTable runningObjectTable;
            GetRunningObjectTable(0, out runningObjectTable);
            IEnumMoniker enumMoniker;
            runningObjectTable.EnumRunning(out enumMoniker);
            enumMoniker.Reset();
            IntPtr fetched = IntPtr.Zero;
            IMoniker[] moniker = new IMoniker[1];
            while (enumMoniker.Next(1, moniker, fetched) == 0)
            {
                IBindCtx bindCtx;
                CreateBindCtx(0, out bindCtx);
                string displayName;
                moniker[0].GetDisplayName(bindCtx, null, out displayName);

                var match = System.Diagnostics.Debugger.IsAttached
                    ? displayName.StartsWith(expectedEntryName)
                    : displayName == expectedEntryName;
                if (match)
                {
                    object comObject;
                    runningObjectTable.GetObject(moniker[0], out comObject);
                    return (DTE2)comObject;
                }
            }
            return null;
        }
    }
}
