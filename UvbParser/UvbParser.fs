open Områder
open OmrådeParser
open JsonWriter

[<EntryPoint>]
let main argv = 
    let fagbeskrivelser = områder |> List.map parseOmråde
    fagbeskrivelser |> List.iter writeJson
    0 // returner en fornuftig exit kode
