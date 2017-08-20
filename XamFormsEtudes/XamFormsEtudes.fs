namespace XamFormsEtudes

open Xamarin.Forms

type ReadFileResourcePage (mainPage : ContentPage) =
    let viewModel = ReadFileResource.PageVM ()
    let button = Button (Text = "Read File Resource")
    let page = ReadFileResource.Page (viewModel)
    let loadFilePage _ = do
        mainPage.Navigation.PushAsync (page) |> ignore
        viewModel.Text <- "Sample.txt" |> ReadFileResource.readFile
    do
        button.Clicked.Add (loadFilePage)
    member val Button = button with get

type App () = 
    inherit Application ()
    let stack = StackLayout (VerticalOptions = LayoutOptions.Center, Padding = Thickness (40.0))
    let label = Label (XAlign = TextAlignment.Center, Text = "Main Page")
    let mainPage = ContentPage (Content = stack)
    let page1 = ReadFileResourcePage (mainPage)
    do

        stack.Children.Add (label)
        stack.Children.Add (page1.Button)
        base.MainPage <- NavigationPage (mainPage)