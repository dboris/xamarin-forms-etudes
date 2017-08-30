namespace XamFormsEtudes

open System
open System.ComponentModel
open Xamarin.Forms

module Controls =
    let log = Diagnostics.Debug.WriteLine

    type HybridWebView () =
        inherit View ()

        static let UriProperty = BindableProperty.Create ("Uri", typeof<string>, typeof<HybridWebView>, String.Empty)
        let mutable action = None

        member self.Uri
            with get () = self.GetValue UriProperty :?> string
            and set (value : string) = 
                self.SetValue (UriProperty, value)

        member __.RegisterAction (callback : Action<string>) = 
            action <- Some callback

        member __.Cleanup () = 
            action <- None

        member __.InvokeAction (data : string) =
            if data <> null
            then match action with
                 | None -> ()
                 | Some cb -> cb.Invoke data

module HybridWebview =
    type ViewModel () =
        let htmlSource = HtmlWebViewSource ()
        do
            htmlSource.Html <- """<html>
              <body>
                <h1>Web View</h1>
              </body>
            </html>"""

        member val HtmlSource = htmlSource with get


    type Page (viewModel : ViewModel) as self =
        inherit ContentPage (Padding = Thickness (0., 20., 0., 0.))
        let browser = Controls.HybridWebView (Uri = "index.html", 
                                              HorizontalOptions = LayoutOptions.FillAndExpand, 
                                              VerticalOptions = LayoutOptions.FillAndExpand)

        do 
            browser.RegisterAction (fun data -> self.DisplayAlert ("Alert", sprintf "Hello, %s" data, "OK") |> ignore)
            base.Content <- browser

