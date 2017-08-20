namespace XamFormsEtudes

module PlatformServices =
    type IFileIO =
        abstract member ReadText : string -> string
        abstract member WriteText : string -> string -> unit
