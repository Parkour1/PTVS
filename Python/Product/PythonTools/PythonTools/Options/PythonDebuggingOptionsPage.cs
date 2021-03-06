// Python Tools for Visual Studio
// Copyright(c) Microsoft Corporation
// All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the License); you may not use
// this file except in compliance with the License. You may obtain a copy of the
// License at http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS
// OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY
// IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
// MERCHANTABLITY OR NON-INFRINGEMENT.
//
// See the Apache Version 2.0 License for specific language governing
// permissions and limitations under the License.

using System;
using System.Runtime.InteropServices;
using Microsoft.PythonTools.Parsing;

namespace Microsoft.PythonTools.Options {
    [ComVisible(true)]
    public class PythonDebuggingOptionsPage : PythonDialogPage {
        private PythonDebuggingOptionsControl _window;

        // replace the default UI of the dialog page w/ our own UI.
        protected override System.Windows.Forms.IWin32Window Window {
            get {
                if (_window == null) {
                    _window = new PythonDebuggingOptionsControl();
                    LoadSettingsFromStorage();
                }
                return _window;
            }
        }

        #region Editor Options

        /// <summary>
        /// True to ask the user whether to run when their code contains errors.
        /// Default is false.
        /// </summary>
        [Obsolete("Use PythonToolsService.DebuggerOptions instead")]
        public bool PromptBeforeRunningWithBuildError {
            get { return PyService.DebuggerOptions.PromptBeforeRunningWithBuildError; }
            set { PyService.DebuggerOptions.PromptBeforeRunningWithBuildError = value; }
        }

        /// <summary>
        /// True to start analyzing an environment when it is used and has no
        /// database. Default is true.
        /// </summary>
        [Obsolete("Use PythonToolsService.GeneralOptions instead")]
        public bool AutoAnalyzeStandardLibrary {
            get { return PyService.GeneralOptions.AutoAnalyzeStandardLibrary; }
            set { PyService.GeneralOptions.AutoAnalyzeStandardLibrary = value; }
        }

        /// <summary>
        /// True to copy standard output from a Python process into the Output
        /// window. Default is true.
        /// </summary>
        [Obsolete("Use PythonToolsService.DebuggerOptions instead")]
        public bool TeeStandardOutput {
            get { return PyService.DebuggerOptions.TeeStandardOutput; }
            set { PyService.DebuggerOptions.TeeStandardOutput = value; }
        }

        /// <summary>
        /// The severity to apply to inconsistent indentation. Default is warn.
        /// </summary>
        [Obsolete("Use PythonToolsService.GeneralOptions instead")]
        public Severity IndentationInconsistencySeverity {
            get { return PyService.GeneralOptions.IndentationInconsistencySeverity; }
            set {
                PyService.GeneralOptions.IndentationInconsistencySeverity = value;
            }
        }

        /// <summary>
        /// True to pause at the end of execution when an error occurs. Default
        /// is true.
        /// </summary>
        [Obsolete("Use PythonToolsService.DebuggerOptions instead")]
        public bool WaitOnAbnormalExit {
            get { return PyService.DebuggerOptions.WaitOnAbnormalExit; }
            set { PyService.DebuggerOptions.WaitOnAbnormalExit = value; }
        }

        /// <summary>
        /// True to pause at the end of execution when completing successfully.
        /// Default is true.
        /// </summary>
        [Obsolete("Use PythonToolsService.DebuggerOptions instead")]
        public bool WaitOnNormalExit {
            get { return PyService.DebuggerOptions.WaitOnNormalExit; }
            set { PyService.DebuggerOptions.WaitOnNormalExit = value; }
        }

        /// <summary>
        /// Maximum number of calls between modules to analyze. Default is 1300.
        /// </summary>
        [Obsolete("Use PythonToolsService.GeneralOptions instead")]
        public int? CrossModuleAnalysisLimit {
            get { return PyService.GeneralOptions.CrossModuleAnalysisLimit; }
            set { PyService.GeneralOptions.CrossModuleAnalysisLimit = value; }
        }

        /// <summary>
        /// True to break on a SystemExit exception even when its exit code is
        /// zero. This applies only when the debugger would normally break on
        /// a SystemExit exception. Default is false.
        /// </summary>
        /// <remarks>New in 1.1</remarks>
        [Obsolete("Use PythonToolsService.DebuggerOptions instead")]
        public bool BreakOnSystemExitZero {
            get { return PyService.DebuggerOptions.BreakOnSystemExitZero; }
            set { PyService.DebuggerOptions.BreakOnSystemExitZero = value; }
        }

        /// <summary>
        /// True to update search paths when adding linked files. Default is
        /// true.
        /// </summary>
        /// <remarks>New in 1.1</remarks>
        [Obsolete("Use PythonToolsService.GeneralOptions instead")]
        public bool UpdateSearchPathsWhenAddingLinkedFiles {
            get { return PyService.GeneralOptions.UpdateSearchPathsWhenAddingLinkedFiles; }
            set { PyService.GeneralOptions.UpdateSearchPathsWhenAddingLinkedFiles = value; }
        }

        /// <summary>
        /// True if the standard launcher should allow debugging of the standard
        /// library. Default is false.
        /// </summary>
        /// <remarks>New in 1.1</remarks>
        [Obsolete("Use PythonToolsService.DebuggerOptions instead")]
        public bool DebugStdLib {
            get { return PyService.DebuggerOptions.DebugStdLib; }
            set { PyService.DebuggerOptions.DebugStdLib = value; }
        }

        [Obsolete("Use PythonToolsService.GeneralOptions instead")]
        public event EventHandler IndentationInconsistencyChanged {
            add {
                PyService.GeneralOptions.IndentationInconsistencyChanged += value;
            }
            remove {
                PyService.GeneralOptions.IndentationInconsistencyChanged -= value;
            }
        }

        #endregion

        /// <summary>
        /// Resets settings back to their defaults. This should be followed by
        /// a call to <see cref="SaveSettingsToStorage"/> to commit the new
        /// values.
        /// </summary>
        public override void ResetSettings() {
            PyService.DebuggerOptions.Reset();
        }

        public override void LoadSettingsFromStorage() {
            PyService.DebuggerOptions.Load();

            // Synchronize UI with backing properties.
            if (_window != null) {
                _window.SyncControlWithPageSettings(PyService);
            }
        }

        public override void SaveSettingsToStorage() {
            // Synchronize backing properties with UI.
            if (_window != null) {
                _window.SyncPageWithControlSettings(PyService);
            }

            PyService.DebuggerOptions.Save();

        }
    }
}
