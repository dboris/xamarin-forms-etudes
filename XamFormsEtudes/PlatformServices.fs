namespace XamFormsEtudes

open Xamarin.Forms


module PlatformServices =
    type IFileIO =
        abstract member ReadText : string -> string
        abstract member WriteText : string -> string -> unit

    let fileService = DependencyService.Get<IFileIO> ()

    //do
        //if fileService = null then failwith "File service not implemented"
