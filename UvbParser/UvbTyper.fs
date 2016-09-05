module UvbTyper

type Info = 
    { Fag : string
      Niveau : string
      Område : string
      Url : string
      Version : string }

type Fagbeskrivelse = 
    { omraade : string
      fag : string
      niveau : string
      version : string
      url : string
      fagmaalList : string list
      kernestofList : string list }