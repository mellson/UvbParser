module ListeParser

open FSharp.Data

// Splitter noden ved split og tilføjer endelsen ending hvis den ikke allerede slutter på ending.
let splitOgTilføjEndelse (node : string) (split : char) (ending : string) = 
    match node.Trim().Split split with
    | [| _; res |] -> 
        match res.Trim().EndsWith ending with
        | true -> res.Trim()
        | false -> res.Trim() + ending
    | _ -> ""

// Hjælpemetode der rekursivt løber listen af html noder igennem og undervejse 
let rec parseListeHelper (noder : HtmlNode list) (parentName : string) (liste : string list) = 
    match noder |> List.filter (fun n -> n.HasClass("Liste1") || n.HasClass("Liste2")) with
    | node :: resten -> 
        match node.HasClass("Liste1") with
        | true -> 
            match not resten.IsEmpty with
            | true -> 
                match resten.Head.HasClass("Liste2") with
                | true -> parseListeHelper resten (splitOgTilføjEndelse (node.InnerText()) ')' ":") liste
                | false -> parseListeHelper resten parentName (splitOgTilføjEndelse (node.InnerText()) '–' "." :: liste)
            | false -> parseListeHelper resten parentName (splitOgTilføjEndelse (node.InnerText()) '–' "." :: liste)
        | false -> parseListeHelper resten parentName (parentName + " " + splitOgTilføjEndelse (node.InnerText()) '–' "." :: liste)
    // Så er vi igennem alle noder og returnerer nu den liste vi har fundet (minus tomme elementer)
    | [] -> liste |> List.filter (fun x -> x <> "") |> List.rev 

// Løber listen af noder igennem mellem start og slut og henter indhold ud af Liste- 1 og 2 klasser.
let parseListe (noder : HtmlNode seq) (start : string) (slut : string) = 
    match noder |> Seq.tryFindIndex (fun n -> n.InnerText() = start) with
    | Some startIndex -> 
        match noder |> Seq.tryFindIndex (fun n -> n.InnerText() = slut) with
        | Some endIndex -> 
            let node_udsnit = 
                noder
                |> Seq.skip (startIndex + 1)
                |> Seq.take (endIndex - startIndex - 1)
                |> Seq.toList
            parseListeHelper node_udsnit "" []
        | None -> []
    | None -> []