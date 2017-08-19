namespace XamFormsEtudes.Mac

open Foundation
open AppKit

open System
open Xamarin.Forms
open Xamarin.Forms.Platform.MacOS

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit FormsApplicationDelegate ()
    let rect = new CoreGraphics.CGRect (50.0, 50.0, 600.0, 400.0)
    let style = NSWindowStyle.Closable ||| NSWindowStyle.Resizable ||| NSWindowStyle.Titled
    let window = new NSWindow (rect, style, NSBackingStore.Buffered, false)
    do
        window.Title <- "Xamarin.Forms Etudes"
        window.TitleVisibility <- NSWindowTitleVisibility.Hidden

    override __.MainWindow with get () = window

    override self.DidFinishLaunching notification =
        do
            Forms.Init ()
            self.LoadApplication (XamFormsEtudes.App ())
            base.DidFinishLaunching (notification)
