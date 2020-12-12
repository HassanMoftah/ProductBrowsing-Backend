using dbsqlbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsBrowsing_backend.Models
{
    public class MDUser : TBModelBase
    {
        [MDTBProperty(IsdbReadWrite = true)]
        public string Username { get; set; }

        [MDTBProperty(IsdbReadWrite = true)]
        public string Password { get; set; }

        public MDUser() { }

        public static bool IsValid(MDUser user) {

            MDUser userbyusername = new MDUser().GetByParameter<MDUser>("Username", user.Username,out string msg).FirstOrDefault();
            if (userbyusername == null) { return false; }
            if (userbyusername.Password == user.Password) { return true; }
            else { return false; }
        }
        public override string GetTableName()
        {
            return "TBUsers";
        }
    }
}