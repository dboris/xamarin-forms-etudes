namespace XamFormsEtudes

open Xamarin.Forms

type ReadFileResourcePage (mainNav : INavigation) =
    let viewModel = ReadFileResource.PageVM ()
    let page = ReadFileResource.Page (viewModel)
    let button = Button (Text = "Read File Resource")
    let onButtonClick _ = do
        mainNav.PushAsync (page) |> ignore
        viewModel.Text <- "Sample.txt" |> AssemblyResources.readFile
    do
        button.Clicked.Add (onButtonClick)
    member val Button = button with get

type ReadAndWriteFilesPage (mainNav : INavigation) =
    let viewModel = ReadAndWriteFiles.PageVM ("WrittenText.txt")
    let page = ReadAndWriteFiles.Page (viewModel)
    let button = Button (Text = "Read And Write Files")
    let onButtonClick _ = do
        mainNav.PushAsync (page) |> ignore
    do
        button.Clicked.Add (onButtonClick)
    member val Button = button with get

type App () = 
    inherit Application ()
    let stack = StackLayout (VerticalOptions = LayoutOptions.Center, Padding = Thickness (40.0))
    let label = Label (XAlign = TextAlignment.Center, Text = "Main Page")
    let mainPage = ContentPage (Content = stack)
    let navPage = NavigationPage (mainPage)
    let page1 = ReadFileResourcePage (navPage.Navigation)
    let page2 = ReadAndWriteFilesPage (navPage.Navigation)

    do

        stack.Children.Add (label)
        stack.Children.Add (page1.Button)
        stack.Children.Add (page2.Button)
        base.MainPage <- navPage
