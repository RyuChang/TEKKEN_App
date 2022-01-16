﻿using System.Collections.Generic;

namespace TekkenApp.Models
{
    //[Index(nameof(Code), Name = "IX_hitType", IsUnique = true)]
    //[Index(nameof(Number), Name = "IX_hitType_1", IsUnique = true)]

    public partial class HitType : BaseDataEntity
    {
        public HitType()
        {
            SetApp(TableName.HitType);
            NameSet = new HashSet<HitType_name>();
            //move_datacounterType_codeNavigation = new HashSet<Move_data>();
            //move_dataguardType_codeNavigation = new HashSet<Move_data>();
            //move_datahitType_codeNavigation = new HashSet<Move_data>();
            //move_datastartType_codeNavigation = new HashSet<Move_data>();
          //  ICollection<object> NameSet1;
        //    NameSet = new HashSet<HitType_name>() as ICollection<BaseNameEntity>;
            
        }
        public override ICollection<HitType_name> NameSet { get; set; }
        


        //public virtual ICollection<Move_data> move_datacounterType_codeNavigation { get; set; }
        //public virtual ICollection<Move_data> move_dataguardType_codeNavigation { get; set; }
        //public virtual ICollection<Move_data> move_datahitType_codeNavigation { get; set; }
        //public virtual ICollection<Move_data> move_datastartType_codeNavigation { get; set; }
    }
}
