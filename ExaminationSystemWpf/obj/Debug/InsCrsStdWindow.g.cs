﻿#pragma checksum "..\..\InsCrsStdWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CB2117D13A4B657B246E20FEA7787097CFFCA20EC43A9C6A90AB3EEEE8681C85"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ExaminationSystemWpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ExaminationSystemWpf {
    
    
    /// <summary>
    /// InsCrsStdWindow
    /// </summary>
    public partial class InsCrsStdWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\InsCrsStdWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PanelHeader;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\InsCrsStdWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid sidePanel;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\InsCrsStdWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView List1;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\InsCrsStdWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridInstructorCourseStudent;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\InsCrsStdWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbCourse;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\InsCrsStdWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbInstructor;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\InsCrsStdWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbStudent;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\InsCrsStdWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAddInstructorCourseStudent;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ExaminationSystemWpf;component/inscrsstdwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\InsCrsStdWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PanelHeader = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 12 "..\..\InsCrsStdWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.sidePanel = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.List1 = ((System.Windows.Controls.ListView)(target));
            
            #line 25 "..\..\InsCrsStdWindow.xaml"
            this.List1.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.GridInstructorCourseStudent = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.cmbCourse = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.cmbInstructor = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.cmbStudent = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.BtnAddInstructorCourseStudent = ((System.Windows.Controls.Button)(target));
            
            #line 93 "..\..\InsCrsStdWindow.xaml"
            this.BtnAddInstructorCourseStudent.Click += new System.Windows.RoutedEventHandler(this.BtnAddInstructorCourseStudent_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

