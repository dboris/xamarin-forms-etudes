namespace XamFormsEtudes

open Xamarin.Forms

module ReadFileResource =
    type Page () =
        inherit ContentPage ()
        let stack = StackLayout (VerticalOptions = LayoutOptions.Center)
        let label = Label (XAlign = TextAlignment.Center, Text = "Welcome to F# Xamarin.Forms!")
        do 
            stack.Children.Add (label)
            base.Content <- stack


type App () = 
    inherit Application ()
    let stack = StackLayout (VerticalOptions = LayoutOptions.Center, Padding = Thickness (40.0))
    let label = Label (XAlign = TextAlignment.Center, Text = "Main Page")
    let button = Button (Text = "Read File Resource")
    let mainPage = ContentPage (Content = stack)
    let nextPage = ReadFileResource.Page ()
    do
        button.Clicked.Add (fun arg -> mainPage.Navigation.PushAsync (nextPage) |> ignore)
        stack.Children.Add (label)
        stack.Children.Add (button)
        base.MainPage <- NavigationPage (mainPage)