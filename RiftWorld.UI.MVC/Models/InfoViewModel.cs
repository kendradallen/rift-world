﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Runtime;
using RiftWorld.DATA.EF;

namespace RiftWorld.UI.MVC.Models
{
    public class ApprovalVM
    {
        public List<Rumor> Rumors { get; set; }
        public List<Character> Characters { get; set; }
        public List<Journal> Journals { get; set; }
        public List<Character> CharacterEdits { get; set; }
        public List<Journal> JournalEdits { get; set; }
        public List<Character> RetireRequests { get; set; }

        public List<UserDetail> Users { get; set; }
        public List<CharOrg> Orgs { get; set; }
    }

    public class ComboResult
    {
        public List<Info> Infos { get; set; }
        public List<Story> Stories { get; set; }
    }

    public class ComboResultUltra
    {
        public List<Info> Infos { get; set; }
        public List<Story> Stories { get; set; }
        public List<Character> Characters { get; set; }
    }
}