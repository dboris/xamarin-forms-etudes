﻿namespace XamFormsEtudes

open System
open System.Reflection
open System.ComponentModel
open System.Diagnostics
open Xamarin.Forms


module ReadFileResource =
    type internal Marker = interface end
    let markerType = typeof<Marker>
    let assembly = (markerType.GetTypeInfo ()).Assembly

    let printAssemblyResources () =
        assembly.GetManifestResourceNames () |> Seq.iter (fun res -> Debug.WriteLine (res))

    let readFile fileName =
        // Xamarin guide suggests to use namespace qualified file name, but this doesn't work
        // https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/files/
        //let qualifiedName = sprintf "XamFormsEtudes.%s" fileName
        let stream = assembly.GetManifestResourceStream (fileName)
        use reader = new IO.StreamReader (stream)
        reader.ReadToEnd ()

    type PageVM () =
        let mutable text = "Loading..."
        let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs> ()
        interface INotifyPropertyChanged with
            [<CLIEvent>] member __.PropertyChanged = propertyChanged.Publish

        member self.Text
            with get () = text
            and set value = do
                text <- value
                propertyChanged.Trigger (self, PropertyChangedEventArgs ("Text"))

    type Page (viewModel : PageVM) as self =
        inherit ContentPage ()
        let stack = StackLayout (VerticalOptions = LayoutOptions.Center)
        let label = Label (Text = viewModel.Text)
        do
            self.BindingContext <- viewModel
            label.SetBinding (Label.TextProperty, "Text")
            stack.Children.Add (label)
            base.Content <- stack