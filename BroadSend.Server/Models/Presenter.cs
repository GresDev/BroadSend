﻿using System.Collections.Generic;

namespace BroadSend.Server.Models
{
    public class Presenter
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "NameRequired")]
        //[StringLength(256, ErrorMessage = "NameLength", MinimumLength = 1)]
        //[Remote("CheckForItemNameIsUnique", "Presenter")]
        public string Name { get; set; }

        public List<PresenterAlias> Aliases { get; set; }
    }
}