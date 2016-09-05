module FagbeskrivelserParser

open BilagParser
open FSharp.Data
open UvbTyper

let rec parseFagbeskrivelser (url : string) (nodes : HtmlNode seq) (bilag : string list) (fagbeskrivelser : Fagbeskrivelse list) = 
    match bilag with
    // Vi har fat i et bilag der efterfølges af endnu et bilag
    | bilag1 :: bilag2 :: flereBilag -> 
        // Lav et udsnit af html noder der kun omhandler bilag1
        let startIndex = nodes |> Seq.findIndex (fun n -> n.HasId(bilag1))
        let endIndex = nodes |> Seq.findIndex (fun n -> n.HasId(bilag2))
        let bilag1_noder = 
            nodes
            |> Seq.skip (startIndex + 1)
            |> Seq.take (endIndex - startIndex - 1)
        // Byg fagbeskrivelsen til bilag1
        let fagbeskrivelse = parseBilag (url + "#" + bilag1) bilag1_noder
        // Parse videre og tilføj den fagbeskrivelse vi netop har fundet
        parseFagbeskrivelser url nodes (bilag2 :: flereBilag) (fagbeskrivelse :: fagbeskrivelser)
    // Vi har fat i det sidste bilag
    | bilag1 :: flereBilag -> 
        // Lav et udsnit af indhold der kun omhandler det sidste bilag
        let startIndex = nodes |> Seq.findIndex (fun n -> n.HasId(bilag1))
        let bilag = nodes |> Seq.skip (startIndex + 1)
        // Byg fagbeskrivelsen til bilaget og filføj det til vores resultat
        let fagbeskrivelse = parseBilag (url + "#" + bilag1) bilag
        parseFagbeskrivelser url nodes flereBilag (fagbeskrivelse :: fagbeskrivelser)
    // Der er ikke flere bilag og vi er nu færdige så vi returnerer de fagbeskrivelser vi har fundet
    | [] -> List.rev fagbeskrivelser